// Copyright (c) Akeoot / Akeoott <akeoot@pm.me>. Licensed under the AGPL-3.0 Licence.
// See the LICENSE file in the repository root for full license text.

using Spectre.Console;

namespace QuickSetup;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var settings = CommandArgs.Init(args);
        Console.WriteLine(settings.SetupDefault);
        return 0;
    }
}
