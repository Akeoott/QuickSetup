// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;

using SysProcess = System.Diagnostics.Process;

namespace QuickSetup.Process;

public static class Utils
{
    /// <summary>Runs a command with full terminal access. Prompts work.</summary>
    public static int RunInteractive(string fileName, params string[] arguments)
    {
        var psi = new ProcessStartInfo { FileName = fileName, UseShellExecute = false };
        foreach (var arg in arguments) psi.ArgumentList.Add(arg);

        using var process = SysProcess.Start(psi)!;
        process.WaitForExit();
        return process.ExitCode;
    }

    /// <summary>Runs a command and captures stdout. For non-interactive checks only.</summary>
    public static (int ExitCode, string Stdout) RunCapture(string fileName, params string[] arguments)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            foreach (var arg in arguments) psi.ArgumentList.Add(arg);

            using var process = SysProcess.Start(psi)!;
            var stdout = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return (process.ExitCode, stdout.Trim());
        }
        catch
        {
            return (127, "");
        }
    }
}
