#region using
using System;
using System.Security.Principal;
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Models
{
    
    /// <summary>
    /// Exemple de binding pour isoler le mappage de Controler.User du contexte Http
    /// </summary>
    /// <remarks>
    /// http://www.hanselman.com/blog/IPrincipalUserModelBinderInASPNETMVCForEasierTesting.aspx
    /// </remarks>
    public sealed class IPrincipalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        { 
            if (controllerContext == null) 
            {
                throw new ArgumentNullException("controllerContext");
            }
            
            if (bindingContext == null) 
            {
                throw new ArgumentNullException("bindingContext"); 
            }
            
            // => on fait le binding ici
            IPrincipal p = controllerContext.HttpContext.User; 
            
            return p; 
        }
    }
}