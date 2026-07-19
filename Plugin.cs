using System;
using BepInEx;
using BepInEx.Logging;
using GambleWithYourFriendsMod.Configuration;
using GambleWithYourFriendsMod.Core;
using GambleWithYourFriendsMod.Patches;
using GambleWithYourFriendsMod.UI;
using HarmonyLib;
using UnityEngine;

namespace GambleWithYourFriendsMod;

/// <summary>
/// Point d'entrée principal du mod — BepInEx 5 (Unity Mono).
/// </summary>
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGuid = "com.modder.gamblewithyourfriends";
    public const string PluginName = "Gamble With Your Friends Mod Menu";
    public const string PluginVersion = "0.1.0";

    internal static new ManualLogSource Logger { get; private set; } = null!;
    /// <summary>Config du mod (ne pas confondre avec base.Config = fichier BepInEx).</summary>
    internal static ModConfig ModSettings { get; private set; } = null!;
    internal static ModState State { get; private set; } = null!;
    internal static Harmony Harmony { get; private set; } = null!;

    private ModMenuBehaviour _menuBehaviour;

    private void Awake()
    {
        Logger = base.Logger;
        ModSettings = ModConfig.Load(Config);
        State = ModState.Instance;
        Harmony = new Harmony(PluginGuid);

        Logger.LogInfo($"{PluginName} v{PluginVersion} — chargement...");

        ApplyHarmonyPatches();
        RegisterMenuBehaviour();

        Logger.LogInfo($"Menu : touche {ModSettings.MenuToggleKey.Value} | Catégories : {ModMenuBehaviour.CategoryCount}");
    }

    /// <summary>
    /// Applique les patches Harmony (stubs pour l'instant).
    /// </summary>
    private void ApplyHarmonyPatches()
    {
        try
        {
            Harmony.PatchAll(typeof(MoneyPatches));
            Harmony.PatchAll(typeof(RngPatches));
            Logger.LogInfo("Patches Harmony appliqués.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"Échec des patches Harmony : {ex}");
        }
    }

    /// <summary>
    /// Crée un GameObject persistant pour le menu OnGUI.
    /// </summary>
    private void RegisterMenuBehaviour()
    {
        var menuObject = new GameObject("GWYF_ModMenu");
        DontDestroyOnLoad(menuObject);
        _menuBehaviour = menuObject.AddComponent<ModMenuBehaviour>();
        _menuBehaviour.Initialize(ModSettings, State);
    }

    private void OnDestroy()
    {
        ModSettings?.Save();
        Harmony?.UnpatchSelf();
        Logger.LogInfo("Mod déchargé, configuration sauvegardée.");
    }
}
