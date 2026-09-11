using Bibliotheques.Core.Entites;

namespace Bibliotheques.Core.Interfaces
{
    public interface IDepotBibliotheque
    {

        List<Bibliotheque> ObtenirToutes();

        Bibliotheque? ObtenirParId(int id);
        void Ajouter(Bibliotheque bibliotheque);
        void Modifier(Bibliotheque bibliotheque);
        void Supprimer(int id);


    }
}
