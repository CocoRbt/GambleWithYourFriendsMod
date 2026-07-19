namespace GambleWithYourFriendsMod.UI.Categories;

/// <summary>
/// Contrat commun pour chaque onglet/catégorie du menu.
/// </summary>
public interface IMenuCategory
{
    /// <summary>Nom affiché dans l'onglet.</summary>
    string TabName { get; }

    /// <summary>Dessine le contenu de la catégorie.</summary>
    void Draw();
}
