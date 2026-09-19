// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

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
