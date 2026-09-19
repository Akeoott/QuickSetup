// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using QuickSetup.Cli;
using QuickSetup.Process;
using QuickSetup.Service;

using Spectre.Console;

namespace QuickSetup;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var settings = Commandline.Init(args);

        AnsiConsole.Write(new Rule("[bold blue]QuickSetup[/]\n")
            .RuleStyle("grey").LeftJustified());

        var runner = new ProcessRunner();

        ValidationService.Register(runner);
        SystemService.Register(runner);
        PackageService.Register(runner, settings);
        FontService.Register(runner, settings);
        ConfigService.Register(runner, settings);

        var exitCode = runner.Run();
        if (exitCode != 0) return exitCode;

        PrintCompletion();
        return 0;
    }

    private static void PrintCompletion()
    {
        AnsiConsole.MarkupLine("\n[bold green]Script finished.[/]");
        AnsiConsole.MarkupLine("[bold green]Please reboot to apply all changes.[/]\n");
    }
}
