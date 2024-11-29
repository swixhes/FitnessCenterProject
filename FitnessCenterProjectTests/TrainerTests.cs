using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainerTests
    {
        public void Salary_ShouldBeSetCorrectly()
        {
            // Arrange
            var trainer = new Trainer
            {
                FirstName = "Jane",
                LastName = "Doe",
                Age = 35,
                Nationality = "USA",
                Salary = 2000
            };

            // Assert
            Assert.Equal(2000, trainer.Salary);
            Assert.Equal("Jane", trainer.FirstName);
            Assert.Equal(35, trainer.Age);
        }
    }

}