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

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "Bibliothèque Test",
                Adresse = "123 rue Test",
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
                Adresse = "123 rue Test",
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
        public void Creer_AvecNomVide_LanceNomInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "",
                Adresse = "123 rue Test",
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
                Adresse = "",
                Capacite = 200
            };

            // Act & Assert
            Assert.Throws<AdresseRequiseException>(
                () => service.Creer(bibliotheque));

            mockDepot.Verify(
                d => d.Ajouter(It.IsAny<Bibliotheque>()),
                Times.Never);
        }

        [Fact]
        public void Creer_AvecCapaciteZero_LanceCapaciteInvalideException()
        {
            // Arrange
            var mockDepot = new Mock<IDepotBibliotheque>();

            var service = new ServiceBibliotheque(mockDepot.Object);

            var bibliotheque = new Bibliotheque
            {
                Nom = "Bibliothèque Test",
                Adresse = "123 rue Test",
                Capacite = 0
            };

            // Act & Assert
            Assert.Throws<CapaciteInvalideException>(
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
                Adresse = "123 rue Test",
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
                Adresse = "456 rue Test",
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
                Adresse = "456 rue Test",
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
                Adresse = "",
                Capacite = 600
            };

            // Act & Assert
            Assert.Throws<AdresseRequiseException>(
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
                Adresse = "456 rue Test",
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