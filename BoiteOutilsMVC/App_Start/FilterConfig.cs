#region using
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC
{
    /// <summary>
    /// Enregistre les filtres globaux (qui s'appliquent à toutes les actions de tous les contrôleurs)
    /// Le filtrage global des requêtes sont une nouveauté MVC 3, mais les filtres existent depuis la version 1.0
    /// </summary>
    /// <remarks>
    /// Pour en savoir plus:
    /// http://odetocode.com/Blogs/scott/archive/2011/01/15/configurable-global-action-filters-for-asp-net-mvc.aspx
    /// 
    /// // aller aussi dans /Models/Filtres/LisezMoi.txt
    /// </remarks>
    public class FilterConfig
    {
        /// <summary>
        /// Enregistre les filtres globaux
        /// </summary>
        /// <param name="filters">Collection des filtres globaux</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Ajout du filtre HandleErrorAttribute à toute les actions de tous les contrôleurs avec ses paramètres par défaut
            // il réagit sur toutes les actions
            // il redirige vers la vue appelée Error.cshtml (ou Error.aspx si pas en Razor)
            filters.Add(new HandleErrorAttribute());
        }
    }
}