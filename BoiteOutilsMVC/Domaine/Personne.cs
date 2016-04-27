#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#endregion

namespace BoiteOutilsMVC.Domaine
{
    /// <summary>
    /// => Notez l'attribut MetadataType.
    /// Voir les explications plus bas ainsi que dans PersonneMetaData
    /// 
    /// IValidatableObject ne sera abordé que dans la fiche contrôleur WindowMessageController
    /// </summary>
    [DebuggerDisplay("Nom: {Nom}")]
    [MetadataType(typeof(PersonneMetaData))]
    public sealed class Personne : IValidatableObject
    {
        #region Constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        public Personne()
        {
            PointDeVie = 10;
        }
        #endregion

        // => le point intéressant sont les attributs assignés aux propriétés de la classe.
        // Ce sont des exemples d'annotation. Elles vont affecter la façon dont la méthode
        // Html.DisplayFor va réaliser le rendu Html de la propriété et en particulier gérer les opérations de validation...
        // par exemple un type Int32 sera obligatoire et Int32? facultatif par défaut
        // EmailAdressAttribute validera que l'on a saisi un email correct et affichera l'email en mailto lorsqu'il est en lecture seule...

        // Il existe aussi une annotation MetaDataTypeAttribute qui permet de désigner un type dont le rôle est de fournir 
        // toutes ces métas données.
        //
        // Cette fonctionnalité est surtout intéressante pour les données autogénérées par exemple par un edmx
        //

        public int Id { get; set; }

        public int GenreMusicalId { get; set; }

        /// <summary>
        /// Civilité
        /// </summary>
        public TypeCivilite Civilite { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public DateTime DateNaissance { get; set; }

        public bool SuperHeros { get; set; }

        public string Email { get; set; }
  
        public string Blog { get; set; }

        public string Telephone { get; set; }

        public int PointDeVie { get; set; }

        public string Informations { get; set; }

        #region Validate
        /// <summary>
        /// Détermine si l'objet spécifié est valide
        /// </summary>
        /// <param name="validationContext">Contexte de validation</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nom.Equals(Prenom))
            {
                yield return new ValidationResult("Le nom doit différer du prénom",
                    new string[] { "Nom", "Prenom" });
            }
        } 
        #endregion
    }
}