# Gamble With Your Friends — Mod Menu (BepInEx 5)

Mod menu pour **Gamble With Your Friends**, basé sur **BepInEx 5** (Unity Mono) + Harmony.

## Fonctionnalités (v0.1)

- Menu OnGUI avec **F8**
- Catégories : Player, Casino, Money, Items, World, Misc
- Save/Load config (F9 / F10)
- Stubs Harmony (argent, RNG)

## Prérequis

1. [BepInEx 5.x **Unity Mono x64**](https://github.com/BepInEx/BepInEx/releases) dans le dossier du jeu
2. [.NET SDK](https://dotnet.microsoft.com/download)

Vérifie que le jeu est bien Mono : dossier `*_Data\Managed\` avec `Assembly-CSharp.dll` (pas seulement `GameAssembly.dll`).

## Build (PowerShell)

```powershell
cd $env:USERPROFILE\Documents\GambleWithYourFriendsMod
git pull
$env:GAME_DIR = "E:\SteamLibrary\steamapps\common\Gamble With Your Friends"
dotnet build -c Release
Copy-Item "bin\Release\net472\GambleWithYourFriendsMod.dll" "$env:GAME_DIR\BepInEx\plugins\"
```

## Config

`BepInEx/config/com.modder.gamblewithyourfriends.cfg`
