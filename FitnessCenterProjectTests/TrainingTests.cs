using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainingTests
    {
        [TestMethod]
        public void Training_ShouldInitializeCorrectly()
        {
            // Arrange
            var trainer = new Trainer("John", "Doe", 30, "Ukraine", 1000);
            var hall = new Hall { Name = "Main Hall", Capacity = 20 };
            var training = new Training(TrainingType.Cardio, trainer, hall, DateTime.Now);

            // Assert
            Assert.AreEqual(TrainingType.Cardio, training.Type);
            Assert.AreEqual(trainer, training.Trainer);
            Assert.AreEqual(hall, training.Hall);
        }

        [TestMethod]
        [DataRow("Jane", "Doe", 28, "USA", ClientLevel.Intermediate)]
        [DataRow("Alex", "Smith", 35, "Canada", ClientLevel.Professional)]
        public void AddClient_ShouldAddMultipleClients(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Arrange
            var trainer = new Trainer("John", "Doe", 30, "Ukraine", 1000);
            var hall = new Hall { Name = "Main Hall", Capacity = 20 };
            var training = new Training(TrainingType.Cardio, trainer, hall, DateTime.Now)
            {
                EnrolledClients = new List<Client>()
            };
            var client = new Client(firstName, lastName, age, nationality, level);

            // Act
            training.AddClient(client);

            // Assert
            CollectionAssert.Contains(training.EnrolledClients, client);
        }

        [TestMethod]
        public void AddClient_ShouldNotAddDuplicateClient()
        {
            // Arrange
            var trainer = new Trainer("John", "Doe", 30, "Ukraine", 1000);
            var hall = new Hall { Name = "Main Hall", Capacity = 20 };
            var training = new Training(TrainingType.Cardio, trainer, hall, DateTime.Now)
            {
                EnrolledClients = new List<Client>()
            };
            var client = new Client("Jane", "Doe", 28, "USA", ClientLevel.Intermediate);

            // Act
            training.AddClient(client);
            training.AddClient(client);

            // Assert
            Assert.AreEqual(1, training.EnrolledClients.Count, "Duplicate clients should not be added.");
        }
    }
}
