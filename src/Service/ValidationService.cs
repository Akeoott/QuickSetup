// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

using QuickSetup.Process;

namespace QuickSetup.Service;

internal static class ValidationService
{
    public static void Register(ProcessRunner runner)
    {
        runner.Check(StepCategory.Validation, "Running as normal user",
            () => !string.Equals(Environment.GetEnvironmentVariable("USER"), "root",
                                 StringComparison.Ordinal),
            "DO NOT run this as root. Run as your normal user.");

        runner.Check(StepCategory.Validation, "Internet connection",
            () => Utils.RunCapture("ping", "-c", "1", "archlinux.org").ExitCode == 0,
            "No internet connection. Connect and retry.");

        runner.Command(StepCategory.Validation, "Sudo access",
            "sudo", ["-v"]);

        runner.Check(StepCategory.Validation, "Running from backup directory",
            () =>
            {
                var runtime = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Code", "backup");
                return Path.GetFullPath(Environment.CurrentDirectory) == runtime
                       && Directory.Exists(".config");
            },
            "Must be run from ~/Code/backup alongside its .config directory.");
    }
}
