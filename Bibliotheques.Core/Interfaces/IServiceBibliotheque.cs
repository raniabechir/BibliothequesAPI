using Bibliotheques.Core.Entites;

namespace Bibliotheques.Core.Interfaces
{
    public interface IServiceBibliotheque
    {
        public List<Bibliotheque> Lister(string? nom = null);

        public Bibliotheque? Obtenir(int id);
        public void Creer(Bibliotheque bibliotheque);

        public void Supprimer(int id);
        public void Modifier(Bibliotheque bibliotheque);
    }
}
