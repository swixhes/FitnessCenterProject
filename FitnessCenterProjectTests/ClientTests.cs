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
            public void CompareTo_ShouldSortClientsByLevel()
            {
                // Arrange
                var client1 = new Client("Alice", "Brown", 25, "USA", ClientLevel.Початковець);
                var client2 = new Client("Bob", "Smith", 30, "Canada", ClientLevel.Професійний);

                // Act
                var result = client1.CompareTo(client2);

                // Assert
                Assert.IsTrue(result < 0);
            }
        

    }
}
