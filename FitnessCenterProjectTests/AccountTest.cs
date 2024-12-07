using FitnessCenterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        [DataRow("validUser", "validPass123", true)]
        [DataRow("", "validPass123", false)]
        [DataRow("validUser", "", false)]
        [DataRow("validUser", "short", false)]
        public void ValidateInput_ShouldValidateUsernameAndPassword(string username, string password, bool expected)
        {
            // Arrange
            var account = new TrainerAccount(username, password, new Trainer("Test", "Trainer", 30, "USA", 1000, ClientLevel.Середній));

            // Act
            var result = account.ValidateInput(username, password);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
