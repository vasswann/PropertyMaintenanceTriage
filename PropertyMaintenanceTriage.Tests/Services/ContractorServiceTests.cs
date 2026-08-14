using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Services;

namespace PropertyMaintenanceTriage.Tests.Services
{
    public class ContractorServiceTests
    {
        private ContractorService _contractorService;

        [SetUp]
        public void Setup()
        {
            _contractorService = new ContractorService();
        }

        [TestCase("There is a water leak under the sink.")]
        [TestCase("The toilet is blocked.")]
        [TestCase("The kitchen tap is dripping.")]
        [TestCase("There is a problem with the drain.")]
        public void DetermineContractor_WhenDescriptionContainsPlumbingKeyword_ReturnsPlumber(string issueDescription)
        {
            // Act
            ContractorType result = _contractorService.DetermineContractor(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.Plumber));
        }

        [TestCase("The kitchen lightbulb needs replacing.")]
        [TestCase("The electrical socket is not working.")]
        [TestCase("The socket is sparking.")]
        [TestCase("There is no power in the bedroom.")]
        public void DetermineContractor_WhenDescriptionContainsElectricalKeyword_ReturnsElectrician(string issueDescription)
        {
            // Act
            ContractorType result = _contractorService.DetermineContractor(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.Electrician));
        }

        [TestCase("The front door lock is sticking.")]
        [TestCase("The cupboard handle is loose.")]
        [TestCase("The shelf has come away from the wall.")]
        [TestCase("The door hinge is broken.")]
        public void DetermineContractor_WhenDescriptionContainsHandymanKeyword_ReturnsGeneralHandyman(string issueDescription)
        {
            // Act
            ContractorType result = _contractorService.DetermineContractor(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.GeneralHandyman));
        }

        [Test]
        public void DetermineContractor_WhenNoKeywordMatches_ReturnsPropertyManager()
        {
            // Arrange
            string issueDescription = "There is a strange humming noise coming from the wall.";

            // Act
            ContractorType result = _contractorService.DetermineContractor(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.PropertyManager));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void DetermineContractor_WhenDescriptionIsEmptyOrWhitespace_ReturnsPropertyManager(string issueDescription)
        {
            // Act
            ContractorType result = _contractorService.DetermineContractor(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.PropertyManager));
        }

        [Test]
        public void DetermineContractor_WhenDescriptionIsNull_ReturnsPropertyManager()
        {
            // Act
            ContractorType result = _contractorService.DetermineContractor(null);

            // Assert
            Assert.That(result, Is.EqualTo(ContractorType.PropertyManager));
        }
    }
}