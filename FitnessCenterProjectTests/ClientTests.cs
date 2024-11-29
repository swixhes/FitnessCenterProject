using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        [DataRow("John", "Doe", 25, "USA", ClientLevel.Beginner)]
        [DataRow("Anna", "Smith", 30, "UK", ClientLevel.Intermediate)]
        [DataRow("Michael", "Brown", 45, "Canada", ClientLevel.Professional)]
        public void Client_ShouldInitializeCorrectly(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Arrange
            var client = new Client(firstName, lastName, age, nationality, level);

            // Assert
            Assert.AreEqual(firstName, client.FirstName);
            Assert.AreEqual(lastName, client.LastName);
            Assert.AreEqual(age, client.Age);
            Assert.AreEqual(nationality, client.Nationality);
            Assert.AreEqual(level, client.Level);
        }
        [TestMethod]
        public void UpdateLevel_ShouldChangeClientLevel()
        {
            // Arrange
            var client = new Client("John", "Doe", 25, "USA", ClientLevel.Beginner);

            // Act
            client.UpdateLevel(ClientLevel.Professional);

            // Assert
            Assert.AreEqual(ClientLevel.Professional, client.Level);
        }
        [TestMethod]
        public void Age_ShouldBePositive()
        {
            // Arrange
            var client = new Client("John", "Doe", -25, "USA", ClientLevel.Beginner);

            // Assert
            Assert.IsTrue(client.Age > 0, "Age should be a positive number.");
        }

        [TestMethod]
        [DataRow("", "Doe", 25, "USA", ClientLevel.Beginner)]
        [DataRow(null, "Doe", 25, "USA", ClientLevel.Beginner)]
        public void FirstName_ShouldNotBeEmptyOrNull(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(firstName, lastName, age, nationality, level), "FirstName should not be empty or null.");
        }

        [TestMethod]
        [DataRow("John", "", 25, "USA", ClientLevel.Beginner)]
        [DataRow("John", null, 25, "USA", ClientLevel.Beginner)]
        public void LastName_ShouldNotBeEmptyOrNull(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(firstName, lastName, age, nationality, level), "LastName should not be empty or null.");
        }

        [TestMethod]
        [DataRow("John", "Doe", 25, "USA", (ClientLevel)(-1))]
        [DataRow("Anna", "Smith", 30, "UK", (ClientLevel)100)]
        public void Level_ShouldBeValid(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Client(firstName, lastName, age, nationality, level), "Level should be a valid enum value.");
        }
    }
}
