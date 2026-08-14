using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Services;

namespace PropertyMaintenanceTriage.Tests.Services
{
    public class PriorityServiceTests
    {
        private PriorityService _priorityService;

        [SetUp]
        public void Setup()
        {
            _priorityService = new PriorityService();
        }

        [TestCase("There is a massive water leak from the ceiling.")]
        [TestCase("The kitchen is starting to flood.")]
        [TestCase("There is a strong smell of gas.")]
        [TestCase("The socket is sparking.")]
        [TestCase("There is smoke coming from the fitting.")]
        public void DeterminePriority_WhenDescriptionContainsUrgentKeyword_ReturnsUrgent(string issueDescription)
        {
            // Act
            Priority result = _priorityService.DeterminePriority(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Urgent));
        }

        [TestCase("The kitchen lightbulb needs replacing.")]
        [TestCase("The cupboard has a loose handle.")]
        [TestCase("There is some cosmetic paint damage.")]
        public void DeterminePriority_WhenDescriptionContainsLowKeyword_ReturnsLow(string issueDescription)
        {
            // Act
            Priority result = _priorityService.DeterminePriority(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Low));
        }

        [Test]
        public void DeterminePriority_WhenNoKeywordMatches_ReturnsMedium()
        {
            // Arrange
            string issueDescription = "The bedroom lock is sticking and the key is hard to turn.";

            // Act
            Priority result = _priorityService.DeterminePriority(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Medium));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void DeterminePriority_WhenDescriptionIsEmptyOrWhitespace_ReturnsMedium(string issueDescription)
        {
            // Act
            Priority result = _priorityService.DeterminePriority(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Medium));
        }

        [Test]
        public void DeterminePriority_WhenDescriptionIsNull_ReturnsMedium()
        {
            // Act
            Priority result = _priorityService.DeterminePriority(null);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Medium));
        }

        [Test]
        public void DeterminePriority_WhenUrgentAndLowKeywordsBothExist_ReturnsUrgent()
        {
            // Arrange
            string issueDescription = "The lightbulb is broken and the socket is sparking.";

            // Act
            Priority result = _priorityService.DeterminePriority(issueDescription);

            // Assert
            Assert.That(result, Is.EqualTo(Priority.Urgent));
        }
    }
}