using Bibliotheques.Core.Entites;

namespace Bibliotheques.Data
{
    public static class BibliothequeDonnees
    {
        public static List<Bibliotheque> bibliotheques = new()
    {

 new Bibliotheque { Id = 1, Nom = "Bibliothèque Gabrielle-Roy", Arrondissement = "La Cité-Limoilou", Capacite = 500 },
new Bibliotheque { Id = 2, Nom = "Bibliothèque Monique-Corriveau", Arrondissement = "Sainte-Foy–Sillery–Cap-Rouge", Capacite = 350 },
new Bibliotheque { Id = 3, Nom = "Bibliothèque de Charlesbourg", Arrondissement = "Charlesbourg", Capacite = 300 },
new Bibliotheque { Id = 4, Nom = "Bibliothèque de Sainte-Foy", Arrondissement = "Sainte-Foy–Sillery–Cap-Rouge", Capacite = 400 },
new Bibliotheque { Id = 5, Nom = "Bibliothèque de Beauport", Arrondissement = "Beauport", Capacite = 250 }

    };
    }
}
