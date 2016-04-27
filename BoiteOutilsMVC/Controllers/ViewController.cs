#region using
using BoiteOutilsMVC.Domaine;
using BoiteOutilsMVC.Domaine.ViewModels;
using BoiteOutilsMVC.Models;
using System.Web.Mvc; 
#endregion

// => Si vous débutez, il est vivement recommandé de lire DemoDataController d'abord

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// Démonstration de techniques pour construire une vue
    /// </summary>
    public sealed class ViewController : Controller
    {
        private PersonneBll personneBll = new PersonneBll();

        #region Index
        //
        // GET: /View/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id=0)
        {
            Personne personne = personneBll.GetPersonById(id);
            if (personne == null)
            {
                return HttpNotFound();
            }

            // => on utilise un vue/modèle
            // cela permet de travailler avec des vues typées
            // mais on pourrait aussi utiliser ViewData ou ViewBag.

            PersonneViewModel vueModele = new PersonneViewModel();
            vueModele.PersonneAEditer = personne;
            var genres = personneBll.GenresMusique();
            // => SelectList est une classe spécialisée dans l'alimentation d'une drop down list dans une vue via les Html helper
            // On indique dans cet ordre:
            // le contenu de la liste (ici une collection de Binome)
            // le nom de la propriété qui retourne l'id (Binome.Id)
            // Le nom de la propriété qui retourne l'affichage (Binome.Display)
            vueModele.GenresMusicaux = new SelectList(genres, "Id", "Display", personne.GenreMusicalId);

            return View(vueModele);
        } 
        #endregion

    }
}
