// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

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
