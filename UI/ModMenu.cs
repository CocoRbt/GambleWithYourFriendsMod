using System;
using GambleWithYourFriendsMod.Configuration;
using GambleWithYourFriendsMod.Core;
using GambleWithYourFriendsMod.UI.Categories;
using UnityEngine;

namespace GambleWithYourFriendsMod.UI;

/// <summary>
/// Comportement Unity responsable du rendu OnGUI du menu mod.
/// Attaché à un GameObject persistant créé par Plugin.cs.
/// </summary>
public class ModMenuBehaviour : MonoBehaviour
{
    /// <summary>Enregistrement IL2CPP requis pour les types custom MonoBehaviour.</summary>
    static ModMenuBehaviour()
    {
        Il2CppInterop.Runtime.Injection.ClassInjector.RegisterTypeInIl2Cpp<ModMenuBehaviour>();
    }

    public ModMenuBehaviour(IntPtr ptr) : base(ptr) { }

    private ModConfig _config = null!;
    private ModState _state = null!;
    private IMenuCategory[] _categories = Array.Empty<IMenuCategory>();
    private int _selectedCategoryIndex;
    private bool _menuVisible;
    private Rect _windowRect;
    private Vector2 _scrollPosition;

    /// <summary>Nombre de catégories disponibles.</summary>
    public static int CategoryCount => 6;

    public void Initialize(ModConfig config, ModState state)
    {
        _config = config;
        _state = state;
        _menuVisible = config.MenuVisibleOnStart.Value;
        _windowRect = new Rect(40f, 40f, config.MenuWidth.Value, config.MenuHeight.Value);

        _categories = new IMenuCategory[]
        {
            new PlayerCategory(state),
            new CasinoGamesCategory(state),
            new MoneyCategory(state),
            new ItemsCategory(state),
            new WorldCategory(state),
            new MiscCategory(config, state)
        };

        config.ApplyToState(state);
        MenuStyles.EnsureInitialized();
    }

    private void Update()
    {
        HandleInput();
        ApplyRuntimeEffects();
    }

    /// <summary>
    /// Gère les raccourcis clavier : toggle menu, save/load rapide.
    /// </summary>
    private void HandleInput()
    {
        if (KeybindHelper.WasPressedThisFrame(_config.MenuToggleKey.Value))
            _menuVisible = !_menuVisible;

        if (KeybindHelper.WasPressedThisFrame(_config.QuickSaveKey.Value))
            _config.SaveFromState(_state);

        if (KeybindHelper.WasPressedThisFrame(_config.QuickLoadKey.Value))
            _config.ApplyToState(_state);
    }

    /// <summary>
    /// Effets appliqués chaque frame (vitesse du jeu, etc.).
    /// Les patches Harmony gèrent le reste.
    /// </summary>
    private void ApplyRuntimeEffects()
    {
        if (_state.GameSpeedMultiplier > 0f && !_state.FreezeDayTimer)
            Time.timeScale = _state.GameSpeedMultiplier;
        else if (_state.FreezeDayTimer)
            Time.timeScale = 0f;
    }

    private void OnGUI()
    {
        if (_state.ShowDebugOverlay)
            DrawDebugOverlay();

        if (!_menuVisible)
            return;

        MenuStyles.EnsureInitialized();

        _windowRect = GUI.Window(
            unchecked((int)0x47575946), // ID fenêtre unique "GWYF"
            _windowRect,
            DrawMenuWindow,
            "🎰 Gamble With Your Friends — Mod Menu",
            MenuStyles.Window);
    }

    /// <summary>
    /// Contenu principal de la fenêtre : header, onglets, zone scrollable.
    /// </summary>
    private void DrawMenuWindow(int windowId)
    {
        GUILayout.BeginVertical();

        GUILayout.Label("Mod Menu v0.1", MenuStyles.Header);
        GUILayout.Space(4);

        DrawCategoryTabs();
        GUILayout.Space(6);

        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandHeight(true));
        _categories[_selectedCategoryIndex].Draw();
        GUILayout.EndScrollView();

        GUILayout.Space(4);
        GUILayout.Label($"F8/Insert : menu | F9 : save | F10 : load", MenuStyles.Footer);

        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 24));
    }

    /// <summary>
    /// Barre d'onglets horizontale pour naviguer entre les catégories.
    /// </summary>
    private void DrawCategoryTabs()
    {
        GUILayout.BeginHorizontal();
        for (var i = 0; i < _categories.Length; i++)
        {
            var style = i == _selectedCategoryIndex ? MenuStyles.TabActive : MenuStyles.TabInactive;
            if (GUILayout.Button(_categories[i].TabName, style))
                _selectedCategoryIndex = i;
        }
        GUILayout.EndHorizontal();
    }

    private void DrawDebugOverlay()
    {
        var fps = 1f / Mathf.Max(Time.unscaledDeltaTime, 0.0001f);
        GUI.Label(new Rect(10, 10, 400, 120),
            $"FPS: {fps:F0}\n" +
            $"GodMode: {_state.GodMode} | ForceWin: {_state.ForceWin}\n" +
            $"Infinite$: {_state.InfiniteMoney} | Speed: x{_state.GameSpeedMultiplier:F2}");
    }
}
