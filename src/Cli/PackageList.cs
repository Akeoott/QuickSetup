// SPDX-FileCopyrightText: 2026-present Akeoot <akeoot@pm.me>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace QuickSetup.Cli;

public static class PackageList
{
    internal static readonly string[] Minimal = [
        "bat", "btop",
        "eza", "expac",
        "fish",
        "hwinfo", "man-db",
        "nano", "nodejs", "npm",
        "ripgrep",
        "starship"
    ];

    internal static readonly string[] Default = [
        "ark",
        "brave",
        "cava",
        "discord", "dolphin",
        "fastfetch", "flatpak", "fuse2", "fuse3",
        "gwenview",
        "konsole",
        "python-websockets",
        "qt6-wayland", "qt6-websockets",
        "spotify-edge", "steam",
        "vlc", "vlc-plugin-ffmpeg","vlc-plugin-smb",
        "vlc-plugin-x264","vlc-plugin-x265"
    ];

    internal static readonly string[] Dev = [
        "appimage-builder",
        "docker",
        "linuxdeploy", "linuxdeploy-plugin-qt",
        "jetbrains-toolbox",
        "webkit2gtk-4.1"
    ];
}
