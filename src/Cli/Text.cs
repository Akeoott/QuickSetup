// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

namespace QuickSetup.Cli;

public static class Text
{
    internal const string Help = """

        [bold green]QuickSetup[/] — [dim]Configure once, forget later. Made for personal use.[/]
        [bold red]RUNNING THIS PROGRAM MAY BE DESTRUCTIVE![/]

        [bold aqua]USAGE[/]
            quicksetup [yellow][[OPTIONS]][/]

        [bold aqua]OPTIONS[/]
            [bold yellow]-m, --minimal[/]   Install CLI tools and core setup only.
            [bold yellow]-d, --default[/]   Install desktop apps, dependencies, and more. [dim]Includes --minimal.[/]
            [bold yellow]-D, --dev[/]       Configure Docker and install useful dev tools. [dim]Includes --minimal.[/]
            [bold yellow]-i, --info[/]      Explain QuickSetup and what each preset installs.
            [bold yellow]-h, --help[/]      Show this help.
        """;


    internal const string Info = """

        [bold green]QuickSetup[/] — [dim]Configure once, forget later. Made for personal use.[/]
        [bold red]RUNNING THIS PROGRAM MAY BE DESTRUCTIVE![/]


        [bold aqua]WHAT IS QUICKSETUP?[/]
            A personal tool that installs a list of packages,
            and symlinks preconfigured application configs in [yellow]~/.config[/].

        [bold aqua]PRESETS[/]
            [italic dim]All presets include Minimal. Default and Dev add more on top.[/]

            [bold green]Minimal[/] [bold yellow](--minimal)[/]:
            - Updates the system.
            - Installs core packages and programming languages.
            - Installs paru [dim](a better alternative to yay).[/]
            - Symlinks preconfigured configs into [yellow]~/.config[/].
            - Sets fish as the default shell.
            - Adds common CLI tools from the AUR,
              [dim]such as nano, bat, eza, and ripgrep.[/]

            [bold green]Default[/] [bold yellow](--default)[/] — [dim]Includes Minimal:[/]
            - Installs the JetBrains Mono Nerd Font globally.
            - Installs desktop apps such as VS Code, Spotify, Konsole, Dolphin, Discord, and more.
            - Installs Flatpak and FUSE for Flathub apps and AppImages.
            - Adds more common AUR packages.
            - Pulls in required dependencies,
              [dim]such as python-websockets, qt6-wayland, and qt6-websockets.[/]

            [bold green]Dev[/] [bold yellow](--dev)[/] — [dim]Includes Default and Minimal:[/]
            - Installs JetBrains Toolbox for managing JetBrains IDEs.
            - Optionally installs development tools,
              [dim]such as docker, appimage-builder, and webkit2gtk-4.1.[/]

        [bold aqua]REQUIREMENTS[/]
            - A minimal [bold green]Arch Linux[/] installation.
            - A stable internet connection.
            - [bold green]KDE Plasma 6[/] — packages and configs are tailored to it.

        [italic dim]Package lists are examples, not exhaustive. Presets may include additional packages.[/]
        """;

}
