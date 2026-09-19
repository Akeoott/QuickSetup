// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

namespace QuickSetup.Process;

internal enum StepCategory
{
    Validation,
    System,
    Packages,
    Fonts,
    Configuration,
}

internal sealed record Step(
    StepCategory Category,
    string Name,
    Func<int> Action,
    Func<bool>? Condition = null);

internal sealed record StepResult(
    Step Step,
    bool Ran,
    int ExitCode,
    TimeSpan Duration);
