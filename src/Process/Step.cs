// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

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
