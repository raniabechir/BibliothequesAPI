using Bibliotheques.Api.Dtos;
using Bibliotheques.Core.Entites;
using Bibliotheques.Core.Interfaces;


namespace Bibliotheques.Api.EndPoints
{
    public static class BibliothequesEndPoints
    {
        public static void MapBibliothequeEndpoints(this WebApplication app)
        {
            // GET ALL
            app.MapGet("/bibliotheques", (IServiceBibliotheque service, int page = 1, int length = 25) =>
            {
                if (page < 1 || length < 1 || length > 100)
                {
                    return Results.BadRequest("La page doit être supérieure à 0 et la taille de page doit être entre 1 et 100.");
                }

                List<Bibliotheque> bibliotheques = service.Lister();

                int total = bibliotheques.Count;

                var resultat = bibliotheques
                    .Skip((page - 1) * length)
                    .Take(length)
                    .Select(b => new BibliothequeDTO(b));

                return Results.Ok(new
                {
                    total,
                    page,
                    length,
                    bibliotheques = resultat
                });
            });
            // GET BY ID
            app.MapGet("/bibliotheques/{id}", (int id, IServiceBibliotheque service) =>
            {
                var bibliotheque = service.Obtenir(id);

                return bibliotheque is not null
                    ? Results.Ok(new BibliothequeDTO(bibliotheque))
                    : Results.NotFound();
            });
            // POST
            app.MapPost("/bibliotheques", (BibliothequeDTO dto, IServiceBibliotheque service) =>
            {
                Bibliotheque bibliotheque = dto.VersEntite();

                service.Creer(bibliotheque);

                return Results.Created(
                    $"/bibliotheques/{bibliotheque.Id}",
                    new BibliothequeDTO(bibliotheque)
                );
            });

            // PUT
            app.MapPut("/bibliotheques/{id}", (int id, BibliothequeDTO updatedBiblio, IServiceBibliotheque service) =>
            {
                var currentBiblio = service.Lister().FirstOrDefault(b => b.Id == id);

                if (currentBiblio is null)
                {
                    return Results.NotFound();
                }

                currentBiblio.Nom = updatedBiblio.Nom;
                currentBiblio.Adresse = updatedBiblio.Adresse;
                currentBiblio.Capacite = updatedBiblio.Capacite;
                service.Modifier(currentBiblio);

                return Results.Ok(new BibliothequeDTO(currentBiblio));
            });
            // DELETE
            app.MapDelete("/bibliotheques/{id}", (int id, IServiceBibliotheque service) =>
            {
                var bibliotheque = service.Lister().FirstOrDefault(b => b.Id == id);

                if (bibliotheque is null)
                    return Results.NotFound();

                service.Supprimer(id);
                return Results.NoContent();


            });

            //Filtre
            app.MapGet("/bibliotheques/filtre", (string? nom, IServiceBibliotheque service) =>
            {
                var bibliotheques = service.Lister(nom);

                var resultat = bibliotheques
                    .Select(b => new BibliothequeDTO(b));

                return Results.Ok(resultat);
            });

        }
    }

}
