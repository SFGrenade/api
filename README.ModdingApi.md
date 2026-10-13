# Hollow Knight Modding API

This development build targets Hollow Knight 1.5.12620 (Unity 6). Use a build that matches both the game version and the game process architecture. Compatibility with every existing mod has not been established.

## Installation

Close the game and copy this package's files into its `Managed` directory:

- macOS: `hollow_knight.app/Contents/Resources/Data/Managed`
- Windows and Linux: `hollow_knight_Data/Managed`

Place each mod in its own subdirectory of `Managed/Mods`. The title screen lists loaded mods and any initialization failures.

## Apple Silicon

Steam builds refuse to start when Steam initialization fails, and MonoMod 25.3.6 requires an x86-64 game process on macOS. Use the `osx-x64` package and launch through Rosetta with the app id environment variables set, keeping the Steam client running. Replace the application path below with the location of your installation:

```sh
SteamAppId=367520 SteamGameId=367520 arch -x86_64 "/path/to/hollow_knight.app/Contents/MacOS/Hollow Knight"
```

Launching the universal game normally, whether directly or through Steam, selects arm64, which this version of MonoMod does not support. A universal native dependency alone does not provide arm64 runtime hook support.

## Removal

Restore the original game files using Steam's **Properties → Installed Files → Verify integrity of game files**, or the equivalent repair command in your game client. This removes the API from the game assemblies; manually added mod files may remain in `Managed/Mods`.
