#region using
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Domaine.ViewModels
{
    /// <summary>
    /// Le vue modèle de la vue
    /// </summary>
    public sealed class MessageListViewModel
    {
        public SelectListItem[] CountriesList { get; set; }

        public string SelectedIdCountry { get; set; }

        public string Message { get; set; }
    }
}