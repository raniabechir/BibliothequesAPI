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
            app.MapGet("/bibliotheques", (IServiceBibliotheque service) =>
            {
                List<Bibliotheque> bibliotheques = service.Lister();


                Results.Ok(bibliotheques.Select(b => new BibliothequeDTO(b)));

                // GET BY ID
                app.MapGet("/bibliotheques/{id}", (int id, IServiceBibliotheque service) =>
                {
                    return service.Lister().FirstOrDefault(b => b.Id == id) is { } b
                        ? Results.Ok(new BibliothequeDTO(b))
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

                    service.Supprimer(bibliotheque);
                    return Results.NoContent();


                });

                //Filtre
                app.MapGet("/bibliotheques/filtre", (string? nom, IServiceBibliotheque service) =>
                {
                    var filteredBiblios = service.Lister().AsEnumerable();

                    if (!string.IsNullOrEmpty(nom))
                    {
                        filteredBiblios = filteredBiblios.Where(b => b.Nom.Contains(nom, StringComparison.OrdinalIgnoreCase));
                    }

                    return Results.Ok(filteredBiblios);
                });

            }
    }
    }
}
