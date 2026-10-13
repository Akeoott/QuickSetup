// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace QuickSetup.Cli;

/// <summary>
///
/// </summary>
/// <param name="SetupDefault">If true, will install regular desktop packages</param>
/// <param name="SetupMinimal">If true, will only install minimal packages</param>
/// <param name="SetupDev">If true, will install dev packages</param>
/// <param name="SetNvidia">If true, will set Nvidia environmental variables.</param>
/// <returns></returns>
internal record Settings(
    bool SetupDefault,
    bool SetupMinimal,
    bool SetupDev,
    bool SetNvidia)
{
    /// <summary>Default preset implies Minimal as well.</summary>
    internal bool RunDefault => SetupDefault || SetupDev;

    /// <summary>Dev preset implies Minimal and Default as well.</summary>
    internal bool RunDev => SetupDev;
}
