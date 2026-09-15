using Bibliotheques.Core.Entites;
using Bibliotheques.Core.Exceptions;
using Bibliotheques.Core.Interfaces;


namespace Bibliotheques.Core.Services
{
    public class ServiceBibliotheque : IServiceBibliotheque
    {
        private readonly IDepotBibliotheque m_depotBibliotheque;

        public ServiceBibliotheque(IDepotBibliotheque depot)
        {
            m_depotBibliotheque = depot;
        }

        public List<Bibliotheque> Lister(string? nom = null)
        {
            List<Bibliotheque> bibliotheques = m_depotBibliotheque.ObtenirToutes();

            if (!string.IsNullOrEmpty(nom))
            {
                bibliotheques = bibliotheques
                    .Where(b => b.Nom.Contains(nom, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return bibliotheques;
        }
        public Bibliotheque? Obtenir(int id)
        {
            return m_depotBibliotheque.ObtenirParId(id);
        }

        public void Creer(Bibliotheque bibliotheque)
        {
            if (string.IsNullOrWhiteSpace(bibliotheque.Arrondissement))
            {
                throw new ArrondisementRequiseException();
            }

            if (string.IsNullOrWhiteSpace(bibliotheque.Nom) ||
                bibliotheque.Nom.Length < 2 ||
                bibliotheque.Nom.Length > 100)
            {
                throw new NomInvalideException(bibliotheque.Nom);
            }


            if (bibliotheque.Capacite < 1 || bibliotheque.Capacite > 5000)
            {
                throw new CapaciteInvalideException(bibliotheque.Capacite);
            }

            var bibliotheques = m_depotBibliotheque.ObtenirToutes();

            if (bibliotheques.Any(b =>
                b.Arrondissement.Equals(
                    bibliotheque.Arrondissement,
                    StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArrondissementDejaUtiliseException();
            }


            m_depotBibliotheque.Ajouter(bibliotheque);
        }

        public void Modifier(Bibliotheque bibliotheque)
        {
            if (string.IsNullOrWhiteSpace(bibliotheque.Arrondissement))
            {
                throw new ArrondisementRequiseException();
            }

            if (string.IsNullOrWhiteSpace(bibliotheque.Nom) ||
                bibliotheque.Nom.Length < 2 ||
                bibliotheque.Nom.Length > 100)
            {
                throw new NomInvalideException(bibliotheque.Nom);
            }

            if (bibliotheque.Capacite < 1 || bibliotheque.Capacite > 5000)
            {
                throw new CapaciteInvalideException(bibliotheque.Capacite);
            }

            m_depotBibliotheque.Modifier(bibliotheque);
        }

        public void Supprimer(int id)
        {

            m_depotBibliotheque.Supprimer(id);
        }
    }
}