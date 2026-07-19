using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie World : vitesse du jeu, timer du jour.
/// </summary>
public sealed class WorldCategory : IMenuCategory
{
    private readonly ModState _state;

    public WorldCategory(ModState state) => _state = state;

    public string TabName => "World";

    public void Draw()
    {
        GUILayout.Label("Monde & temps", MenuStyles.Section);

        GUILayout.Label($"Vitesse du jeu : x{_state.GameSpeedMultiplier:F2}");
        _state.GameSpeedMultiplier = GUILayout.HorizontalSlider(_state.GameSpeedMultiplier, 0.1f, 5f);

        _state.FreezeDayTimer = GUILayout.Toggle(_state.FreezeDayTimer, " Geler le timer du jour");

        if (!_state.FreezeDayTimer)
        {
            GUILayout.Space(4);
            GUILayout.Label($"Override timer jour : {(_state.DayTimerOverride < 0 ? "auto" : _state.DayTimerOverride.ToString("F0"))}");
            _state.DayTimerOverride = GUILayout.HorizontalSlider(_state.DayTimerOverride, -1f, 300f);
        }

        GUILayout.Space(8);
        if (GUILayout.Button("Réinitialiser Time.timeScale"))
        {
            Time.timeScale = 1f;
            _state.GameSpeedMultiplier = 1f;
        }
    }
}
