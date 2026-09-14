using Bibliotheques.Core.Entites;
namespace Bibliotheques.Api.Dtos
{
    public class BibliothequeDTO
    {
        [property: Required, StringLength(100, MinimumLength = 2)]
        public string Nom;

        [property: Required]
        public string Adresse;
        [property: Range(1, 5000)]
        public int Capacite;

        public BibliothequeDTO(Bibliotheque bibliotheque)
        {
            Nom = bibliotheque.Nom;
            Adresse = bibliotheque.Adresse;
            Capacite = bibliotheque.Capacite;
        }



        public Bibliotheque VersEntite()
        {
            return new Bibliotheque
            {
                Nom = Nom,
                Adresse = Adresse,
                Capacite = Capacite
            };
        }






    }
}
