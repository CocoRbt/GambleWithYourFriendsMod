using UnityEngine;

namespace GambleWithYourFriendsMod.Core;

/// <summary>
/// Utilitaires pour la détection des touches clavier.
/// </summary>
public static class KeybindHelper
{
    /// <summary>
    /// Vérifie si la touche configurée vient d'être pressée cette frame.
    /// </summary>
    public static bool WasPressedThisFrame(KeyCode key)
    {
        return key != KeyCode.None && Input.GetKeyDown(key);
    }

    /// <summary>
    /// Convertit une chaîne de config BepInEx en KeyCode Unity.
    /// </summary>
    public static KeyCode ParseKeyCode(string value, KeyCode fallback = KeyCode.F8)
    {
        if (string.IsNullOrWhiteSpace(value))
            return fallback;

        return System.Enum.TryParse(value, ignoreCase: true, out KeyCode parsed)
            ? parsed
            : fallback;
    }
}
