using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class FitnessCenterTests
    {
        [TestMethod]
        public void FitnessCenter_ShouldInitializeCorrectly()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("MyFitness");

            // Assert
            Assert.AreEqual("MyFitness", fitnessCenter.Name);
            Assert.AreEqual(0, fitnessCenter.Halls.Count);
            Assert.AreEqual(0, fitnessCenter.Trainings.Count);
            Assert.AreEqual(0, fitnessCenter.Clients.Count);
            Assert.AreEqual(0, fitnessCenter.Trainers.Count);
            Assert.AreEqual(0, fitnessCenter.Accounts.Count);
        }

        [TestMethod]
        
        public void RegisterAccount_ShouldAddAccount()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("MyFitness");
            var client = new Client("John", "Doe", 30, "USA", ClientLevel.Початковець);
            var account = new ClientAccount("johndoe", "password123", client, fitnessCenter, (msg, color) => { });

            // Act
            fitnessCenter.RegisterAccount(account);

            // Assert
            Assert.IsTrue(fitnessCenter.Accounts.Contains(account));
        }

        [TestMethod]
        public void AddTraining_ShouldIncreaseTrainingCount()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("MyFitness");
            var hall = new Hall("Main Hall", 20);
            var trainer = new Trainer("John", "Doe", 30, "USA", 5000, ClientLevel.Початковець);
            fitnessCenter.Halls.Add(hall);

            var training = new Training(TrainingType.Кардіо, trainer, hall, DateTime.Now, 20);

            // Act
            fitnessCenter.Trainings.Add(training);

            // Assert
            Assert.AreEqual(1, fitnessCenter.Trainings.Count);
            Assert.IsTrue(fitnessCenter.Trainings.Contains(training));
        }

        [TestMethod]
        public void AddClientToTraining_ShouldAddClient()
        {
            // Arrange
            var hall = new Hall("Main Hall", 10);
            var trainer = new Trainer("Anna", "Smith", 35, "Canada", 4000, ClientLevel.Середній);
            var training = new Training(TrainingType.Силові, trainer, hall, DateTime.Now, 10);
            var client = new Client("Jane", "Doe", 25, "Canada", ClientLevel.Середній);

            // Act
            var added = training.AddClient(client);

            // Assert
            Assert.IsTrue(added);
            Assert.AreEqual(1, training.Clients.Count);
            Assert.IsTrue(training.Clients.Contains(client));
        }

        [TestMethod]
        public void AddClientToFullTraining_ShouldThrowException()
        {
            // Arrange
            var hall = new Hall("Small Hall", 1);
            var trainer = new Trainer("Paul", "Brown", 40, "UK", 5000, ClientLevel.Професійний);
            var training = new Training(TrainingType.Йога, trainer, hall, DateTime.Now, 1);
            var client1 = new Client("Alice", "Johnson", 28, "UK", ClientLevel.Початковець);
            var client2 = new Client("Bob", "Taylor", 30, "UK", ClientLevel.Початковець);

            training.AddClient(client1);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => training.AddClient(client2));
        }

        [TestMethod]
        public void RegisterClient_ShouldPreventDuplicateAccounts()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("MyFitness");
            var client1 = new Client("John", "Doe", 30, "USA", ClientLevel.Початковець);
            var account1 = new ClientAccount("johndoe", "password123", client1, fitnessCenter, (msg, color) => { });
            fitnessCenter.RegisterAccount(account1);

            var client2 = new Client("John", "Doe", 25, "USA", ClientLevel.Початковець);
            var account2 = new ClientAccount("johndoe", "newpassword", client2, fitnessCenter, (msg, color) => { });

            // Act
            fitnessCenter.RegisterAccount(account2);

            // Assert
            Assert.AreEqual(2, fitnessCenter.Accounts.Count);
        }

        [TestMethod]
        public void DisplayTrainings_ShouldFilterByDate()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("MyFitness");
            var hall = new Hall("Yoga Studio", 15);
            fitnessCenter.Halls.Add(hall);

            var trainer = new Trainer("Emily", "Clark", 35, "USA", 4000, ClientLevel.Професійний);
            var training1 = new Training(TrainingType.Йога, trainer, hall, DateTime.Today, 15);
            var training2 = new Training(TrainingType.Йога, trainer, hall, DateTime.Today.AddDays(1), 15);

            fitnessCenter.Trainings.Add(training1);
            fitnessCenter.Trainings.Add(training2);

            // Act
            var trainingsToday = fitnessCenter.Trainings.Where(t => t.Date.Date == DateTime.Today).ToList();

            // Assert
            Assert.AreEqual(1, trainingsToday.Count);
            Assert.IsTrue(trainingsToday.Contains(training1));
            Assert.IsFalse(trainingsToday.Contains(training2));
        }
    }
}
