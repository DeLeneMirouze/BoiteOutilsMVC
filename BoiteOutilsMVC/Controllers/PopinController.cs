#region using
using BoiteOutilsMVC.Domaine;
using BoiteOutilsMVC.Models;
using System.Web.Mvc;
using System.IO;
#endregion

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// Démonstration d'une popin
    /// http://www.stefanprodan.eu/2011/05/edit-data-in-dialog-form-with-jquery-and-asp-net-mvc/
    /// </summary>
    public class PopinController : Controller
    {
        PersonneBll personneBll = new PersonneBll();

        #region Index
        //
        // GET: /Popin/

        public ActionResult Index()
        {
            var personne = personneBll.GetPersonnes();

            return View(personne);
        }
        #endregion

        #region Edit

        //
        // GET: /Popin/Edit/5

        /// <summary>
        /// => Obtient le formulaire d'édition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult EditPersonne(int id)
        {
            Personne personne = personneBll.GetPersonById(id);

            var param = new
                {
                    // => récupère le rendu de la vue partielle _PersonneEdit
                    Html = RenderPartialView("_PersonneEdit", personne)
                };

            return Json(
                param,
                JsonRequestBehavior.AllowGet
            );
        }

        //
        // POST: /Popin/Edit/5

        /// <summary>
        /// Sauvegarde les modifications
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPersonne(Personne personne)
        {
            try
            {
                personneBll.Save(personne);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// obtient le html du rendu d'une vue
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string RenderPartialView(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}
