using FitnessCenterProject;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class HallTests
    {
        [TestMethod]
        public void Hall_ShouldInitializeCorrectly()
        {
            // Arrange
            var hall = new Hall { Name = "Main Hall", Capacity = 50 };

            // Assert
            Assert.AreEqual("Main Hall", hall.Name);
            Assert.AreEqual(50, hall.Capacity);
        }

        [TestMethod]
        public void Hall_Capacity_ShouldBePositive()
        {
            // Arrange
            var hall = new Hall { Name = "Small Hall", Capacity = -10 };

            // Assert
            Assert.IsTrue(hall.Capacity >= 0, "Capacity should be a positive number.");
        }
    }
}
