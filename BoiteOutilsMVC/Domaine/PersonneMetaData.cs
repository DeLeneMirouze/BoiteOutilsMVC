#region using
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Web.Mvc;

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
    public sealed class PersonneMetaData
    {
        /* =>
         *  Cette classe est la classe des métas données de Personne
         *  Elle n'a pas d'autres buts que de fournir les annotations, elle ne sera jamais instanciées directement
         * 
         */

        public int Id { get; set; }

        // => Nom d'affichage utilisé par les Html helper
        [DisplayName("Genre musical")]
        public int GenreMusicalId { get; set; }

        /// <summary>
        /// Civilité
        /// </summary>
        public TypeCivilite Civilite { get; set; }

        [MaxLength(80)]
        [MinLength(1)]
        [Required]
        public string Nom { get; set; }

        [MaxLength(80)]
        [MinLength(1)]
        public string Prenom { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateNaissance { get; set; }

        public bool SuperHeros { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true,
            NullDisplayText = "Faites une saisie",
            ApplyFormatInEditMode = false,
            DataFormatString = "http://{0}")]
        [Url]
        public string Blog { get; set; }

        [Phone]
        public string Telephone { get; set; }

        [Range(1,10)]
        public int PointDeVie { get; set; }

        // => AllowHtml: autorise cette propriété à recevoir du code Html
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Informations { get; set; }
    }
}