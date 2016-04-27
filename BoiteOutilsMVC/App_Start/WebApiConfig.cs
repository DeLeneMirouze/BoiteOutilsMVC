#region using
using System.Web.Http; 
#endregion

namespace BoiteOutilsMVC
{
    /// <summary>
    /// Route ASP.NET Web API
    /// <remarks>
    /// Nouvelle brique livrée avec .Net 4.5, mais utilisable en .Net 4.
    /// 
    /// Il s’agit d’un Framework qui permet de développer rapidement et simplement des services HTTP pour la mise à disposition 
    /// de données cross-plateforme et le développement d’application RESTful. Les Web API ne sont en rien une exclusivité des applications Web.
    /// Pour en savoir plus:
    /// 
    /// http://www.juliencorioland.net/Archives/aspnet-mvc-premiers-pas-avec-les-aspnet-web-api---partie-1
    /// http://www.juliencorioland.net/Archives/aspnet-mvc-premiers-pas-avec-les-aspnet-web-api---partie-2
    /// 
    /// http://net.tutsplus.com/tutorials/building-an-asp-net-mvc4-application-with-ef-and-webapi/
    /// </remarks>
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // route par défaut des services
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
