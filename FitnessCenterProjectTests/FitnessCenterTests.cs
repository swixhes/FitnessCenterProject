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
            var fitnessCenter = new FitnessCenter
            {
                Name = "Elite Fitness",
                Halls = new List<Hall>(),
                Trainings = new List<Training>()
            };

            // Assert
            Assert.AreEqual("Elite Fitness", fitnessCenter.Name);
            Assert.AreEqual(0, fitnessCenter.Halls.Count);
            Assert.AreEqual(0, fitnessCenter.Trainings.Count);
        }

        [TestMethod]
        [DataRow("Main Hall", 30)]
        [DataRow("Cardio Hall", 25)]
        [DataRow("Yoga Hall", 20)]
        public void AddHall_ShouldAddHallToList(string hallName, int capacity)
        {
            // Arrange
            var fitnessCenter = new FitnessCenter
            {
                Name = "Elite Fitness",
                Halls = new List<Hall>()
            };
            var hall = new Hall { Name = hallName, Capacity = capacity };

            // Act
            fitnessCenter.Halls.Add(hall);

            // Assert
            CollectionAssert.Contains(fitnessCenter.Halls, hall);
        }

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

            var trainer = new Trainer("John", "Doe", 30, "Ukraine", 1000);
            var hall = new Hall { Name = "Main Hall", Capacity = 20 };
            var training = new Training(TrainingType.Cardio, trainer, hall, DateTime.Now);

            // Act
            fitnessCenter.Trainings.Add(training);

            // Assert
            CollectionAssert.Contains(fitnessCenter.Trainings, training);
        }
    }
}
