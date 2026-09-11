using Bibliotheques.Core.Entites;

namespace Bibliotheques.Data
{
    public static class BibliothequeDonnees
    {
        public static List<Bibliotheque> bibliotheques = new()
    {
        new Bibliotheque { Id = 1, Nom = "Bibliothèque Gabrielle-Roy", Adresse = "350, rue Saint-Joseph Est" },
        new Bibliotheque { Id = 2, Nom = "Bibliothèque Monique-Corriveau", Adresse = "1100, route de l'Église" },
        new Bibliotheque { Id = 3, Nom = "Bibliothèque de Charlesbourg", Adresse = "7950, 1re Avenue" },
        new Bibliotheque { Id = 4, Nom = "Bibliothèque de Sainte-Foy", Adresse = "999, avenue Roland-Beaudin" },
        new Bibliotheque { Id = 5, Nom = "Bibliothèque de Beauport", Adresse = "600, avenue Royale" }
    };
    }
}
