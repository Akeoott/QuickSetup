// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using QuickSetup.Cli;
using QuickSetup.Process;

namespace QuickSetup.Service;

internal static class FontService
{
    private const string InstallScript = """
        zip="$HOME/Code/backup/other/JetBrainsMono.zip"
        target="/usr/local/share/fonts/j"

        [[ -f "$zip" ]] || { echo "Font zip missing; skipping."; exit 0; }

        if [[ -d "$target" && -n "$(ls -A "$target" 2>/dev/null)" ]]; then
            echo "Already installed; skipping."
            exit 0
        fi

        sudo mkdir -p "$target"
        sudo bsdtar -xf "$zip" -C "$target"
        sudo find "$target" -type d -exec chmod 755 {} +
        sudo find "$target" -type f -exec chmod 644 {} +
        sudo fc-cache -fv 2>/dev/null || true
        """;

    public static void Register(ProcessRunner runner, Settings settings)
    {
        runner.Command(StepCategory.Fonts, "Install JetBrains Mono Nerd Font",
            "bash", ["-c", InstallScript],
            condition: () => settings.RunDefault);
    }
}
