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
            var trainer = new Trainer("Олена", "Коваль", 35, "Україна", 2000);

            // Assert
            Assert.AreEqual("Олена", trainer.FirstName, "FirstName is not set correctly.");
            Assert.AreEqual("Коваль", trainer.LastName, "LastName is not set correctly.");
            Assert.AreEqual(35, trainer.Age, "Age is not set correctly.");
            Assert.AreEqual("Україна", trainer.Nationality, "Nationality is not set correctly.");
            Assert.AreEqual(2000, trainer.Salary, "Salary is not set correctly.");
        }

        [TestMethod]
        [DataRow(-1000)]
        [DataRow(0)]
        public void Salary_ShouldNotBeNegative(int salary)
        {
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trainer("Олена", "Коваль", 35, "Україна", salary), "Salary should not be negative or zero.");
        }

        [TestMethod]
        [DataRow(-1)]
        public void Age_ShouldBePositive(int age)
        {
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trainer("Олена", "Коваль", age, "Україна", 2000), "Age should be a positive number.");
        }

        
    }
}
