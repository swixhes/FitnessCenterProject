using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainerTests
    {
        [TestMethod]
        public void Trainer_ShouldInitializeCorrectly()
        {
            // Arrange
            var trainer = new Trainer("Jane", "Doe", 35, "USA", 2000);

            // Assert
            Assert.AreEqual("Jane", trainer.FirstName, "FirstName is not set correctly.");
            Assert.AreEqual("Doe", trainer.LastName, "LastName is not set correctly.");
            Assert.AreEqual(35, trainer.Age, "Age is not set correctly.");
            Assert.AreEqual("USA", trainer.Nationality, "Nationality is not set correctly.");
            Assert.AreEqual(2000, trainer.Salary, "Salary is not set correctly.");
        }

        [TestMethod]
        public void Salary_ShouldNotBeNegative()
        {
            // Arrange
            var trainer = new Trainer("Jane", "Doe", 35, "USA", -1000);

            // Assert
            Assert.IsTrue(trainer.Salary >= 0, "Salary should not be negative.");
        }

        [TestMethod]
        public void Age_ShouldBePositive()
        {
            // Arrange
            var trainer = new Trainer("Jane", "Doe", -25, "USA", 2000);

            // Assert
            Assert.IsTrue(trainer.Age > 0, "Age should be a positive number.");
        }

        [TestMethod]
        [DataRow("John", "Smith", 28, "Canada", 1500)]
        [DataRow("Anna", "Johnson", 40, "UK", 2500)]
        [DataRow("Michael", "Brown", 33, "Germany", 1800)]
        public void Trainer_ShouldInitializeWithMultipleData(
            string firstName, string lastName, int age, string nationality, int salary)
        {
            // Arrange
            var trainer = new Trainer(firstName, lastName, age, nationality, salary);

            // Assert
            Assert.AreEqual(firstName, trainer.FirstName, "FirstName is not set correctly.");
            Assert.AreEqual(lastName, trainer.LastName, "LastName is not set correctly.");
            Assert.AreEqual(age, trainer.Age, "Age is not set correctly.");
            Assert.AreEqual(nationality, trainer.Nationality, "Nationality is not set correctly.");
            Assert.AreEqual(salary, trainer.Salary, "Salary is not set correctly.");
        }

        [TestMethod]
        [DataRow(2000, 10, 2200)]
        [DataRow(1500, 20, 1800)]
        [DataRow(1000, 50, 1500)]
        public void Salary_ShouldIncreaseByPercentage(int initialSalary, int percentageIncrease, int expectedSalary)
        {
            // Arrange
            var trainer = new Trainer("John", "Doe", 30, "USA", initialSalary);

            // Act
            trainer.IncreaseSalary(percentageIncrease);

            // Assert
            Assert.AreEqual(expectedSalary, trainer.Salary, "Salary did not increase correctly.");
        }
    }
}
