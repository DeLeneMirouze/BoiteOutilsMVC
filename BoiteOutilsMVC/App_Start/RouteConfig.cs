#region using
using System.Web.Mvc;
using System.Web.Routing; 
#endregion

namespace BoiteOutilsMVC
{
    /// <summary>
    /// Configuration des routes
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // on ignore les routes qui portent sur des fichiers de ressource *.axd
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // route par défaut
            // le contrôleur par défaut est HomeController
            // L'action par défaut est Index
            // L'Id est optionnel
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}