using Bibliotheques.Core.Entites;
using Bibliotheques.Core.Exceptions;
using Bibliotheques.Core.Interfaces;
using Bibliotheques.Core.Services;
using Moq;

namespace Bibliotheques.Tests.Unitaires
{
    public class ServiceBibliothequeTests
    {
        [Fact]
        public void Lister_RetourneToutesLesBibliotheques()
        {
            // Arrange
            var bibliotheques = TestData.CreerBibliotheques();

            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirToutes())
                .Returns(bibliotheques);

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            var resultat = service.Lister();

            // Assert
            Assert.Equal(5, resultat.Count);

            mockDepot.Verify(
                d => d.ObtenirToutes(),
                Times.Once);
        }




        [Fact]
        public void Lister_AvecNomIgnoreLaCasse_RetourneBibliothequesCorrespondantes()
        {
            // Arrange
            var bibliotheques = TestData.CreerBibliotheques();

            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirToutes())
                .Returns(bibliotheques);

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            var resultat = service.Lister("GABRIELLE");

            // Assert
            Assert.Single(resultat);
        }

        [Fact]
        public void Lister_AvecNomInexistant_RetourneListeVide()
        {
            // Arrange
            var bibliotheques = TestData.CreerBibliotheques();

            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirToutes())
                .Returns(bibliotheques);

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            var resultat = service.Lister("Inexistant");

            // Assert
            Assert.Empty(resultat);
        }

        [Fact]
        public void Obtenir_AvecIdExistant_RetourneBibliotheque()
        {
            // Arrange
            var bibliotheque = TestData.CreerBibliotheques()[0];

            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirParId(1))
                .Returns(bibliotheque);

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            var resultat = service.Obtenir(1);

            // Assert
            Assert.NotNull(resultat);
            Assert.Equal(1, resultat.Id);
            Assert.Equal(
                "Bibliothèque Gabrielle-Roy",
                resultat.Nom);

            mockDepot.Verify(
                d => d.ObtenirParId(1),
                Times.Once);
        }

        [Fact]
        public void Obtenir_AvecIdInexistant_RetourneNull()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirParId(999))
                .Returns((Bibliotheque?)null);

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            var resultat = service.Obtenir(999);

            // Assert
            Assert.Null(resultat);

            mockDepot.Verify(
                d => d.ObtenirParId(999),
                Times.Once);
        }

        [Fact]
        public void Creer_AvecBibliothequeValide_AppelleAjouter()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();
            mockDepot.Setup(d => d.ObtenirToutes()).Returns(new List<Bibliotheque>() { });


            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "Bibliothèque Test",
                Arrondissement = " Bibliothèque Test",
                Capacite = 200
            };

            // Act
            service.Creer(bibliotheque);

            // Assert
            mockDepot.Verify(
                d => d.Ajouter(bibliotheque),
                Times.Once);
        }

        [Fact]
        public void Creer_AvecNomTropCourt_LanceNomInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "A",
                Arrondissement = "123 ",
                Capacite = 200
            };

            // Act & Assert
            Assert.Throws<NomInvalideException>(
                () => service.Creer(bibliotheque));

            mockDepot.Verify(
                d => d.Ajouter(It.IsAny<Bibliotheque>()),
                Times.Never);
        }




        [Fact]
        public void Creer_AvecAdresseVide_LanceAdresseRequiseException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "",
                Capacite = 200
            };

            // Act & Assert
            Assert.Throws<ArrondisementRequiseException>(
                () => service.Creer(bibliotheque));

            mockDepot.Verify(
                d => d.Ajouter(It.IsAny<Bibliotheque>()),
                Times.Never);
        }


        [Fact]
        public void Creer_AvecCapaciteTropGrande_LanceCapaciteInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "Bibliothèque Test",
                Arrondissement = "123",
                Capacite = 5001
            };

            // Act & Assert
            Assert.Throws<CapaciteInvalideException>(
                () => service.Creer(bibliotheque));

            mockDepot.Verify(
                d => d.Ajouter(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Modifier_AvecBibliothequeValide_AppelleModifier()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Id = 1,
                Nom = "Bibliothèque Modifiée",
                Arrondissement = "456 ",
                Capacite = 600
            };

            // Act
            service.Modifier(bibliotheque);

            // Assert
            mockDepot.Verify(
                d => d.Modifier(bibliotheque),
                Times.Once);
        }

        [Fact]
        public void Modifier_AvecNomInvalide_LanceNomInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Id = 1,
                Nom = "A",
                Arrondissement = "456",
                Capacite = 600
            };

            // Act & Assert
            Assert.Throws<NomInvalideException>(
                () => service.Modifier(bibliotheque));

            mockDepot.Verify(
                d => d.Modifier(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Modifier_AvecAdresseVide_LanceAdresseRequiseException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Id = 1,
                Nom = "Bibliothèque Test",
                Arrondissement = "",
                Capacite = 600
            };

            // Act & Assert
            Assert.Throws<ArrondisementRequiseException>(
                () => service.Modifier(bibliotheque));

            mockDepot.Verify(
                d => d.Modifier(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Modifier_AvecCapaciteInvalide_LanceCapaciteInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Id = 1,
                Nom = "Bibliothèque Test",
                Arrondissement = "456 rue Test",
                Capacite = 5001
            };

            // Act & Assert
            Assert.Throws<CapaciteInvalideException>(
                () => service.Modifier(bibliotheque));

            mockDepot.Verify(
                d => d.Modifier(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Creer_AvecArrondissementDejaUtilise_LanceException()
        {
            // Act
            var bibliotheques = TestData.CreerBibliotheques();
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            mockDepot
                .Setup(d => d.ObtenirToutes())
                .Returns(bibliotheques);

            var service = new ServiceBibliotheque(mockDepot.Object);

            var nouvelleBibliotheque = new Bibliotheque
            {
                Nom = "Nouvelle bibliothèque",
                Arrondissement = bibliotheques[0].Arrondissement,
                Capacite = 200
            };

            // Act & Assert
            Assert.Throws<ArrondissementDejaUtiliseException>(
                () => service.Creer(nouvelleBibliotheque));

            mockDepot.Verify(
                d => d.Ajouter(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Supprimer_AppelleSupprimerDuDepot()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            // Act
            service.Supprimer(1);

            // Assert
            mockDepot.Verify(
                d => d.Supprimer(1),
                Times.Once);
        }
    }
}