using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        [DataRow("Іван", "Петров", 25, "Україна", ClientLevel.Початковець)]
        [DataRow("Анна", "Сидорова", 30, "Великобританія", ClientLevel.Середній)]
        [DataRow("Михайло", "Білий", 45, "Канада", ClientLevel.Професійний)]
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
        [DataRow(-1)]
        [DataRow(0)]
        public void Age_ShouldBePositive(int age)
        {
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Client("Іван", "Петров", age, "Україна", ClientLevel.Початковець), "Age should be a positive number.");
        }

        [TestMethod]
        [DataRow("", "Петров", 25, "Україна", ClientLevel.Початковець)]
        [DataRow(null, "Петров", 25, "Україна", ClientLevel.Початковець)]
        public void FirstName_ShouldNotBeEmptyOrNull(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(firstName, lastName, age, nationality, level), "FirstName should not be empty or null.");
        }

        [TestMethod]
        [DataRow("Іван", "", 25, "Україна", ClientLevel.Початковець)]
        [DataRow("Іван", null, 25, "Україна", ClientLevel.Початковець)]
        public void LastName_ShouldNotBeEmptyOrNull(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(firstName, lastName, age, nationality, level), "LastName should not be empty or null.");
        }

        [TestMethod]
        [DataRow("Іван", "Петров", 25, "Україна", (ClientLevel)(-1))]
        [DataRow("Анна", "Сидорова", 30, "Україна", (ClientLevel)100)]
        public void Level_ShouldBeValid(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Client(firstName, lastName, age, nationality, level), "Level should be a valid enum value.");
        }
    }
}
