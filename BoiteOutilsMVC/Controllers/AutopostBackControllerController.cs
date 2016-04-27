#region using
using BoiteOutilsMVC.Domaine.ViewModels;
using BoiteOutilsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// Contrôleur de la démo autopostback sur une sélection dans une liste déroulante
    /// </summary>
    public class AutopostBackController : Controller
    {
        public ActionResult Index()
        {
            MessageListViewModel model = new MessageListViewModel();
            model.CountriesList = DropdownData.GetCachedCountriesList();
            model.Message = "Faites une sélection d'un pays";
            return View("List",model);
        }

        //
        // GET: /ComponentBehavior/List

        [HttpPost]
        public ActionResult List(MessageListViewModel model)
        {
            model.CountriesList = DropdownData.GetCachedCountriesList();

            var pays = model.CountriesList.Where(p => p.Value == model.SelectedIdCountry).Select(p => p.Text).First();
            model.Message = string.Format("Vous avez sélectionné {0}", pays);
            return View(model);
        }

    }
}
