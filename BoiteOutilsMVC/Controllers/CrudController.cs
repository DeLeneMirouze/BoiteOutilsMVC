#region using
using BoiteOutilsMVC.Domaine;
using BoiteOutilsMVC.Models;
using System.Web.Mvc; 
#endregion

// *************************************************
// => Commencer par lire les explications sur la classe Personne.cs puis les explications trouvées dans DemoTypedView.cshtml
// avant de lire la suite.
// Ce contrôleur démontre les techniques de base pour manipuler les données en MVC. D'autres exemples plus sophistiqués dans d'autres
// contrôleurs sont fournis également. Il est conseillé de commencer par DemoDataController
// -------------------------------------------------------------------------------------------
// => UNE ERREUR FRÉQUENTE EST DE PLACER TROP DE CODE DANS LE CONTRÔLEUR
// le contrôleur ne doit rien faire de plus qu'interroger le modèle puis sélectionner une vue appropriée
// pas ou peu de raisons d'avoir plus de 10 lignes de code dans une méthode d'action
//
//        Les responsabilités d'un contrôleur sont:
//         
//          - Valider les entrées
//          - Appels vers le modèle pour préparer les vues
//          - Redirection vers la vue appropriée
// *************************************************

namespace BoiteOutilsMVC.Controllers
{
    /// <summary>
    /// => Ce contrôleur a pour objet la démonstration de techniques d'interaction vue <--> contrôleur
    /// pour effectuer diverses opérations CRUD sur des données
    /// // Il montrera également comment paramétriser le verbe Http associé à une action
    /// </summary>
    public sealed class CrudController : Controller
    {
        /*-----------------------------
         * Une difficulté pratique se pose. Si on parle de CRUD on parle de conteneur de donnée. Le conteneur peut être n'importe quoi:
         * SQL Server, fichier texte... Tout est compatible avec MVC.
         * 
         * Il nous faut bien sûr écrire du code pour cette démo. On a choisit deux options qui toutes deux ne nous oblige pas à disposer d'une base de données:
         * 
         * - une Bll: une classe boite noire qui effectue les opérations CRUD
         * - EF en mode code  First
         * 
         * Ces deux solutions peuvent d'ailleur être fusionnées, à vous de décider, ce n'est pas le rôle du tuto!
         * Vous pouvez aussi adopter une approche EF schema-first, mais il faudra alors obligatoirement une base de données 
         * ce qui ne pose évidement pas de problèmes dans une application réelle.
         * 
         * De nombreuses approches existent donc, il n'appartient pas à ce tuto de les discuter. 
         * Mais quel que soit votre choix, ayez parfaitement en tête l'avertissement suivant:
         * 
         * => UNE ERREUR FRÉQUENTE EST DE PLACER TROP DE CODE DANS LE CONTRÔLEUR
         * le contrôleur ne doit rien faire de plus qu'interroger le modèle puis sélectionner une vue appropriée
         * pas ou peu de raisons d'avoir plus de 10 lignes de code dans une méthode d'action
         * Les responsabilités d'un contrôleur sont:
         * 
         * - Valider les entrées
         * - Appels vers le modèle pour préparer les vues
         * - Redirection vers la vue appropriée
         * 
         * C'est pour cette raison que personnellement je préfère écrire une Bll avec une méthode pour chaque action. Dans ce cas la propriete
         * db n'est pas utile du moins dans le contrôleur.
         * 
         * La méthode va (peut-être) encapsuler de l'EF, mais peut importe. J'augmente les chances d'éviter de placer du code qui
         * n'a rien à faire dans le contrôleur.
         * Ce code est le code métier. Par exemple on peut décider d'afficher un prix TTC ou HT en fonction d'un paramètre du site
         * ou bien du profil du client. Ce n'est pas au contrôleur de faire ce choix, mais au code métier.
         * */


        /// <summary>
        /// => Voir les commentaires précédents
        /// 
        /// Contexte de base de données pour EF (approche code first)
        /// </summary>
        private BoiteOutilsMVCContext db = new BoiteOutilsMVCContext();
        /// <summary>
        /// Approche bll
        /// </summary>
        private PersonneBll bll = new PersonneBll();

        #region Index
        //
        // GET: /FDLM/

        /// <summary>
        /// Obtient la liste des personnes disponibles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Voici deux méthodes possibles pour gérer le modèle et retourner les données à afficher sur la vue
            // La plus traditionnelle est de tout placer dans une BLL ce qui procure l'avantage d'isoler complètement le contrôleur
            // du choix effectué pour la technologie d'accès à la base de données ou de la base elle même.

            // Toutefois dans le cas fréquent où la base est attaquée par EF on peut également développer un contexte comme le montre le code
            // laissé en commentaire

            //var personnes = db.Personnes.ToList();
            var personnes = bll.GetPersonnes();

            return View(personnes);
        }
        #endregion

        #region Details
        //
        // GET: /FDLM/Details/5

        /// <summary>
        /// Obtient les détails sur une personne
        /// </summary>
        /// <param name="id">id de la personne recherchée</param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            // on peut se demander comment fait MVC pour mapper le paramètre id à quelque chose situé sur la vue
            // ce problème sera examiné dans BinderController

            // => ici aussi, deux méthodes démontrées

            Personne personne = bll.GetPersonById(id);
            //Personne personne = db.Personnes.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        } 
        #endregion

        #region Create
        /* => on voit ici à l'oeuvre un pattern important à utiliser.
         * 
         * On souhaite afficher et gérer une page de création d'un nouvel utilisateur. Celà implique deux actions différentes:
         * 1) Afficher la page de création de Personne en cliquant sur le lien Créer
         * 2) Enregistrer les saisies effectuées dans le formulaire de création
         * 
         * Ces deux actions se traduisent par les deux méthodes d'action Create du contrôleur données ci-après.
         * Elle ne sont toutefois pas identiques et ce n'est pas seulement du à leur signature.
         * 
         * Par convention on réserve les méthodes GET aux méthodes qui lisent les données ou tout au moins qui ne les modifient pas.
         * On réserve les méthodes POST aux méthodes qui modifient les données, donc celle qui va enregistrer la nouvelle instance de Personne.
         * 
         * C'est important car pour les robots d'indexation les méthodes GET sont considérées comme sûre et ces lien seront suivit.
         * Si un lien GET permettait par exemple de supprimer un enregistrement dans les tables, on risquerait ainsi de vider le site dès sa
         * première indexation!!
         * 
         * Le résultat est obtenu à l'aide de l'attribut HttpPostAttribute. Si une méthode est marquée de cet attribue, c'est elle qui 
         * sera appelée en cas de POST.
         * En l'absence d'attribut la méthode sera appelée quel que soit le verbe utilisé
         * */

        //
        // GET: / DemoData/Create

        /// <summary>
        /// Afficher la vue de saisie d'une nouvelle instance de Personne
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: / DemoData/Create

        /// <summary>
        /// Enregistre la nouvelle instance
        /// </summary>
        /// <param name="personne">Personne à créer</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Personne personne)
        {
            /* =>
             * Ici une règle d'or: ne jamais faire confiance aux données en provenance de la partie cliente.
             * Les annotations (voir la classe Personne) ont permit de valider diverses choses (type, taille des champs...),
             * mais il est indispensable de recommencer côté serveur.
             * C'est ce que fait ModelState.IsValid
             * 
             * Bien sûr on peut compléter avec des validations supplémentaires (unicité par exemple), on complète alors la
             * pile des messages avec ModelState.AddModelError()
             * 
             * On verra dans une autre fiche des méthodes pour afficher un message d'erreur.
             * 
             * La première chose que fait un hacker est de tenter d'injecter, via une session telnet par exemple, des données invalides histoire
             * de voir ce qui se passe sur le site...
             * */


            if (ModelState.IsValid)
            {
                //db.Personnes.Add(personne);
                //db.SaveChanges();

                bll.Save(personne);

                // => la validation réussit, on retourne à la liste
                return RedirectToAction("Index");
            }

            // => La validation échoue, on réaffiche la page de saisie
            return View(personne);
        } 
        #endregion

        #region Edit
        //
        // GET: /FDLM/Edit/5

        /// <summary>
        /// Affiche le formulaire en mode Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            //Personne personne = db.Personnes.Find(id);
            Personne personne = bll.GetPersonById(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        //
        // POST: /FDLM/Edit/5

        /// <summary>
        /// Enregistre les nouveaux états de Personne
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Personne personne)
        {
            if (ModelState.IsValid)
            {
                // => ne pas oublier, sinon EF va essayer de créer un nouvel enregistrement
                //db.Entry(personne).State = EntityState.Modified;
                //db.SaveChanges();

                bll.Save(personne);
                return RedirectToAction("Index");
            }

            return View(personne);
        } 
        #endregion

        #region Delete
        //
        // GET: /FDLM/Delete/5

        /// <summary>
        /// Affiche la vue de confirmation de la suppression
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            Personne personne = bll.GetPersonById(id);
            //Personne personne = db.Personnes.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        //
        // POST: /FDLM/Delete/5

        // => Noter l'utilisation de l'attribut ActionName qui permet de spécifier l'action qu'une méthode du contrôleur actionne
        // lorsque ce n'est pas le nom par défaut
        // Lisez aussi cet article:
        // http://haacked.com/archive/2008/08/29/how-a-method-becomes-an-action.aspx
        // Pour mieux comprendre à quelles conditions une méthode devient une action

        /// <summary>
        /// Effectue la suppression
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Personne personne = db.Personnes.Find(id);
            //db.Personnes.Remove(personne);
            //db.SaveChanges();

            bll.Delete(id);
            return RedirectToAction("Index");
        } 
        #endregion

        #region Dispose (protected)
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        } 
        #endregion
    }
}
