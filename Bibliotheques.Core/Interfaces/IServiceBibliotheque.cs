using Bibliotheques.Core.Entites;

namespace Bibliotheques.Core.Interfaces
{
    public interface IServiceBibliotheque
    {
        public List<Bibliotheque> Lister(string? nom = null, int? page = null);

        public Bibliotheque Obtenir(int id);
        public void Creer(Bibliotheque bibliotheque);

        public void Supprimer(Bibliotheque bibliotheque);
        public void Modifier(Bibliotheque bibliotheque);
    }
}
