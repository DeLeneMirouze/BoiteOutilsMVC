#region using
using System.Collections.Generic;
using System.Web.Mvc;
#endregion

namespace BoiteOutilsMVC.Models
{
    /// <summary>
    /// => classe pour alimenter la liste déroulante
    /// </summary>
    public static class DropdownData
    {
        #region GetCachedCategoryList
        /// <summary>
        /// Returns a static category list that is cached
        /// </summary>
        /// <returns></returns>
        public static SelectListItem[] GetCachedCountriesList()
        {
            // =>la liste va rarement changer, on la met en cache dans une variable statique
            // Une autre approche serait d'utiliser les caches

            if (_CategoryList != null)
            {
                // en cache
                return _CategoryList;
            }

            // =>double-checked locking pattern
            // ou verrouillage double test
            // Ce pattern est sujet à de vastes débats quen à savoir s'il s'agit d'un pattern ou d'un antipattern
            // la réponse dépend de beaucoup de choses et notamment du compilateur ou du langage
            // Une revue pour c# se trouve ici:
            // http://en.wikipedia.org/wiki/Double-checked_locking#Usage_in_Microsoft_.NET_.28Visual_Basic.2C_C.23.29
            //
            lock (_SyncLock)
            {
                if (_CategoryList != null)
                {
                    return _CategoryList;
                }

                List<SelectListItem> catList = new List<SelectListItem> { 
                                               new SelectListItem() { Text = "France", Value = "1" } ,
                                                new SelectListItem() { Text = "Allemagne", Value = "2" } ,
                                                 new SelectListItem() { Text = "Canada", Value = "3" } ,
                                                  new SelectListItem() { Text = "Brésil", Value = "4" } ,
                                                   new SelectListItem() { Text = "Tunisie", Value = "5" } ,
                                                    new SelectListItem() { Text = "Australie", Value = "6" } ,
                                                     new SelectListItem() { Text = "Japon", Value = "7" } 
                                           };

                _CategoryList = catList.ToArray();
            }

            return _CategoryList;
        } 
        #endregion

        // Le cache
        private static SelectListItem[] _CategoryList;

        /// <summary>
        /// Pour faire un lock
        /// </summary>
        private static object _SyncLock = new object();
    }
}