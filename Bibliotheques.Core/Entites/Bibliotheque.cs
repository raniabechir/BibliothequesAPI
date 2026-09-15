

namespace Bibliotheques.Core.Entites
{
    public class Bibliotheque
    {
        public int Id { get; set; }

        public string Nom { get; set; } = "";

        public string Arrondissement { get; set; } = "";

        public int Capacite;


        public Bibliotheque() { }
        public Bibliotheque(string nom, string adresse, int capacite)
        {


            Nom = nom;
            Arrondissement = adresse;
            Capacite = capacite;
        }


    }
}
