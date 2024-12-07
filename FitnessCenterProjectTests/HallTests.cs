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
            var hall = new Hall("Main Hall", 20);

            // Assert
            Assert.AreEqual("Main Hall", hall.Name);
            Assert.AreEqual(20, hall.Capacity);
        }
    }
}
