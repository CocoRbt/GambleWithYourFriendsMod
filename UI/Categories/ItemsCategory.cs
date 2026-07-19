using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie Items : spawn de jetons et objets.
/// </summary>
public sealed class ItemsCategory : IMenuCategory
{
    private readonly ModState _state;

    public ItemsCategory(ModState state) => _state = state;

    public string TabName => "Items";

    public void Draw()
    {
        GUILayout.Label("Spawn d'objets", MenuStyles.Section);

        GUILayout.Label($"Quantité de jetons : {_state.ChipSpawnAmount}");
        _state.ChipSpawnAmount = Mathf.RoundToInt(
            GUILayout.HorizontalSlider(_state.ChipSpawnAmount, 1f, 10_000f));

        if (GUILayout.Button($"Spawn {_state.ChipSpawnAmount} jetons casino"))
        {
            Plugin.Log.LogInfo($"[Items] Spawn {_state.ChipSpawnAmount} jetons — patch à venir.");
        }

        GUILayout.Space(12);
        GUILayout.Label("Spawn par ID (nom de prefab / item)", MenuStyles.Section);
        _state.ItemSpawnId = GUILayout.TextField(_state.ItemSpawnId, GUILayout.Height(24));

        if (GUILayout.Button("Spawn item par ID"))
        {
            Plugin.Log.LogInfo($"[Items] Spawn item '{_state.ItemSpawnId}' — patch à venir.");
        }
    }
}
