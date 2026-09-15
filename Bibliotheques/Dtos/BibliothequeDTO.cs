using Bibliotheques.Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Bibliotheques.Api.Dtos
{
    public class BibliothequeDTO
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string Nom { get; set; }

        [Required]
        public string Arrondissement { get; set; }

        [Range(1, 5000, ErrorMessage = "La capacité doit être comprise entre 1 et 5000.")]
        public int Capacite { get; set; }

        public BibliothequeDTO()
        {
        }
        public BibliothequeDTO(Bibliotheque bibliotheque)
        {
            Nom = bibliotheque.Nom;
            Arrondissement = bibliotheque.Arrondissement;
            Capacite = bibliotheque.Capacite;
        }

        public Bibliotheque VersEntite()
        {
            return new Bibliotheque(Nom, Arrondissement, Capacite);
        }
    }
}