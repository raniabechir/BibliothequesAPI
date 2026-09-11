using Bibliotheques.Core.Entites;


namespace Bibliotheques.Data
{
    public class DepotBibliotheques
    {
        private List<Bibliotheque> bibliotheques = BibliothequeDonnees.bibliotheques;
        public List<Bibliotheque> ObtenirToutes()
        {
            return bibliotheques;
        }

        public Bibliotheque? ObtenirParId(int id)
        {
            return bibliotheques.FirstOrDefault(b => b.Id == id);
        }
        public void Ajouter(Bibliotheque bibliotheque)
        {
            bibliotheques.Add(bibliotheque);
        }
        public void Modifier(Bibliotheque bibliotheque)
        {
            int index = bibliotheques.FindIndex(b => b.Id = bibliotheque.Id);

            if (index != -1)
            {
                bibliotheques[index] = bibliotheque;
            }
        }

        public void Supprimer(int id)
        {
            Bibliotheque bibliotheque = ObtenirParId(id);
            if (bibliotheque is not null)
            {
                bibliotheques.Remove(bibliotheque);
            }

        }



    }
}
