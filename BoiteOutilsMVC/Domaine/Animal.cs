#region using

#endregion


namespace BoiteOutilsMVC.Domaine
{
    /// <summary>
    /// => Notez le sealed.
    /// C'est une bonne habitude de toujours utiliser des classes sealed tant que l'on a pas démontré que l'on a besoin de
    /// s'en servir comme classe de base.
    /// C'est un peu pour les mêmes raisons que l'on donne toujours le moins de droits possibles à un utilisateur dans un
    /// système.
    /// </summary>
    public sealed class Animal
    {
        public string Nom { get; set; }

        public string Famille { get; set; }
    }
}