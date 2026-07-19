using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using GambleWithYourFriendsMod.Configuration;
using GambleWithYourFriendsMod.Core;
using GambleWithYourFriendsMod.Patches;
using GambleWithYourFriendsMod.UI;
using HarmonyLib;
using UnityEngine;

namespace GambleWithYourFriendsMod;

/// <summary>
/// Point d'entrée principal du mod BepInEx.
/// Charge la configuration, applique les patches Harmony et affiche le menu.
/// </summary>
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BasePlugin
{
    public const string PluginGuid = "com.modder.gamblewithyourfriends";
    public const string PluginName = "Gamble With Your Friends Mod Menu";
    public const string PluginVersion = "0.1.0";

    internal static new ManualLogSource Log { get; private set; } = null!;
    internal static ModConfig Config { get; private set; } = null!;
    internal static ModState State { get; private set; } = null!;
    internal static Harmony Harmony { get; private set; } = null!;

    private ModMenuBehaviour? _menuBehaviour;

    public override void Load()
    {
        Log = base.Log;
        Config = ModConfig.Load(Config);
        State = ModState.Instance;
        Harmony = new Harmony(PluginGuid);

        Log.LogInfo($"{PluginName} v{PluginVersion} — chargement...");

        ApplyHarmonyPatches();
        RegisterMenuBehaviour();

        Log.LogInfo($"Menu : touche {Config.MenuToggleKey.Value} | Catégories : {ModMenu.CategoryCount}");
    }

    /// <summary>
    /// Applique tous les patches Harmony définis dans le namespace Patches.
    /// Les patches sont des stubs pour l'instant — à compléter après reverse du jeu.
    /// </summary>
    private void ApplyHarmonyPatches()
    {
        try
        {
            Harmony.PatchAll(typeof(MoneyPatches));
            Harmony.PatchAll(typeof(RngPatches));
            Log.LogInfo("Patches Harmony appliqués.");
        }
        catch (Exception ex)
        {
            Log.LogError($"Échec des patches Harmony : {ex}");
        }
    }

    /// <summary>
    /// Crée un GameObject persistant qui gère l'affichage OnGUI du menu.
    /// </summary>
    private void RegisterMenuBehaviour()
    {
        var menuObject = new GameObject("GWYF_ModMenu");
        UnityEngine.Object.DontDestroyOnLoad(menuObject);
        _menuBehaviour = menuObject.AddComponent<ModMenuBehaviour>();
        _menuBehaviour.Initialize(Config, State);
    }

    /// <summary>
    /// Sauvegarde la configuration à la fermeture du jeu.
    /// </summary>
    private void OnDestroy()
    {
        Config.Save();
        Harmony?.UnpatchSelf();
        Log.LogInfo("Mod déchargé, configuration sauvegardée.");
    }
}
