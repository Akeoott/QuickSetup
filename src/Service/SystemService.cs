// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using QuickSetup.Process;

namespace QuickSetup.Service;

internal static class SystemService
{
    private static readonly string[] DotnetChannels = ["8.0", "9.0", "10.0"];

    public static void Register(ProcessRunner runner)
    {
        runner.Action(StepCategory.System, "Enable multilib repository", EnableMultilib);

        runner.Command(StepCategory.System, "Update system & base dependencies",
            "sudo",
            ["pacman", "-Syu", "--noconfirm", "--needed",
             "base-devel", "git", "curl", "gnupg", "rustup"]);

        runner.Command(StepCategory.System, "Set Rust stable toolchain",
            "rustup", ["default", "stable"]);

        runner.Action(StepCategory.System, "Install .NET 8.0 / 9.0 / 10.0", InstallDotnet);

        runner.Action(StepCategory.System, "Build and install paru", InstallParu);
    }

    private static int EnableMultilib()
    {
        const string conf = "/etc/pacman.conf";
        var text = File.ReadAllText(conf);
        if (!text.Contains("#[multilib]")) return 0;

        return Utils.RunInteractive("sudo", "sed", "-i",
            @"/^#\[multilib\]/,/^#Include/s/^#//", conf);
    }

    private static int InstallDotnet()
    {
        var tmp = Directory.CreateTempSubdirectory().FullName;
        try
        {
            var script = Path.Combine(tmp, "dotnet-install.sh");
            if (Utils.RunCapture("curl", "-sSL",
                    "https://dot.net/v1/dotnet-install.sh", "-o", script).ExitCode != 0)
                return 1;

            Utils.RunCapture("chmod", "+x", script);

            return DotnetChannels
                .Select(channel => Utils.RunInteractive("bash", script, "-c", channel))
                .FirstOrDefault(code => code != 0);
        }
        finally
        {
            TryDelete(tmp);
        }
    }

    private static int InstallParu()
    {
        if (Utils.RunCapture("which", "paru").ExitCode == 0) return 0;


        var tmp = Directory.CreateTempSubdirectory().FullName;
        try
        {
            var clone = Path.Combine(tmp, "paru");
            if (Utils.RunInteractive("git", "clone",
                    "https://aur.archlinux.org/paru.git", clone) != 0)
                return 1;

            return Utils.RunInteractive("bash", "-c",
                $"cd '{clone}' && makepkg -si --noconfirm --needed");
        }
        finally
        {
            TryDelete(tmp);
        }
    }

    private static void TryDelete(string path)
    {
        try { Directory.Delete(path, recursive: true); }
        catch { /* ignored */ }
    }
}
