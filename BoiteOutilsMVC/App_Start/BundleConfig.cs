#region using
using System.Web.Optimization; 
#endregion

namespace BoiteOutilsMVC
{
    /// <summary>
    /// Gestion du regroupement (bundleling)
    /// </summary>
    /// <remarks> =>
    /// Nouveauté MVC 4 qui propose des options de minification et de regroupement
    /// Minification: réduction de la taille des fichiers etdonc de la bande passante utilisée
    /// Regroupement: une page web est en général composée de plusieurs fichiers (html, css, js....)
    ///                 Les navigateurs ne les chargent pas tous simultanément, mais par blocs (9 pour IE).
    ///                 Dans le cas d'un grand nombre de fichiers il peut être avantageux de les regrouper dans des blocs pour les charger ensembles
    ///                 C'est ce que fait l'option de regroupement
    /// 
    /// Pour en savoir plus:
    /// http://rdonfack.developpez.com/tutoriels/dotnet/asp-net-mvc-optimiser-temps-chargement-page-utilisant-regroupement-et-minification/
    /// http://www.davidhayden.me/blog/asp.net-mvc-4-bundling-and-minification
    /// </remarks>
    public class BundleConfig
    {
        // Pour plus d’informations sur le Bundling, accédez à l’adresse http://go.microsoft.com/fwlink/?LinkId=254725 (en anglais)
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

        }
    }
}