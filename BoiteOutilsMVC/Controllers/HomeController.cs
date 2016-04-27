#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoiteOutilsMVC.Domaine; 
#endregion

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// Contrôleur par défaut.
    /// Ce contrôleur sert également à démontrer quelques mécanismes de base d'interaction entre le contrôleur et la vue
    /// </summary>
    /// <remarks>
    /// Il sera intéressant de lire ceci:
    /// http://www.rachelappel.com/when-to-use-viewbag-viewdata-or-tempdata-in-asp.net-mvc-3-applications
    /// </remarks>
    public sealed class HomeController : Controller
    {
        #region => Introduction
        // Normalement une action retourne une instance de ActionResult.
        // Toutefois ce n'est pas obligatoire et en pratique virtuellement n'importe quel type pourrait être retourné, 
        // y compris void (qui n'est pas un type ceci étant!).
        //
        // Le framework fournit des implémentations spécialisées d'ActionResult, comme:
        // ContentResult, EmptyResult, FileResult, JsonResult...
        //
        // Par exemple:
        // EmptyResult est l'action renvoyée vers la vue par le framework si la méthode d'action est void ou Null.
        // Si le type de retour est Object, String, Int..., le framework encapsule le ToString du type de retour dans un ContentResult
        // ...
        // Ce contrôleur n'a pas d'autres ambitions que démontrer concrètement quelques mécanismes de base d'interaction entre le 
        // contrôleur et la vue
        #endregion

        #region Index (ViewBag)
        // Verbe GET (valeur par défaut)
        // Url: /Home


        /// <summary>
        /// Action par défaut (démonstration de ViewBag)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // une façon de remonter des infos dans la vue
            // ViewBag est une propriété dynamic, il s’agit d’une des nouveautés de C# 4.0.
            // Nous ne conseillons pas l'emploi de ViewBag qui offre de nombreux inconvénients
            // http://completedevelopment.blogspot.fr/2011/12/stop-using-viewbag-in-most-places.html
            ViewBag.Message = "Démo ViewBag";

            // => retourne la vue par défaut associée à cette action: /Views/Home/Index.xxx
            // Une surcharge de View() permet de préciser le nom d'une autre vue
            return View();
        }
        #endregion

        #region DemoViewData
        // Verbe GET (valeur par défaut)
        // Url: /Home/DemoViewData


        /// <summary>
        /// Démonstration de ViewData
        /// </summary>
        /// <returns></returns>
        public ActionResult DemoViewData()
        {
            var couleurs = new List<string> 
            { "Bleu", 
                "Blanc", 
                "Rouge" };

            ViewData["Couleurs"] = couleurs;
            return View();
        }
        #endregion

        #region DemoStringReturn
        // Verbe GET (valeur par défaut)
        // Url: /Home/DemoStringReturn


        /// <summary>
        /// Démonstration d'une vue qui retourne une String directement
        /// </summary>
        /// <returns></returns>
        public string DemoStringReturn()
        {
            // => Comme vous le constaterez en regardant le source du rendu de la vue
            // celui-ci sera constitué de la seule chaîne
            // d'un point de ve sécurité il serait d'ailleurs plus prudent d'écrire ceci:
            // 
            // return HttpUtility.HtmlEncode("Une chaîne a été retournée");

            return "Une chaîne a été retournée";

            // => ce type de méthode d'action se prête bien à retourner certaines ressources 
            // que l'on souhaite afficher telle quel comme un fichier, une image...
            // Dans ce cas il est possible que l'utilisation de ContentResult soit plus intéressant
            // afin de passer des paramétrages d'encodage ou de type MIME.
            // Voyez la méthode DemoStringContentResult
            //
            // d'une façon générale on utilise ContentReult toutes les fois où le message renvoyé par l'action constitue la vue elle-même 
            // (download de fichier, requête Ajax, Web WebApi...)
        }
        #endregion

        #region DemoStringContentResult
        // Verbe GET (valeur par défaut)
        // Url: /Home/DemoStringContentResult

        /// <summary>
        /// Démonstration de l'utilisation de ContentResult
        /// </summary>
        /// <returns></returns>
        public ActionResult DemoStringContentResult()
        {
            // => Rapprochez cette méthode de DemoStringReturn
            // Le comportement est identique
            // 
            // Observez les différentes surcharges de Content

            return Content("Une chaîne a été retournée!");

            // => on pourrait aussi
            //return new ContentResult() { Content = "Une chaîne a été retournée!" };
        } 
        #endregion

        #region DemoStringInView
        // Verbe GET (valeur par défaut)
        // Url: /Home/DemoStringInView

        /// <summary>
        /// Retourner des donnés via la vue
        /// </summary>
        /// <returns></returns>
        public ActionResult DemoStringInView()
        {
            // => Rapprochez cette méthode de DemoStringReturn ou DemoStringContentResult
            // Le comportement est totalement différent
            // 
            // Il est important de typer la chaîne parce que sinon MVC rechercherait une vue portant le nom de 
            // "Bonjour je suis une chaîne de caractères" ce qui ne marcherait évidemment pas.
            return View((object)"Bonjour je suis une chaîne de caractères");
        }
        #endregion

        #region DemoTypedView
        // Url: /Home/DemoTypedView

        /// <summary>
        /// Démonstration d'une vue fortement typée
        /// </summary>
        /// <returns></returns>
        public ActionResult DemoTypedView()
        {
            // => Si la vue est construite avec l'assistant Visual Studio, il faudra au préalable compiler le projet pour que la classe
            // Animal soit visible dans les types du projet

            Animal animal = new Animal() { Nom = "Chien", Famille = "Mammifère"};
            return View(animal);
        } 
        #endregion

        #region GoTo
        // /Home/GoTo

        /// <summary>
        /// Démonstration d'un ActionResult de type HttpStatusCodeResult
        /// </summary>
        /// <returns></returns>
        public ActionResult GoTo()
        {
            // => HttpNotFoundResult est une classe dérivée de HttpStatusCodeResult qui permet de créer une vue avec
            // un code de retour Http personnalisé
            // On trouve aussi HttpUnauthorizedResult (401), RedirectResult et RedirectToRouteResult (302)

            return new HttpNotFoundResult("Page non trouvée");
        } 
        #endregion
    }
}
