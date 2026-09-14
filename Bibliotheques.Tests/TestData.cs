using Bibliotheques.Core.Entites;

namespace Bibliotheques.Tests
{
    public static class TestData
    {
        public static List<Bibliotheque> CreerBibliotheques()
        {
            return new List<Bibliotheque>
            {
                new Bibliotheque
                {
                    Id = 1,
                    Nom = "Bibliothèque Gabrielle-Roy",
                    Adresse = "350, rue Saint-Joseph Est",
                    Capacite = 500
                },

                new Bibliotheque
                {
                    Id = 2,
                    Nom = "Bibliothèque Monique-Corriveau",
                    Adresse = "1100, route de l'Église",
                    Capacite = 350
                },

                new Bibliotheque
                {
                    Id = 3,
                    Nom = "Bibliothèque de Charlesbourg",
                    Adresse = "7950, 1re Avenue",
                    Capacite = 300
                },

                new Bibliotheque
                {
                    Id = 4,
                    Nom = "Bibliothèque de Sainte-Foy",
                    Adresse = "999, avenue Roland-Beaudin",
                    Capacite = 400
                },

                new Bibliotheque
                {
                    Id = 5,
                    Nom = "Bibliothèque de Beauport",
                    Adresse = "600, avenue Royale",
                    Capacite = 250
                }
            };
        }
    }
}