using Bibliotheques.Core.Entites;
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

        public List<Bibliotheque> Lister(string? nom = null, int? page = null)
        {
            return m_depotBibliotheque.ObtenirToutes();
        }
        public Bibliotheque Obtenir(int id)
        {
            return m_depotBibliotheque.ObtenirParId(id);
        }

        public void Creer(Bibliotheque bibliotheque)
        {
            m_depotBibliotheque.Ajouter(bibliotheque);
        }

        public void Modifier(Bibliotheque bibliotheque)
        {
            m_depotBibliotheque.Modifier(bibliotheque);
        }

        public void Supprimer(int id)
        {

            m_depotBibliotheque.Supprimer(id);
        }
    }
}