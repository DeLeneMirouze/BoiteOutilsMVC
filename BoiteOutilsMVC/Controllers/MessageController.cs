#region using
using BoiteOutilsMVC.Domaine;
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
    /// Comprendre la validation et les messages inlines (dans la page)
    /// </summary>
    public sealed class MessageController : Controller
    {
        /* =>
         * On peut valider des données de deux façons:
         * Dans le modèle lui-même
         * En créant (ou utilisant) une annotation
         * 
         * On peut en effet créer des annotations personnalisées en implémentant ValidationAttribute ou bien en dérivant une
         * annotation existante. C'est une nouveauté MVC 3.
         * Par exemple voici un exemple intéressant:
         * http://www.codeproject.com/Articles/260177/Custom-Validation-Attribute-in-ASP-NET-MVC3
         * 
         * Qui valide qu'un âge soit entre deux valeurs (18 et 60 ans par exmple) à partir de la date de naissance
         * 
         * Une autre possibilité est de donner à une entité la possibilité de s'auto-valider en implémentant IValidatableObject.
         * Cette interface n'est pas spécifique à MVC, mais peut servir avec EF, DynamicData, ADO...
         * Tout ce qu'il y a à faire est d'implémenter l'interface, le reste est automatique.
         * 
         * Un exemple d'implémentation est fait pour Personne. Essayer de saisir un nom identique au prénom.
         * 
         * */

        #region Messages inline
        //
        // GET: /Window/Create
            
        public ActionResult Create()
        {
            // => permet de récupérer les valeurs par défaut
            Personne personne = new Personne();
            personne.PointDeVie = 9;
            return View(personne);
        }

        //
        // GET: /Window/Create
        [HttpPost]
        public ActionResult Create(Personne personne)
        {
            //=> ValidationSummary est paramétré pour ne pas afficher les messages d'erreur des propriétés du modèle

            // => notez qu'il n'y a pas de clef (chaine vide) qui associe le message à une propriété du modèle
            // par conséquent, avec les paramètres choisis, seul ValidationSummary affichera ces messages
            if (personne.PointDeVie != 10)
            {
                ModelState.AddModelError("", "Soyons cool, donnons lui 10 points de vie");
            }

            // => Cette fois la clef est le nom d'une propriété du modèle (SuperHeros)
            // Seul ValidationMessageFor affichera le message
            if (!personne.SuperHeros)
            {
                ModelState.AddModelError("SuperHeros", "Il nous faut un super héros");
            }

            if (ModelState.IsValid)
            {
                // => autre manière de faire apparaître un message
                ViewBag.Message = "Enregistrement effectué";
                return View();
            }

            return View(personne);
        } 
        #endregion

        #region Messages de validation personnalisés
        // => ici tout se passe dans le code de CompteClient

        // => pas tout à fait il y a ceci
        // allez voir les explication sur l'annotation Remote
        public JsonResult Unicite(string logon)
        {
            if (logon == "toto7")
            {
                // => la validation réussi
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // => la validation échoue
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreatePersonnalized()
        {
            CompteClient compte = new CompteClient();
            return View(compte);
        }

        [HttpPost]
        public ActionResult CreatePersonnalized(CompteClient compteClient)
        {
         
            return View(compteClient);
        } 
        #endregion

        #region ModalPopup
        // => Voir la vue
        public ActionResult ModalPopup()
        {
            return View();
        } 
        #endregion

        #region Vue partielle 

        /// <summary>
        /// Vue partielle synchrone: chargée via @Html.Partial
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialSync()
        {
            PersonneBll personneBll = new PersonneBll();
            return View(personneBll.GetPersonnes());
        }

        /// <summary>
        /// Vue partielle synchrone: chargée via @Ajax.ActionLink
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialASync()
        {
            return PartialSync();
        }

        public ActionResult Details(int id = 0)
        {
            PersonneBll personneBll = new PersonneBll();
            Personne personne = personneBll.GetPersonById(id);


            //=> appelle la vue partielle _details que par convention on préfixera d'un _ (underscore)
            // cela n'est pas obligatoire, mais permet de les distinguer plus facilement
            // PartialView retourne une vue partielle
            return PartialView("_Details", personne);

        } 
        #endregion
    }
}
