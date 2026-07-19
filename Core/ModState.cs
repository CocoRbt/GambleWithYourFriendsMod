namespace GambleWithYourFriendsMod.Core;

/// <summary>
/// État runtime partagé entre le menu UI et les patches Harmony.
/// Toutes les options activables du mod sont centralisées ici.
/// </summary>
public sealed class ModState
{
    public static ModState Instance { get; } = new();

    // ── Player ──────────────────────────────────────────────
    public bool GodMode { get; set; }
    public bool NoCooldown { get; set; }
    public float MovementSpeedMultiplier { get; set; } = 1f;
    public bool NoClip { get; set; }

    // ── Casino Games ────────────────────────────────────────
    public bool ForceWin { get; set; }
    public bool ModifyRng { get; set; }
    public int RngBiasPercent { get; set; } = 100;

    // ── Money ───────────────────────────────────────────────
    public bool InfiniteMoney { get; set; }
    public bool InfiniteTickets { get; set; }
    public int MoneyAmountToSet { get; set; } = 999_999;

    // ── Items ───────────────────────────────────────────────
    public int ChipSpawnAmount { get; set; } = 100;
    public string ItemSpawnId { get; set; } = string.Empty;

    // ── World ───────────────────────────────────────────────
    public float GameSpeedMultiplier { get; set; } = 1f;
    public bool FreezeDayTimer { get; set; }
    public float DayTimerOverride { get; set; } = -1f;

    // ── Misc ────────────────────────────────────────────────
    public bool ShowDebugOverlay { get; set; }

    /// <summary>
    /// Réinitialise toutes les options à leurs valeurs par défaut.
    /// </summary>
    public void ResetToDefaults()
    {
        GodMode = false;
        NoCooldown = false;
        MovementSpeedMultiplier = 1f;
        NoClip = false;

        ForceWin = false;
        ModifyRng = false;
        RngBiasPercent = 100;

        InfiniteMoney = false;
        InfiniteTickets = false;
        MoneyAmountToSet = 999_999;

        ChipSpawnAmount = 100;
        ItemSpawnId = string.Empty;

        GameSpeedMultiplier = 1f;
        FreezeDayTimer = false;
        DayTimerOverride = -1f;

        ShowDebugOverlay = false;
    }
}
