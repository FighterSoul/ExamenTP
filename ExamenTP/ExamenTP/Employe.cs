using System;

namespace ExamenTP
{
    internal class Employe
    {
        private static int _compteur = 0;
        public int Matricule { get; private set; }
        public string Nom { get; set; }
        public string Poste { get; set; }
        public decimal Salaire { get; set; }
        public DateTime DateEmbauche { get; set; }

        public Employe(string nom, string poste, decimal salaire, DateTime dateEmbauche)
        {
            Matricule = ++_compteur;
            Nom = nom;
            Poste = poste;
            Salaire = salaire;
            DateEmbauche = dateEmbauche;
        }

        public override string ToString()
        {
            return $"Matricule: {Matricule} > Nom: {Nom} > Poste: {Poste} > Salaire: {Salaire} > Date d'embauche: {DateEmbauche:dd/MM/yyyy}";
        }
    }
}