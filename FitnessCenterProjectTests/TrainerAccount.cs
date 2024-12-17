using FitnessCenterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class TrainerAccountTests
    {
        [TestMethod]
        public void ValidateInput_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var trainer = new Trainer("Іван", "Іванов", 30, "Україна", 20000, ClientLevel.Середній);
            var trainerAccount = new TrainerAccount("trainer123", "securePass", trainer, (msg, color) => { });

            // Act
            var isValid = trainerAccount.ValidateInput(trainerAccount.Username, trainerAccount.Password);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateInput_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var trainer = new Trainer("Іван", "Іванов", 30, "Україна", 20000, ClientLevel.Середній);
            var trainerAccount = new TrainerAccount("", "short", trainer, (msg, color) => { });

            // Act
            var isValid = trainerAccount.ValidateInput(trainerAccount.Username, trainerAccount.Password);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
