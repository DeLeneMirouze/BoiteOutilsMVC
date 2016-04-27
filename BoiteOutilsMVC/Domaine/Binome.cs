#region using

#endregion

namespace BoiteOutilsMVC.Domaine
{
    /// <summary>
    /// Classe pour alimenter un couple id/affichage
    /// </summary>
    public sealed class Binome
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Affichage
        /// </summary>
        public string Display { get; set; }
    }
}