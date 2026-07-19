using HarmonyLib;

namespace GambleWithYourFriendsMod.Patches;

/// <summary>
/// Patches Harmony pour le RNG et les résultats des jeux de casino.
///
/// ÉTAPES POUR COMPLÉTER :
/// 1. Identifier Random.Range, UnityEngine.Random, ou un service RNG custom du jeu
/// 2. Trouver les méthodes CalculateOutcome / Spin / Deal pour chaque mini-jeu
/// 3. Brancher ForceWin et RngBiasPercent depuis ModState
/// </summary>
[HarmonyPatch]
public static class RngPatches
{
    /// <summary>
    /// Exemple : biaiser UnityEngine.Random.Range pour les machines à sous.
    /// </summary>
    /*
    [HarmonyPatch(typeof(UnityEngine.Random), nameof(UnityEngine.Random.Range), new[] { typeof(int), typeof(int) })]
    [HarmonyPostfix]
    public static void RandomRangeInt_Postfix(int min, int max, ref int __result)
    {
        var state = ModState.Instance;
        if (!state.ModifyRng && !state.ForceWin)
            return;

        if (state.ForceWin)
        {
            __result = max - 1; // toujours le meilleur index possible
            return;
        }

        // Biais probabiliste simple — à affiner par jeu
        if (UnityEngine.Random.value * 100f > state.RngBiasPercent)
            __result = min;
    }
    */

    /// <summary>
    /// Exemple : forcer le résultat d'une partie de blackjack.
    /// </summary>
    /*
    [HarmonyPatch(typeof(BlackjackTable), "ResolveRound")]
    [HarmonyPrefix]
    public static void BlackjackResolve_Prefix(ref RoundResult __result)
    {
        if (ModState.Instance.ForceWin)
            __result = RoundResult.PlayerWin;
    }
    */

    /// <summary>
    /// Ancre Harmony temporaire — remplacer par les vrais patches jeu.
    /// </summary>
    [HarmonyPatch(typeof(UnityEngine.Time), "get_deltaTime")]
    [HarmonyPostfix]
    public static void DeltaTime_Postfix(ref float __result)
    {
        // Pas de modification ici — hook neutre pour bootstrap.
        // Les patches gameplay seront ajoutés dans les prochaines itérations.
    }
}
