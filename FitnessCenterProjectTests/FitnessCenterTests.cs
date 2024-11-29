using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class FitnessCenterTests
    {
        [TestMethod]
        public void AddTraining_ShouldAddTrainingToList()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter
            {
                Name = "Elite Fitness",
                Halls = new List<Hall> { new Hall { Name = "Main Hall", Capacity = 20 } },
                Trainings = new List<Training>()
            };

            var trainer = new Trainer { FirstName = "John", LastName = "Doe", Age = 30, Nationality = "Ukraine", Salary = 1000 };
            var hall = new Hall { Name = "Main Hall", Capacity = 20 };
            var training = new Training(TrainingType.Cardio, trainer, hall, DateTime.Now);

            // Act
            fitnessCenter.Trainings.Add(training);

            // Assert
            Assert.Contains(training, fitnessCenter.Trainings);
        }

    }
}