using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie Casino Games : force win, modification RNG.
/// </summary>
public sealed class CasinoGamesCategory : IMenuCategory
{
    private readonly ModState _state;

    public CasinoGamesCategory(ModState state) => _state = state;

    public string TabName => "Casino";

    public void Draw()
    {
        GUILayout.Label("Machines & jeux de casino", MenuStyles.Section);

        _state.ForceWin = GUILayout.Toggle(_state.ForceWin, " Force Win (gagner à chaque partie)");
        _state.ModifyRng = GUILayout.Toggle(_state.ModifyRng, " Modifier le RNG");

        if (_state.ModifyRng)
        {
            GUILayout.Space(4);
            GUILayout.Label($"Biais RNG : {_state.RngBiasPercent}%");
            _state.RngBiasPercent = Mathf.RoundToInt(
                GUILayout.HorizontalSlider(_state.RngBiasPercent, 0f, 100f));
        }

        GUILayout.Space(12);
        GUILayout.Label("Jeux ciblés par les patches Harmony :", MenuStyles.Section);
        GUILayout.Label("• Slots");
        GUILayout.Label("• Roulette");
        GUILayout.Label("• Plinko");
        GUILayout.Label("• Blackjack");
        GUILayout.Label("• (autres — à découvrir via reverse)");
    }
}
