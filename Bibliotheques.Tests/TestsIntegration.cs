using Bibliotheques.Api.Dtos;
using Bibliotheques.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Bibliotheques.Tests.Integration
{
    public class BibliothequesApiTests :
        IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient m_client;

        public BibliothequesApiTests(
            WebApplicationFactory<Program> factory)
        {
            m_client = factory.CreateClient();
        }

        private void ReinitialiserDonnees()
        {
            BibliothequeDonnees.bibliotheques =
                TestData.CreerBibliotheques();
        }

        [Fact]
        public async Task GetBibliotheques_RetourneOk()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync("/bibliotheques");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }

        [Fact]
        public async Task GetBibliotheques_RetourneCinqBibliotheques()
        {
            ReinitialiserDonnees();

            var resultat =
                await m_client.GetFromJsonAsync<ResultatBibliotheques>(
                    "/bibliotheques");

            Assert.NotNull(resultat);
            Assert.Equal(5, resultat.total);
            Assert.Equal(5, resultat.bibliotheques.Count);
        }

        [Fact]
        public async Task GetBibliothequeParId_AvecIdExistant_RetourneOk()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync("/bibliotheques/1");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }

        [Fact]
        public async Task GetBibliothequeParId_AvecIdInexistant_RetourneNotFound()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync("/bibliotheques/999");

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);
        }
        [Fact]
        public async Task PostBibliotheque_AvecArrondissementDejaUtilise_RetourneConflict()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Nouvelle Bibliothèque",
                Arrondissement = "Charlesbourg",
                Capacite = 200
            };

            var response =
                await m_client.PostAsJsonAsync(
                    "/bibliotheques",
                    dto);

            Assert.Equal(
                HttpStatusCode.Conflict,
                response.StatusCode);
        }
        [Fact]
        public async Task GetBibliotheques_AvecPagination_RetourneBonneQuantite()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync(
                    "/bibliotheques?page=1&length=2");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            var resultat =
                await response.Content
                    .ReadFromJsonAsync<ResultatBibliotheques>();

            Assert.NotNull(resultat);
            Assert.Equal(5, resultat.total);
            Assert.Equal(2, resultat.bibliotheques.Count);
            Assert.Equal(1, resultat.page);
            Assert.Equal(2, resultat.length);
        }

        [Fact]
        public async Task GetBibliotheques_AvecLength101_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync(
                    "/bibliotheques?page=1&length=101");

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task GetBibliotheques_AvecPageZero_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync(
                    "/bibliotheques?page=0&length=25");

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task GetBibliothequesAvecFiltre_RetourneBibliothequeCorrespondante()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.GetAsync(
                    "/bibliotheques/filtre?nom=Gabrielle");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            var resultat =
                await response.Content
                    .ReadFromJsonAsync<List<BibliothequeDTO>>();

            Assert.NotNull(resultat);
            Assert.Single(resultat);
            Assert.Equal(
                "Bibliothèque Gabrielle-Roy",
                resultat[0].Nom);
        }

        [Fact]
        public async Task PostBibliotheque_AvecDonneesValides_RetourneCreated()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "123 ",
                Capacite = 200
            };

            var response =
                await m_client.PostAsJsonAsync(
                    "/bibliotheques",
                    dto);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task PostBibliotheque_AvecNomInvalide_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "A",
                Arrondissement = "123 ",
                Capacite = 200
            };

            var response =
                await m_client.PostAsJsonAsync(
                    "/bibliotheques",
                    dto);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task PostBibliotheque_AvecAdresseVide_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "",
                Capacite = 200
            };

            var response =
                await m_client.PostAsJsonAsync(
                    "/bibliotheques",
                    dto);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task PostBibliotheque_AvecCapaciteInvalide_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "123 rue Test",
                Capacite = 5001
            };

            var response =
                await m_client.PostAsJsonAsync(
                    "/bibliotheques",
                    dto);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task PutBibliotheque_AvecDonneesValides_RetourneOk()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Bibliothèque Modifiée",
                Arrondissement = "456 ",
                Capacite = 600
            };

            var response =
                await m_client.PutAsJsonAsync(
                    "/bibliotheques/1",
                    dto);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }

        [Fact]
        public async Task PutBibliotheque_AvecIdInexistant_RetourneNotFound()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "456 ",
                Capacite = 600
            };

            var response =
                await m_client.PutAsJsonAsync(
                    "/bibliotheques/999",
                    dto);

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);
        }

        [Fact]
        public async Task PutBibliotheque_AvecNomInvalide_RetourneBadRequest()
        {
            ReinitialiserDonnees();

            var dto = new BibliothequeDTO
            {
                Nom = "A",
                Arrondissement = "456 ",
                Capacite = 600
            };

            var response =
                await m_client.PutAsJsonAsync(
                    "/bibliotheques/1",
                    dto);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteBibliotheque_AvecIdExistant_RetourneNoContent()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.DeleteAsync(
                    "/bibliotheques/1");

            Assert.Equal(
                HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteBibliotheque_AvecIdInexistant_RetourneNotFound()
        {
            ReinitialiserDonnees();

            var response =
                await m_client.DeleteAsync(
                    "/bibliotheques/999");

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);
        }

        private class ResultatBibliotheques
        {
            public int total { get; set; }

            public int page { get; set; }

            public int length { get; set; }

            public List<BibliothequeDTO> bibliotheques { get; set; }
                = new();
        }
    }
}