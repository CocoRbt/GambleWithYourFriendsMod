using UnityEngine;

namespace GambleWithYourFriendsMod.UI;

/// <summary>
/// Styles visuels partagés pour le menu OnGUI.
/// Palette sombre type "mod menu" casino.
/// </summary>
public static class MenuStyles
{
    private static bool _initialized;
    private static GUIStyle _windowStyle = null!;
    private static GUIStyle _headerStyle = null!;
    private static GUIStyle _tabActiveStyle = null!;
    private static GUIStyle _tabInactiveStyle = null!;
    private static GUIStyle _sectionStyle = null!;
    private static GUIStyle _footerStyle = null!;

    public static GUIStyle Window => _windowStyle;
    public static GUIStyle Header => _headerStyle;
    public static GUIStyle TabActive => _tabActiveStyle;
    public static GUIStyle TabInactive => _tabInactiveStyle;
    public static GUIStyle Section => _sectionStyle;
    public static GUIStyle Footer => _footerStyle;

    /// <summary>
    /// Initialise les styles une seule fois (requis par Unity IMGUI).
    /// </summary>
    public static void EnsureInitialized()
    {
        if (_initialized) return;

        _windowStyle = new GUIStyle(GUI.skin.window)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.UpperCenter
        };

        _headerStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = new Color(1f, 0.85f, 0.2f) }
        };

        _tabActiveStyle = new GUIStyle(GUI.skin.button)
        {
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white, background = MakeTexture(2, 2, new Color(0.2f, 0.55f, 0.35f)) }
        };

        _tabInactiveStyle = new GUIStyle(GUI.skin.button)
        {
            normal = { textColor = new Color(0.85f, 0.85f, 0.85f) }
        };

        _sectionStyle = new GUIStyle(GUI.skin.box)
        {
            padding = new RectOffset(10, 10, 8, 8),
            margin = new RectOffset(4, 4, 4, 4)
        };

        _footerStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 11,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = new Color(0.6f, 0.6f, 0.6f) }
        };

        _initialized = true;
    }

    private static Texture2D MakeTexture(int width, int height, Color color)
    {
        var pixels = new Color[width * height];
        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        var texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
