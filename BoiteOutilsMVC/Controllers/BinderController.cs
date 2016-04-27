#region using
using BoiteOutilsMVC.Domaine;
using BoiteOutilsMVC.Models;
using System.Security.Principal;
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Controllers
{
    // => Si vous débutez, il est vivement recommandé de lire DemoDataController d'abord


    /* -------------------------------------------------------
    * Le binding sont les techniques qui permettent à MVC de binder les éléments éditables d'un formulaire
    * à des paramètres des méthodes d'action.
    * -------------------------------------------------------
    * 
    * Les vues de saisies contiennent des éléments Html tels inputbox qui vont accueilir des éléments saisis par
    * le client. Ces éléments sont reconnus par la plomberie http via leur attribut name. 
    * Les éléments dotés d'un name seront transmis en POST ou en GET au contrôleur. On devrait donc écrire du code similaire
    * à celui-ci:
    * 
    * [HttpPost]
    * public ActionResult Edit() {
    * var personne = new Personne();
    * 
    * personne.Nom = Request["Nom"];
    * ........
    * }
    * 
    * 
    * Ce code présente deux difficultés:
    * 1) il est laborieux et difficile à maintenir
    * 2) il est lié fortement à un contexte Http via Request ce qui complique les tests unitaires
    * 
    * Ceci est déjà mieux:
    * 
    * [HttpPost]
    * public ActionResult Edit(FormCollection values) {
    * var personne = new Personne();
    * 
    * personne.Nom = values["Nom"];
    * ........
    * }
    * 
    * Mais il est encore possible d'aller plus loin en effectuant une correspondance nom à nom entre les paramètres 
    * de la méthode et le nom des champs  de saisies du formulaire.
    * On retrouve un concept important en MVC: la convention de nommage.
    *
    * [HttpPost]
    * public ActionResult Edit(Personne personne) {
    * ........
    * }
    * 
    * Nous allons analyser quelques aspect de ces techniques.
     * N'hésitez pas à lire cet article en complément:
     * http://odetocode.com/blogs/scott/archive/2009/04/27/6-tips-for-asp-net-mvc-model-binding.aspx
     * 
     * Pour finir sachez que vous n'êtes pas obligé d'utiliser le binding par défaut. En implémentant IModelBinder vous pouvez construire des
     * scénarios plus adaptés à vos besoins. Par exemple ici:
     * http://www.hanselman.com/blog/SplittingDateTimeUnitTestingASPNETMVCCustomModelBinders.aspx
     * 
     * Scott Hanselman montre un exemple de binding qui côté vue affiche une zone de saisie date ET une zone de saisie heure,
     * mais côté contrôleur binde sur un unique paramètre DateTime
     * 
     * Dans ce post:
     * http://www.hanselman.com/blog/IPrincipalUserModelBinderInASPNETMVCForEasierTesting.aspx
     * 
     * On trouve l'utilisation d'un binding personnalisé pour supprimer la dépendence du contrôleur avec la propriété User qui 
     * est elle-même liée à HttpContext ce qui rend les tests unitaires difficiles.
     * 
     * Implémenter un binder personnalisé peut donc aussi être un moyen de décorréler le contrôleur d'un contexte peut être
     * difficile à simuler lors de tests unitaires
    * ------------------------------------------------------*/


    /// <summary>
    /// => Démonstration de plusieurs techniques de binding
    /// 
    /// LISEZ ATTENTIVEMENT LE SOURCE DE PERSONNE.CS
    /// 
    /// </summary>
    /// http://odetocode.com/blogs/scott/archive/2009/04/27/6-tips-for-asp-net-mvc-model-binding.aspx
    /// <remarks>
    /// </remarks>
    public sealed class BinderController : Controller
    {
        /// <summary>
        /// Approche bll
        /// </summary>
        private PersonneBll bll = new PersonneBll();

        #region Edit
        /* =>
         * Les deux méthodes Edit montrent le fonctionnement du binding par défaut
         * Dans les deux cas la vue est la même (fortement typée)
         * Le binder sait s'adapter et rapprocher les valeurs des éléments html ayant un attribut name des paramètres disponibles dans
         * la signature des méthode, que ce soit une valeur directe (id) ou une classe (personne)
         * */

        /// <summary>
        /// Affiche le formulaire en mode Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            Personne personne = bll.GetPersonById(id);
 
            return View(personne);
        }

        /// <summary>
        /// Enregistre les nouveaux états de Personne
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit([Bind(Exclude="PointDeVie")] Personne personne)
        {
            /* => Noter l'attribut Bind 
             * 
             * Le risque avec le binding automatique est qu'un hacker ajoute à la réponse une nouvelle valeur pour la propriété PointDeVie
             * et puisse ainsi changer ses points de vie!
             * L'attribut BindAttribute permet d'éviter ceci en proposant une liste d'exclusion des propriétés qui ne seront pas bindées par le binder
             * 
             * Cette attribut propose d'autres options comme Include, Prefix
             * */

            return View(personne);
        }
        #endregion

        #region EditExplicite
        /* =>
         * Cette fois on réalise un binding EXPLICITE à l'aide des méthodes UpdateModel ou bien TryUpdateModel
         * (seul le premier cas est démontré ici)
         * */

        /// <summary>
        /// Affiche le formulaire en mode Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditExplicite(int id = 0)
        {
            Personne personne = bll.GetPersonById(id);

            return View(personne);
        }

        /// <summary>
        /// Enregistre les nouveaux états de Personne
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditExplicite()
        {
            Personne personne = new Personne();

            try
            {
                UpdateModel(personne);

                // sauvegarde des modifs
                bll.Save(personne);

                // => la vue n'a pas été implémentée dans notre exemple
                // on va donc laisser ceci en commentaire
                //return RedirectToAction("Index");

                // et mettre ceci à la place
                return View(personne);
            }
            catch
            {
                return View(personne);
            }
        }
        #endregion

        #region EditUser
        /* =>
         * La méthode d'action attend cette fois un nouveau paramètre user de type IPrincipal
         * Ce paramètre n'est pas explicitement sur la vue, comment et à quoi le binder?
         * 
         * On va écrire un IModelBinder qui sera attaché à ce paramètre via ModelBinderAttribute qui se chargera du binding
         * Dans notre cas le binder cherche la valeur courante de HttpContext.User
         * */

        /// <summary>
        /// Affiche le formulaire en mode Edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user">user courant</param>
        /// <returns></returns>
        public ActionResult EditUser(int id, [ModelBinder(typeof(IPrincipalModelBinder))] IPrincipal user)
        {
            // => on pourrait éviter l'emploi de ModelBinderAttribute en faisant une déclaration générique dans Application_Start() de global.asax:
            // ModelBinders.Binders[typeof(IPrincipal)] = new IPrincipalModelBinder();
            // du coup la liaison au nouveau binde devient implicite et s'applique à toutes les actions de tous les contrôleurs


            Personne personne = bll.GetPersonById(id);

            // => l'ajout du nouveau binder évite d'avoir à écrire ceci:
            // if (personne.Nom != User.Identity.Name)
            // qui appelle la méthode User du contrôleur
            // de cette façon si la méthode d'action est testée unitairement on a la possibilité
            // d'injecter une instance de IPrincipal sans avoir à dépendre d'un contexte asp.net


            if (personne.Nom != user.Identity.Name)
            {
                return View("AccesNonAutorise");
            }

            return View(personne);
        }

        /// <summary>
        /// Accès non autorisé
        /// </summary>
        /// <returns></returns>
        public string AccesNonAutorise()
        {
            return "L'accès n'est pas autorisé";
        }
        #endregion
    }
}
