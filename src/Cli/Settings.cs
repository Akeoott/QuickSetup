// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace QuickSetup.Cli;

internal record Settings(
    bool SetupDefault,
    bool SetupMinimal,
    bool SetupDev)
{
    /// <summary>Default preset implies Minimal as well.</summary>
    internal bool RunDefault => SetupDefault || SetupDev;

    /// <summary>Dev preset implies Minimal and Default as well.</summary>
    internal bool RunDev => SetupDev;
}
