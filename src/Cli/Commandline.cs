// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using Mono.Options;

using Spectre.Console;

namespace QuickSetup.Cli;

internal static class Commandline
{
    public static Settings Init(string[] args)
    {
        var isDefault = false;
        var isMinimal = false;
        var isDev = false;

        var showInfo = false;
        var showHelp = false;

        var options = new OptionSet
        {
            { "m|minimal", _ => isMinimal = true },
            { "d|default", _ => isDefault = true },
            { "D|dev",     _ => isDev     = true },
            { "i|info",    _ => showInfo  = true },
            { "h|help",    _ => showHelp  = true },
        };

        List<string> positional;
        try
        {
            positional = options.Parse(args);
        }
        catch (OptionException e)
        {
            Fail(e.Message);
            return null!; // unreachable
        }

        if (showHelp || args.Length == 0)
            ShowHelpAndExit();
        if (showInfo)
            ShowInfoAndExit();

        if (positional.Count > 0)
            Fail($"Unexpected argument \"{string.Join(" ", positional)}\"");

        var selectedCount =
            (isDefault ? 1 : 0) +
            (isMinimal ? 1 : 0) +
            (isDev ? 1 : 0);

        if (selectedCount > 1)
            Fail("Only one of --minimal, --default, or --dev can be specified.");

        return new Settings(isDefault, isMinimal, isDev);
    }

    private static void ShowHelpAndExit()
    {
        AnsiConsole.MarkupLine(Text.Help);
        Environment.Exit(0);
    }

    private static void ShowInfoAndExit()
    {
        AnsiConsole.MarkupLine(Text.Info);
        Environment.Exit(0);
    }

    private static void Fail(string message)
    {
        AnsiConsole.MarkupLine($"[red]Error[/]: {Markup.Escape(message)}");
        AnsiConsole.MarkupLine("Use [Yellow2]--help[/] to see all commands.");
        Environment.Exit(1);
    }
}
