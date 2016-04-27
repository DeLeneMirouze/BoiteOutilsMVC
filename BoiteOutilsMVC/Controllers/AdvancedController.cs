#region using
using System;
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// Utilisation du cache de sortie et des actions enfants
    /// </summary>
    public sealed class AdvancedController : Controller
    {
        /* =>
         * Les vues partielles sont utiles pour factoriser des morceaux de vue ou bien pour appeler sur une page des 
         * actions qui viennent de contrôleurs différents
         * */

        #region Démo cache
        //=> visitez le code des vues CurrentTime et Display
        // http://www.c-sharpcorner.com/uploadfile/sanks/whats-new-in-Asp-Net-mvc-3-controllers-part-1/
        // la vue partielle CurrentTime est mise en cache dans cet exemple

        // => Notez l'utilisation de ActionName pour renommer la méthode d'action
        [ActionName("Display")]
        public ActionResult Index()
        {
            var model = DateTime.Now;
            return View(model);
        }

        /*=>
         * ChildActionOnly impose que l'appel de cette action ne peut être appelée que comme une action enfant.
         * Seuls un appel à RenderAction ou Action pourra ainsi activer cette action.
         * En l'abscence de cet attribut on peut depuis MVC 3 également tester ControllerContext.IsChildAction
         * 
         * OutputCache règle la durée du cache
         * */

        [ChildActionOnly]
        [OutputCache(Duration = 10)]
        public PartialViewResult CurrentTime()
        {
            var model = DateTime.Now;
            return PartialView(model);
        } 
        #endregion
    }
}
