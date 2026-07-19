using HarmonyLib;

namespace GambleWithYourFriendsMod.Patches;

/// <summary>
/// Patches Harmony pour l'argent et les tickets.
///
/// ÉTAPES POUR COMPLÉTER :
/// 1. Dumper le jeu avec BepInEx + Il2CppDumper
/// 2. Trouver les classes type PlayerWallet, EconomyManager, CurrencyService...
/// 3. Remplacer les noms de type/méthode ci-dessous par les vrais symboles
/// 4. Décommenter les attributs [HarmonyPatch] et implémenter les prefixes/postfixes
/// </summary>
[HarmonyPatch]
public static class MoneyPatches
{
    /// <summary>
    /// Exemple de patch : empêcher la diminution de l'argent si InfiniteMoney est actif.
    /// À adapter une fois la méthode SpendMoney / RemoveCurrency identifiée.
    /// </summary>
    /*
    [HarmonyPatch(typeof(PlayerEconomy), nameof(PlayerEconomy.SpendMoney))]
    [HarmonyPrefix]
    public static bool SpendMoney_Prefix(ref bool __result)
    {
        if (!ModState.Instance.InfiniteMoney)
            return true; // exécuter la méthode originale

        __result = true; // simuler un paiement réussi sans déduire
        return false;    // skip original
    }
    */

    /// <summary>
    /// Exemple : forcer le solde à chaque lecture si argent infini.
    /// </summary>
    /*
    [HarmonyPatch(typeof(PlayerEconomy), "get_Balance")]
    [HarmonyPostfix]
    public static void GetBalance_Postfix(ref int __result)
    {
        if (ModState.Instance.InfiniteMoney)
            __result = ModState.Instance.MoneyAmountToSet;
    }
    */

    /// <summary>
    /// Patch factice pour que Harmony.PatchAll ne échoue pas sur une classe vide.
    /// Supprimer quand les vrais patches seront ajoutés.
    /// </summary>
    [HarmonyPatch(typeof(UnityEngine.Application), nameof(UnityEngine.Application.Quit))]
    [HarmonyPrefix]
    public static bool Quit_Prefix()
    {
        // Ne bloque jamais Quit — sert uniquement d'ancre Harmony au bootstrap.
        return true;
    }
}
