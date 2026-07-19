using GambleWithYourFriendsMod.Core;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Catégorie Money : argent infini, tickets, définition du solde.
/// </summary>
public sealed class MoneyCategory : IMenuCategory
{
    private readonly ModState _state;

    public MoneyCategory(ModState state) => _state = state;

    public string TabName => "Money";

    public void Draw()
    {
        GUILayout.Label("Argent & tickets", MenuStyles.Section);

        _state.InfiniteMoney = GUILayout.Toggle(_state.InfiniteMoney, " Argent infini");
        _state.InfiniteTickets = GUILayout.Toggle(_state.InfiniteTickets, " Tickets infinis");

        GUILayout.Space(8);
        GUILayout.Label($"Montant à définir : {_state.MoneyAmountToSet:N0}");
        var amountStr = GUILayout.TextField(_state.MoneyAmountToSet.ToString());
        if (int.TryParse(amountStr, out var parsed))
            _state.MoneyAmountToSet = Mathf.Max(0, parsed);

        GUILayout.Space(8);
        if (GUILayout.Button("Appliquer le montant maintenant"))
        {
            Plugin.Log.LogInfo($"[Money] Définir solde à {_state.MoneyAmountToSet} — patch à venir.");
        }

        if (GUILayout.Button("Ajouter 10 000"))
        {
            Plugin.Log.LogInfo("[Money] +10 000 — patch à venir.");
        }
    }
}
