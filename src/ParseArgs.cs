// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;

using Mono.Options;

using Spectre.Console;

namespace QuickSetup;

internal record Settings(
    bool SetupDefault,
    bool SetupMinimal,
    bool SetupDev
);

internal static class CommandArgs
{
    private const string HelpText = """
        QuickSetup, configure once and forget.

        [Aqua]USAGE[/]
            [Gray50]quicksetup[/] [[OPTIONS]]

        [Aqua]OPTIONS[/]
            -m, --minimal   Default installation including GUI apps.
            -d, --default   Minimal installation only including CLI tools.
            -D, --dev       Dev installation configuring Docker and installing useful dev tools.
            -h, --help      Show help.
        """;

    public static Settings Init(string[] args)
    {
        var isDefault = false;
        var isMinimal = false;
        var isDev = false;
        var showHelp = false;

        var options = new OptionSet
        {
            { "m|minimal", _ => isMinimal = true },
            { "d|default", _ => isDefault = true },
            { "D|dev", _ => isDev = true },
            { "h|help", _ => showHelp = true },
        };

        List<string> positional = null!;
        try
        {
            positional = options.Parse(args);
        }
        catch (OptionException e)
        {
            Fail(e.Message);
        }

        if (showHelp || args.Length == 0)
            ShowHelpAndExit();

        if (positional.Count > 0)
            Fail($"Unexpected argument \"{string.Join(" ", positional)}\"");

        var selectedCount =
            (isDefault ? 1 : 0) +
            (isMinimal ? 1 : 0) +
            (isDev     ? 1 : 0);

        if (selectedCount > 1)
        {
            Fail("Only one of --minimal, --default, or --dev can be specified.");
        }

        return new Settings(isDefault, isMinimal, isDev);
    }

    private static void ShowHelpAndExit()
    {
        AnsiConsole.MarkupLine(HelpText);
        Environment.Exit(0);
    }

    private static void Fail(string message)
    {
        AnsiConsole.MarkupLine($"[red]Error[/]: {message}");
        AnsiConsole.MarkupLine("Use [Yellow2]--help[/] to see all commands.");
        Environment.Exit(1);
    }
}
