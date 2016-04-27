#region using
using System.Collections.Generic;
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Models.Filtres
{
    /// <summary>
    /// => Implémentation d'un fournisseur de filtre, c'est à dire une interface pour obtenir une instance de filtre
    /// C'est une nouveauté de MVC 3.
    /// 
    /// </summary>
    public sealed class PerformanceTestFilterProvider : IFilterProvider
    {
        /// <summary>
        /// Retourne un énumérateur qui retourne toutes les instances de IFilterProvider
        /// </summary>
        /// <param name="controllerContext">Contexte du contrôleur</param>
        /// <param name="actionDescriptor">Descripteur de l'action</param>
        /// <returns></returns>
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            // => les derniers paramètres sont des abstractions du contrôleur et de la méthode d'action respectivement 
            // cela nous permet d'obtenir des informations sur le contexte indépendemment de la nature exacte du contrôleur
            // et de l'action


            /* =>
             * GetFilter est une méthode façade qui agrège les filtres issus de tous les fournisseurs dans une simple liste.
             * L'ordre d'injection n'est pas important
             * 
             * Pourquoi utiliser IFilterProvider?
             * Si on observe bien le code de PerformanceTestFilter on observe que l'on a besoin d'une instance par action afin de  pouvoir
             * mesurer des requêtes concurrentes.
             * Si on ajoute le filtre de façon globale:
             * GlobalFilters.Filters.Add(new PerformanceTestFilter());
             * 
             * Crée une seule instance pour toutes les actions. Ce n'est pas ce dont on a besoin.
             * 
             * Par défaut on dispose de 3 fournisseurs de filtres:
             * 
             * 1) GlobalFilters.Filters pour les filtres globaux
             * 2) FilterAttributeFilterProvider pour les filtres attributs
             * 3) ControllerInstanceFilterProvider pour les filtres du contrôleur
             * 
             * Aucun de ces fournisseurs ne nous convient, on va donc en créer un nouveau.             * 
             * IFilterProvider permet donc de créer un fournisseur personnalisé.
             * 
             * On va ensuite l'injecter dans Application_Start()
             * 
             * pour en savoir plus, en particulier l'utilisation d'injection de dépendances
             * http://bradwilson.typepad.com/blog/2010/07/service-location-pt4-filters.html
             * un exemple de provider qui fournit un filtre, sauf pour certaines actions
             * http://haacked.com/archive/2011/04/25/conditional-filters.aspx
             */

            // GetFilters est appelée à chaque requête.
            // Ici il n'y a pas de conditions particulières, donc le code sera toujours exécuté et en l'occurence
            // on fournit un tableau de Filter qui contient entre autre une nouvelle inctance de PerformanceTestFilterAttribute
            return new[] { 
                new Filter(new PerformanceTestFilterAttribute(), FilterScope.Global, 0) 
            };
        }
    }
}