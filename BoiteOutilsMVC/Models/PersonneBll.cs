#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoiteOutilsMVC.Domaine; 
#endregion

namespace BoiteOutilsMVC.Models
{
    /// <summary>
    /// => Repository des Personnes. Rien de particulier ici
    /// </summary>
    public sealed class PersonneBll
    {

        // => Ce code n'a pas vocation a être un exemple, on lui demande juste de fonctionner

        #region GetPersonnes
        public List<Personne> GetPersonnes()
        {
            List<Personne> personnes = new List<Personne>();

            Personne personne;

            personne = new Personne();
            personne.Id = 1;
            personne.Civilite = TypeCivilite.Madame;
            personne.Nom = "Dupont";
            personne.Prenom = "Sylvie";
            personne.Blog = "www.monblog.com";
            personne.DateNaissance = new DateTime(1960, 1, 1);
            personne.Email = "s.dupont@ici.com";
            personne.PointDeVie = 6;
            personne.SuperHeros = false;
            personne.Telephone = "1111111111";
            personne.GenreMusicalId = 1;

            personnes.Add(personne);

            personne = new Personne();
            personne.Id = 2;
            personne.Civilite = TypeCivilite.Monsieur;
            personne.Nom = "Batman";
            personne.Blog = "www.batman.com";
            personne.DateNaissance = new DateTime(1950, 1, 1);
            personne.Email = "batman@ici.com";
            personne.PointDeVie = 10;
            personne.SuperHeros = true;
            personne.Telephone = "6546976535";
            personne.GenreMusicalId = 2;

            personnes.Add(personne);

            personne = new Personne();
            personne.Id = 3;
            personne.Prenom = "Sandra";
            personne.Civilite = TypeCivilite.Mademoiselle;
            personne.Nom = "Babache";
            personne.DateNaissance = new DateTime(1980, 1, 1);
            personne.PointDeVie = 10;
            personne.SuperHeros = false;
            personne.Telephone = "123456789";
            personne.GenreMusicalId = 0;

            personnes.Add(personne);

            personne = new Personne();
            personne.Id = 4;
            personne.Nom = "BadLuck";
            personne.Prenom = "Brian";
            personne.Civilite = TypeCivilite.Madame;
            personne.DateNaissance = new DateTime(1980, 1, 1);
            personne.PointDeVie = 1;
            personne.SuperHeros = false;
            personne.GenreMusicalId = 2;

            personnes.Add(personne);

            return personnes;
        } 
        #endregion

        #region GetPersonById
        /// <summary>
        /// Recherche une personne par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Personne GetPersonById(int id)
        {
            var Personnes = GetPersonnes();
            return Personnes.Where(p => p.Id == id).First();
        } 
        #endregion

        #region Save
        /// <summary>
        /// Sauvegarde d'une instance de Personne
        /// </summary>
        /// <param name="personne">Instance de Personne à sauvegarder</param>
        public void Save(Personne personne)
        {

        } 
        #endregion

        #region Delete
        /// <summary>
        /// Suppression d'une instance
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
          
        } 
        #endregion

        #region GenresMusique
        /// <summary>
        /// Obtient une liste de genres musicaux
        /// </summary>
        /// <returns></returns>
        public List<Binome> GenresMusique()
        {
            List<Binome> genresMusique = new List<Binome>();
            Binome binome;

            binome = new Binome() { Id = 0, Display = "Jazz" };
            genresMusique.Add(binome);

            binome = new Binome() { Id = 1, Display = "Rock" };
            genresMusique.Add(binome);

            binome = new Binome() { Id = 2, Display = "Classique" };
            genresMusique.Add(binome);

            binome = new Binome() { Id = 3, Display = "Pop" };
            genresMusique.Add(binome);

            return genresMusique;
        } 
        #endregion
    }
}