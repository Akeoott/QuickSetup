// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

using QuickSetup.Cli;
using QuickSetup.Process;

namespace QuickSetup.Service;

internal static class PackageService
{
    public static void Register(ProcessRunner runner, Settings settings)
    {
        runner.Command(StepCategory.Packages, "Minimal packages",
            "paru", ["-S", "--noconfirm", "--needed", "--skipreview",
                     .. PackageList.Minimal]);

        runner.Command(StepCategory.Packages, "Default packages",
            "paru", ["-S", "--noconfirm", "--needed", "--skipreview",
                     .. PackageList.Default],
            condition: () => settings.RunDefault);

        runner.Command(StepCategory.Packages, "Dev packages",
            "paru", ["-S", "--noconfirm", "--needed", "--skipreview",
                     .. PackageList.Dev],
            condition: () => settings.RunDev);
    }
}
