#region using
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
#endregion


namespace BoiteOutilsMVC.Domaine
{
    /// <summary>
    /// => Observez les annotations
    /// </summary>
    public sealed class CompteClient 
    {
        /*=>
         * La propriété ErrorMessage permet de créer un message de validation personnalisé. On peut ajouter un place holder
         * qui sera remplacé par le nom de la propriété ici "Mot de passe" et non pas "MotDePasse" puisqu'il y a l'annotation
         * DisplayName
         * 
         * Dans le cadre d'une localisation de l'application on a bien sûr plutôt intérêt à rechercher les messages dans un fichier de ressources.
         * C'est parfaitement possible comme le démontre la propriété Name
         * 
         * Observez aussi le fonctionnement des attributs Remote et Compare
         * */


        //=> RemoteAttribute attend un contrôleur et une action (mais il y a plusieurs surcharges)
        // lance un appel asynchrone vers l'action pour réaliser côté client une validation côté serveur
        // par exemple valider l'unicité d'un logon
        [Remote("Unicite", "WindowMessage",ErrorMessage="Le logon choisit n'est pas unique, choisissez toto7")]
        [Required(ErrorMessage="Vous devez fournir un logon")]
        public string Logon { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("mot de passe")]
        [Required(ErrorMessage="Vous devez fournir un {0}")]
        public string MotDePasse { get; set; }


        //=> Observez l'attribut Compare qui permet de valider si deux zones de saisies contiennent la même chose
        [DataType(DataType.Password)]
        [DisplayName("Répéter le mot de passe")]
        [System.Web.Mvc.Compare("MotDePasse")]
        public string MotDePasse2 { get; set; }


        // => Le message de validation est extrait d'un fichier de ressources
        // ErrorMessageResourceType est le type de la ressource, il doit donc exister un fichier Ressources.resx dans le projet
        // ErrorMessageResourceName est le nom de la ressource
        [Required(ErrorMessageResourceType = typeof(Ressources), ErrorMessageResourceName = "CompteClient_NameRequired")]
        public string Name { get; set; }
    }
}