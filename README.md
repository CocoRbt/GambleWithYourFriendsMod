# Gamble With Your Friends — Mod Menu (BepInEx 5)

Mod menu pour **Gamble With Your Friends**, basé sur **BepInEx 5** + **HarmonyX**.

## Fonctionnalités (v0.1 — base)

- Menu OnGUI accessible avec **F8** (configurable, ex. Insert)
- 6 catégories : **Player**, **Casino**, **Money**, **Items**, **World**, **Misc**
- Système de configuration BepInEx (save/load F9/F10)
- Stubs Harmony prêts pour argent infini et modification RNG
- Commentaires en français dans tout le code

## Prérequis

1. [BepInEx 5 IL2CPP](https://docs.bepinex.dev/) installé dans le dossier du jeu
2. Lancer le jeu une fois pour générer `BepInEx/interop/` et `BepInEx/unity-libs/`
3. [.NET SDK](https://dotnet.microsoft.com/download) pour compiler le mod

## Installation rapide

```bash
# 1. Cloner / copier ce dossier
cd GambleWithYourFriendsMod

# 2. Définir le chemin du jeu
export GAME_DIR="/chemin/vers/Gamble With Your Friends"

# 3. Compiler
dotnet build -c Release

# 4. Copier la DLL
cp bin/Release/net472/GambleWithYourFriendsMod.dll "$GAME_DIR/BepInEx/plugins/"
```

## Structure du projet

```
GambleWithYourFriendsMod/
├── Plugin.cs                 # Point d'entrée BepInEx
├── Configuration/
│   └── ModConfig.cs          # Fichier .cfg BepInEx
├── Core/
│   ├── ModState.cs           # État runtime partagé
│   └── KeybindHelper.cs
├── UI/
│   ├── ModMenu.cs            # Rendu OnGUI + input
│   ├── MenuStyles.cs
│   └── Categories/           # Un fichier par onglet
└── Patches/
    ├── MoneyPatches.cs       # Harmony — argent / tickets
    └── RngPatches.cs         # Harmony — RNG / force win
```

## Configuration

Fichier généré : `BepInEx/config/com.modder.gamblewithyourfriends.cfg`

| Clé | Défaut | Description |
|-----|--------|-------------|
| `MenuToggleKey` | F8 | Ouvrir/fermer le menu |
| `QuickSaveKey` | F9 | Sauvegarde rapide |
| `QuickLoadKey` | F10 | Chargement rapide |

## Prochaines étapes (features à implémenter)

1. **Infinite Money / Tickets** — patcher `PlayerEconomy` (noms à confirmer via dump)
2. **Force Win / RNG** — hooks sur slots, roulette, plinko, blackjack
3. **Spawn items** — trouver le système d'inventaire / prefabs jetons
4. **God Mode + No Cooldown** — patch dégâts + timers d'abilités
5. **Game speed / day timer** — hook sur le gestionnaire de jour/nuit du casino
6. **Teleport** — positions des salles + API mouvement joueur

## Reverse engineering

```bash
# Après première exécution avec BepInEx :
# - Consulter BepInEx/LogOutput.log
# - Utiliser Il2CppDumper sur GameAssembly.dll
# - Chercher : Money, Wallet, Chip, Slot, Roulette, Random, DayManager
```

## Notes techniques

- Cible **IL2CPP** (`BepInEx.Unity.IL2CPP`). Pour une build Mono, remplacer par `BepInEx.Unity.Mono` et retirer l'enregistrement Il2Cpp du `ModMenuBehaviour`.
- Le menu utilise **Unity IMGUI (OnGUI)** — léger, sans dépendance ImGui.NET. Migration possible vers ImGui plus tard.
- Les patches actuels sont des **ancres bootstrap** ; remplacez-les par les vrais symboles du jeu.

## Licence

Usage personnel / éducatif. Respectez les conditions d'utilisation du jeu.
