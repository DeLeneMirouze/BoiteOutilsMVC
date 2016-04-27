#region using
using System.Web.Mvc; 
#endregion

namespace BoiteOutilsMVC.Controllers
{
    // => lire /Models/Filtres/LISEZMOI.txt
    // rien d'important dans le contrôleur

    public sealed class FilterController : Controller
    {
        //
        // GET: /Filter/

        public ActionResult Index()
        {
            return View();
        }

    }
}
