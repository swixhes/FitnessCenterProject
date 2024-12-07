using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainerTests
    {
        [TestMethod]
        [DataRow("Paul", "Brown", 40, "UK", 5000, ClientLevel.Професійний)]
        [DataRow("Anna", "Smith", 35, "Canada", 4000, ClientLevel.Середній)]
        public void Trainer_ShouldInitializeCorrectly(string firstName, string lastName, int age, string nationality, int salary, ClientLevel level)
        {
            // Arrange
            var trainer = new Trainer(firstName, lastName, age, nationality, salary, level);

            // Assert
            Assert.AreEqual(firstName, trainer.FirstName);
            Assert.AreEqual(lastName, trainer.LastName);
            Assert.AreEqual(age, trainer.Age);
            Assert.AreEqual(nationality, trainer.Nationality);
            Assert.AreEqual(salary, trainer.Salary);
            Assert.AreEqual(level, trainer.ExpertiseLevel);
        }
    }
}
