Hollow Knight Modding API
=========================
![build](https://github.com/hk-modding/api/actions/workflows/build.yaml/badge.svg)
![docs](https://github.com/hk-modding/api/actions/workflows/docs.yaml/badge.svg)
![GitHub all releases](https://img.shields.io/github/downloads/hk-modding/api/total)

A Hollow Knight Modding API/loader for Hollow Knight 1.5.12620 (Unity 6). Uses [MonoMod](https://github.com/MonoMod/MonoMod).

This branch builds on the [Unity 6 port](https://github.com/hk-modding/api/pull/164) and is under development. Existing mods may require changes for Unity 6.

If you're a mod developer, there are [examples](https://github.com/hk-modding/api/tree/master/Examples) in the repo.

Documentation can be found [here](https://hk-modding.github.io/api/).

Build
=======

Install the .NET SDK (8.0 or later). On macOS and Linux, also install Mono and make sure `mono` is on `PATH`; the build runs .NET Framework patching tools through Mono.

Copy the `Managed` folder from a matching, unmodified Hollow Knight installation into the repository root as `Vanilla`. On macOS this folder is inside `hollow_knight.app/Contents/Resources/Data/Managed`.

Build from the repository root, choosing the runtime for the game process:

```sh
dotnet build Assembly-CSharp --configuration Release --runtime osx-x64
```

Use `win-x64` on Windows or `linux-x64` on Linux. The project builds its patching tools and dependencies automatically and writes the installable files to `OutputFinal`.

The patcher disables method inlining in modified game assemblies so runtime hooks can observe calls reliably.

On Apple Silicon, use `osx-x64` and run the game through Rosetta for compatibility testing. MonoMod 25.3.6 does not support native `osx-arm64` runtime detours. The native dependency packager can select arm64 files, and CI produces universal macOS libraries, but neither establishes native arm64 mod compatibility.

Builds leave the game installation untouched unless `CopyDir` is explicitly supplied. To install the result, close the game and copy the contents of `OutputFinal` into its `Managed` folder, or build with `-p:CopyDir="/absolute/path/to/Managed"`.

The GitHub Actions workflow needs access to the private `hk-modding/hk-binary-archives` repository through `BINARY_ARCHIVE_DEPLOY_KEY`. Local builds use the supplied `Vanilla` folder.

The [hook generator regression tests](Tests/HookGen/README.md) run without game files.

License
=======
Distributed under the MIT [license](https://github.com/hk-modding/api/blob/master/LICENSE).
