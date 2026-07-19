using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie Player : god mode, cooldown, déplacement.
/// </summary>
public sealed class PlayerCategory : IMenuCategory
{
    private readonly ModState _state;

    public PlayerCategory(ModState state) => _state = state;

    public string TabName => "Player";

    public void Draw()
    {
        GUILayout.Label("Options joueur", MenuStyles.Section);

        _state.GodMode = GUILayout.Toggle(_state.GodMode, " God Mode (invincibilité)");
        _state.NoCooldown = GUILayout.Toggle(_state.NoCooldown, " No Cooldown (pas de temps d'attente)");
        _state.NoClip = GUILayout.Toggle(_state.NoClip, " No Clip (traverser les murs)");

        GUILayout.Space(8);
        GUILayout.Label($"Vitesse de déplacement : x{_state.MovementSpeedMultiplier:F1}");
        _state.MovementSpeedMultiplier = GUILayout.HorizontalSlider(_state.MovementSpeedMultiplier, 0.5f, 5f);

        GUILayout.Space(12);
        GUILayout.Label("Téléportation (à implémenter)", MenuStyles.Section);
        GUILayout.Label("Les coordonnées et points de téléportation seront ajoutés après analyse du jeu.");

        if (GUILayout.Button("Téléporter au spawn"))
        {
            Plugin.Logger.LogInfo("[Player] Téléportation spawn — patch à venir.");
        }
    }
}
