using BepInEx.Configuration;
using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.Configuration;

/// <summary>
/// Gestion centralisée du fichier de configuration BepInEx.
/// Fichier généré : BepInEx/config/com.modder.gamblewithyourfriends.cfg
/// </summary>
public sealed class ModConfig
{
    // ── Général ─────────────────────────────────────────────
    public ConfigEntry<KeyCode> MenuToggleKey { get; }
    public ConfigEntry<int> MenuWidth { get; }
    public ConfigEntry<int> MenuHeight { get; }
    public ConfigEntry<bool> MenuVisibleOnStart { get; }

    // ── Raccourcis optionnels ───────────────────────────────
    public ConfigEntry<KeyCode> QuickSaveKey { get; }
    public ConfigEntry<KeyCode> QuickLoadKey { get; }

    // ── Valeurs persistées des features ─────────────────────
    public ConfigEntry<bool> SavedGodMode { get; }
    public ConfigEntry<bool> SavedInfiniteMoney { get; }
    public ConfigEntry<bool> SavedForceWin { get; }
    public ConfigEntry<float> SavedGameSpeed { get; }

    private readonly ConfigFile _configFile;

    private ModConfig(ConfigFile configFile)
    {
        _configFile = configFile;

        MenuToggleKey = configFile.Bind(
            "Général",
            "MenuToggleKey",
            KeyCode.F8,
            "Touche pour ouvrir/fermer le menu mod (ex: F8, Insert)");

        MenuWidth = configFile.Bind("Général", "MenuWidth", 520, "Largeur du menu en pixels");
        MenuHeight = configFile.Bind("Général", "MenuHeight", 640, "Hauteur du menu en pixels");
        MenuVisibleOnStart = configFile.Bind("Général", "MenuVisibleOnStart", false, "Afficher le menu au démarrage");

        QuickSaveKey = configFile.Bind("Général", "QuickSaveKey", KeyCode.F9, "Raccourci sauvegarde rapide de la config");
        QuickLoadKey = configFile.Bind("Général", "QuickLoadKey", KeyCode.F10, "Raccourci chargement rapide de la config");

        SavedGodMode = configFile.Bind("État sauvegardé", "GodMode", false, "Dernière valeur God Mode");
        SavedInfiniteMoney = configFile.Bind("État sauvegardé", "InfiniteMoney", false, "Dernière valeur argent infini");
        SavedForceWin = configFile.Bind("État sauvegardé", "ForceWin", false, "Dernière valeur force win");
        SavedGameSpeed = configFile.Bind("État sauvegardé", "GameSpeed", 1f, "Dernier multiplicateur de vitesse");
    }

  public static ModConfig Load(ConfigFile configFile)
    {
        return new ModConfig(configFile);
    }

    /// <summary>
    /// Applique les valeurs sauvegardées dans le fichier vers l'état runtime.
    /// </summary>
    public void ApplyToState(ModState state)
    {
        state.GodMode = SavedGodMode.Value;
        state.InfiniteMoney = SavedInfiniteMoney.Value;
        state.ForceWin = SavedForceWin.Value;
        state.GameSpeedMultiplier = SavedGameSpeed.Value;
    }

    /// <summary>
    /// Écrit l'état runtime actuel dans le fichier de configuration.
    /// </summary>
    public void SaveFromState(ModState state)
    {
        SavedGodMode.Value = state.GodMode;
        SavedInfiniteMoney.Value = state.InfiniteMoney;
        SavedForceWin.Value = state.ForceWin;
        SavedGameSpeed.Value = state.GameSpeedMultiplier;
        Save();
    }

    public void Save()
    {
        _configFile.Save();
    }
}
