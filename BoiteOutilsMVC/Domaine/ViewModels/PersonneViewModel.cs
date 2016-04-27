#region using
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Domaine.ViewModels
{
    /// <summary>
    /// Une classe Vue/modèle est une classe qui fournit toutes les données utilisées par une vue. Elle permet de créer des vues
    /// fortement typées
    /// </summary>
    public sealed class PersonneViewModel
    {
        public Personne PersonneAEditer { get; set; }

        public SelectList GenresMusicaux { set; get; }
    }
}