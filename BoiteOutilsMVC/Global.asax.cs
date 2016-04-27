#region using
using BoiteOutilsMVC.Models.Filtres;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing; 
#endregion

namespace BoiteOutilsMVC
{
    // Remarque : pour obtenir des instructions sur l'activation du mode classique IIS6 ou IIS7, 
    // visitez http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        #region Application_Start (protected)
        /// <summary>
        /// Appelée au démarrage de l'application Web
        /// </summary>
        protected void Application_Start()
        {
            // => Nouvauté MVP 2.
            // Enregistre les areas qui sont un moyen de découper le projet en sous projets indépendants
            // C'est quoi et comment ça marche:
            // http://2010wave.blogspot.fr/2009/12/how-to-create-areas-in-aspnet-mvc.html
            AreaRegistration.RegisterAllAreas();

            // => Depuis MVC 4, le code global.asax est découpé en ces 4 classes alors qu'auparavant la totalité du code se trouvait ici.
            // Aller lire le code de ces classes dans App_Start
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // => ajouté ici pour les besoins d'une des démos sur les filtre
            // ne fait pas partie du code standard
            // voir /Models/Filtres
            FilterProviders.Providers.Add(new PerformanceTestFilterProvider());
        } 
        #endregion
    }
}