// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;

using Spectre.Console;

namespace QuickSetup.Process;

internal sealed class ProcessRunner
{
    private readonly List<Step> _steps = [];
    private readonly List<StepResult> _results = [];

    public ProcessRunner Command(
        StepCategory category, string name,
        string file, string[] args,
        Func<bool>? condition = null)
    {
        _steps.Add(new Step(category, name,
            () => Utils.RunInteractive(file, args), condition));
        return this;
    }

    public ProcessRunner Action(
        StepCategory category, string name,
        Func<int> action,
        Func<bool>? condition = null)
    {
        _steps.Add(new Step(category, name, action, condition));
        return this;
    }

    /// <summary>Predicate step. Returns 0 if <paramref name="ok"/> is true, 1 otherwise.</summary>
    public ProcessRunner Check(
        StepCategory category, string name,
        Func<bool> ok, string? failMessage = null)
    {
        _steps.Add(new Step(category, name, () =>
        {
            if (ok()) return 0;
            if (failMessage is not null)
                AnsiConsole.MarkupLine($"[red]  {Markup.Escape(failMessage)}[/]");
            return 1;
        }));
        return this;
    }

    public int Run()
    {
        var exit = 0;

        for (var i = 0; i < _steps.Count; i++)
        {
            var step = _steps[i];

            if (step.Condition is not null && !step.Condition())
            {
                _results.Add(new StepResult(step, false, 0, TimeSpan.Zero));
                continue;
            }

            AnsiConsole.MarkupLine(
                $"[bold blue]▶[/] [bold]{Markup.Escape(step.Name)}[/]");

            var sw = Stopwatch.StartNew();
            var code = step.Action();
            sw.Stop();

            _results.Add(new StepResult(step, true, code, sw.Elapsed));

            if (code != 0)
            {
                exit = code;
                break;
            }
        }

        AnsiConsole.WriteLine();
        RenderSummary();
        return exit;
    }

    private void RenderSummary()
    {
        var tree = new Tree("[bold green]Summary[/]").Guide(TreeGuide.Line);

        foreach (var category in Enum.GetValues<StepCategory>())
        {
            var items = _results.Where(r => r.Step.Category == category).ToList();
            if (items.Count == 0) continue;

            var catNode = tree.AddNode($"[bold aqua]{category}[/]");

            foreach (var r in items)
            {
                var (icon, color) = !r.Ran
                    ? ("⏭", "grey")
                    : r.ExitCode == 0
                        ? ("✔", "green")
                        : ("✘", "red");

                var duration = r.Ran
                    ? $" [grey]({r.Duration.TotalSeconds:F1}s)[/]"
                    : " [grey](skipped)[/]";

                catNode.AddNode(
                    $"[{color}]{icon}[/] {Markup.Escape(r.Step.Name)}{duration}");
            }
        }

        AnsiConsole.Write(tree);

        var ran = _results.Count(r => r.Ran && r.ExitCode == 0);
        var failed = _results.Count(r => r.Ran && r.ExitCode != 0);
        var skipped = _results.Count(r => !r.Ran);
        var total = _results.Sum(r => r.Duration.TotalSeconds);

        AnsiConsole.WriteLine();

        var line =
            $"[bold green]{ran} succeeded[/] · " +
            (failed > 0 ? $"[red]{failed} failed[/] · " : "") +
            $"[grey]{skipped} skipped[/] · " +
            $"[grey]{total:F1}s total[/]";

        AnsiConsole.MarkupLine(line);
    }
}
