#region using
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.Routing; 
#endregion

namespace BoiteOutilsMVC.Models.Filtres
{
    // => ici c'est le même code que PerformanceTestFilter, mais cette fois le filtre
    // implémente ActionFilterAttribute ce qui lui permet d'être utilisé comme attribut
    //
    // Lire le fichier LISEZMOI.TXT
    // Et aussi le tuto suivant:
    // http://www.asp.net/mvc/tutorials/older-versions/controllers-and-routing/understanding-action-filters-cs
    //
    // Note: si on ne souhaite pas utiliser ce filtre comme attribut, rien ne nous oblige à implémenter ActionFilterAttribute
    // On peut se contenter d'implémenter IActionFilter et/ou IResultFilter

    public class PerformanceTestFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch stopWatch = new Stopwatch();

        #region OnActionExecuted
        /// <summary>
        /// => Méthode exécutée APRES l'exécution de l'action
        /// </summary>
        /// <param name="filterContext">Contexte du filtre</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatch.Stop();
            var executionTime = stopWatch.ElapsedMilliseconds;

            // => Ici on utilise une nouvelle fonctionnalité de .Net 4.5 appelée "caller attribute"
            // lire les commentaires dans Log()
            Log(executionTime, filterContext.RouteData);
        } 
        #endregion

        #region OnActionExecuting
        /// <summary>
        /// => Méthode appelée AVANT l'exécution d'une action
        /// </summary>
        /// <param name="filterContext">Contexte du filtre</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch.Reset();
            stopWatch.Start();
        } 
        #endregion

        #region OnResultExecuted
        /// <summary>
        /// Méthode appelée APRES l'exécution du résultat de l'action (rendu par exemple)
        /// </summary>
        /// <param name="filterContext">Contexte du filtre</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        } 
        #endregion

        #region OnResultExecuting
        /// <summary>
        /// Méthode appelée AVANT l'exécution du résultat de l'action (rendu par exemple)
        /// </summary>
        /// <param name="filterContext">Contexte du filtre</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        } 
        #endregion

        #region Log (private)
        /// <summary>
        /// Logue un message dans la fenêtre d'output de Visual Studio
        /// </summary>
        /// <param name="executionTime">Temps d'exécution en ms</param>
        /// <param name="methodName">Nom de la méthode qui appelle Log</param>
        /// <param name="routeData">Route suivie jusqu'à l'action</param>
        private void Log(long executionTime,RouteData routeData, [CallerMemberName] string methodName="")
        {
            // => caller attribute
            // il s'agit d'un jeu de 3 attributs:
            // CallerMemberName, CallerFilePath et CallerLineNumber capables de récupérer des métas informations
            // sur le nom de la méthode courante, le numéro de ligne... dans le fichier PDB
            // Comme on le voit ici on n'a plus besoin de renseigner explicitement le nom de la méthode qui a appelé Log
            // le compilateur le fait pour nous!
            // Un bon tuto:
            // http://blogs.msdn.com/b/vijaysk/archive/2012/09/27/net-4-5-information-of-caller-function-caller-attributes-in-net-4-5.aspx

            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}, temps exécution: {3} ms", methodName, controllerName, actionName, executionTime);
           
            // => le résultat s'affiche dans la fenêtre d'output de Visual Studio
            Debug.WriteLine(message);
        }  
        #endregion

    }
}