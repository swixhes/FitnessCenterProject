using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainingTests
    {
        [TestMethod]
        [DataRow(TrainingType.Кардіо)]
        [DataRow(TrainingType.Йога)]
        public void AddClient_ShouldAddClientToTraining(TrainingType type)
        {
            // Arrange
            var trainer = new Trainer("John", "Doe", 30, "USA", 5000, ClientLevel.Середній);
            var hall = new Hall("Main Hall", 10);
            var training = new Training(type, trainer, hall, DateTime.Today, 10);
            var client = new Client("Alice", "Johnson", 28, "USA", ClientLevel.Середній);

            // Act
            var result = training.AddClient(client);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(training.Clients.Contains(client));
        }
    }
}
