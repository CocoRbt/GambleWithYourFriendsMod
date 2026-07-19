using GambleWithYourFriendsMod.Configuration;
using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie Misc : sauvegarde/chargement config, debug, infos mod.
/// </summary>
public sealed class MiscCategory : IMenuCategory
{
    private readonly ModConfig _config;
    private readonly ModState _state;

    public MiscCategory(ModConfig config, ModState state)
    {
        _config = config;
        _state = state;
    }

    public string TabName => "Misc";

    public void Draw()
    {
        GUILayout.Label("Configuration & divers", MenuStyles.Section);

        if (GUILayout.Button("Sauvegarder la configuration"))
        {
            _config.SaveFromState(_state);
            Plugin.Logger.LogInfo("[Misc] Configuration sauvegardée.");
        }

        if (GUILayout.Button("Charger la configuration"))
        {
            _config.ApplyToState(_state);
            Plugin.Logger.LogInfo("[Misc] Configuration chargée.");
        }

        if (GUILayout.Button("Réinitialiser toutes les options"))
        {
            _state.ResetToDefaults();
            Plugin.Logger.LogInfo("[Misc] État réinitialisé.");
        }

        GUILayout.Space(12);
        _state.ShowDebugOverlay = GUILayout.Toggle(_state.ShowDebugOverlay, " Overlay debug (FPS, état)");

        GUILayout.Space(12);
        GUILayout.Label("Informations", MenuStyles.Section);
        GUILayout.Label($"Mod : {Plugin.PluginName}");
        GUILayout.Label($"Version : {Plugin.PluginVersion}");
        GUILayout.Label($"Touche menu : {_config.MenuToggleKey.Value}");
        GUILayout.Label($"GUID : {Plugin.PluginGuid}");
    }
}
