// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using QuickSetup.Cli;
using QuickSetup.Process;

using Spectre.Console;

namespace QuickSetup.Service;

internal static class ConfigService
{
    private static readonly (string Src, string Dest)[] Mappings =
    [
        ("starship.toml",           ".config/starship.toml"),
        ("fish/config.fish",        ".config/fish/config.fish"),
        ("fastfetch/archlinux.png", ".config/fastfetch/archlinux.png"),
        ("fastfetch/config.jsonc",  ".config/fastfetch/config.jsonc"),
        ("nano/nanorc",             ".config/nano/nanorc"),
    ];

    public static void Register(ProcessRunner runner, Settings settings)
    {
        runner.Action(StepCategory.Configuration, "Set fish as default shell", SetFishShell);

        runner.Action(StepCategory.Configuration, "Deploy user configs", DeployConfigs);

        runner.Command(StepCategory.Configuration, "Enable docker service",
            "sudo", ["systemctl", "enable", "--now", "docker.service"],
            condition: () => settings.RunDev);

        runner.Action(StepCategory.Configuration, "Add user to docker group", () =>
                Utils.RunInteractive("sudo", "usermod", "-aG", "docker",
                    Environment.GetEnvironmentVariable("USER")!),
            condition: () => settings.RunDev);
    }

    private static int SetFishShell()
    {
        var fish = File.Exists("/usr/bin/fish") ? "/usr/bin/fish" : "/usr/local/bin/fish";
        if (!File.Exists(fish)) return 0;
        if (Environment.GetEnvironmentVariable("SHELL") == fish) return 0;

        return Utils.RunInteractive("sudo", "chsh", "-s", fish,
            Environment.GetEnvironmentVariable("USER")!);
    }

    private static int DeployConfigs()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var origin = Path.Combine(home, "Code", "backup", ".config");

        if (!Directory.Exists(origin)) return 1;

        foreach (var (src, dest) in Mappings)
        {
            var srcFull = Path.Combine(origin, src);
            var destFull = Path.Combine(home, dest);

            if (!File.Exists(srcFull) && !Directory.Exists(srcFull))
            {
                AnsiConsole.MarkupLine(
                    $"[yellow]  Missing source:[/] {Markup.Escape(srcFull)}");
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(destFull)!);

            if (File.Exists(destFull) || Directory.Exists(destFull))
                File.Delete(destFull);

            File.CreateSymbolicLink(destFull, srcFull);

            AnsiConsole.MarkupLine(
                $"[grey]  linked[/] {Markup.Escape(src)} [grey]→[/] {Markup.Escape(dest)}");
        }

        return 0;
    }
}
