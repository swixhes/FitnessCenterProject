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
            var trainer = new Trainer("Олена", "Коваль", 30, "Україна", 2000);
            var hall = new Hall { Name = "Основний зал", Capacity = 20 };
            var training = new Training(TrainingType.Кардіо, trainer, hall, DateTime.Now);

            // Assert
            Assert.AreEqual(TrainingType.Кардіо, training.Type);
            Assert.AreEqual(trainer, training.Trainer);
            Assert.AreEqual(hall, training.Hall);
        }

        [TestMethod]
        [DataRow("Андрій", "Лисенко", 28, "Україна", ClientLevel.Початковець)]
        [DataRow("Софія", "Іванова", 35, "Канада", ClientLevel.Професійний)]
        public void AddClient_ShouldAddMultipleClients(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Arrange
            var trainer = new Trainer("Олена", "Коваль", 30, "Україна", 2000);
            var hall = new Hall { Name = "Основний зал", Capacity = 20 };
            var training = new Training(TrainingType.Кардіо, trainer, hall, DateTime.Now)
            {
                EnrolledClients = new List<Client>()
            };
            var client = new Client(firstName, lastName, age, nationality, level);

            // Act
            training.AddClient(client);

            // Assert
            CollectionAssert.Contains(training.EnrolledClients, client);
        }
    }
}
