#region using
using System.Data.Entity;
using BoiteOutilsMVC.Domaine; 
#endregion

namespace BoiteOutilsMVC.Models
{
    /// <summary>
    /// Contexte de base de données EF
    /// </summary>
    public sealed class BoiteOutilsMVCContext : DbContext
    {
        // Vous pouvez ajouter du code personnalisé à ce fichier. Les modifications ne seront pas remplacées.
        // 
        // Si vous voulez qu'Entity Framework abandonne et régénère la base de données
        // automatiquement à chaque fois que vous modifiez le schéma du modèle, ajoutez le code
        // suivant à la méthode Application_Start dans le fichier Global.asax.
        // Remarque : cette opération supprime et recrée la base de données à chaque modification du modèle.
        //
        // => ce qui implique la perte des données qui s'y trouvent!
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BoiteOutilsMVC.Models.BoiteOutilsMVCContext>());

        #region Constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        public BoiteOutilsMVCContext()
            : base("name=BoiteOutilsMVCContext")
        {
            // => BoiteOutilsMVCContext est le nom de la chaîne de connexion
        } 
        #endregion

        public DbSet<Personne> Personnes { get; set; }
    }
}
