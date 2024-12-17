using FitnessCenterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProjectTests
{
    [TestClass]
    public class ClientAccountTests
    {
        [TestMethod]
        public void Register_ValidClient_AddsAccountToFitnessCenter()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("Best Gym");
            var client = new Client("Олександр", "Коваленко", 25, "Україна", ClientLevel.Початковець);
            var clientAccount = new ClientAccount("client123", "securePass", client, fitnessCenter, (msg, color) => { });

            // Act
            clientAccount.Register();

            // Assert
            Assert.AreEqual(1, fitnessCenter.Accounts.Count);
            Assert.AreEqual(clientAccount, fitnessCenter.Accounts[0]);
        }

        [TestMethod]
        public void Register_DuplicateUsername_ShowsError()
        {
            // Arrange
            var fitnessCenter = new FitnessCenter("Best Gym");
            var client1 = new Client("Олександр", "Коваленко", 25, "Україна", ClientLevel.Початковець);
            var client2 = new Client("Анна", "Іваненко", 22, "Україна", ClientLevel.Професійний);

            var clientAccount1 = new ClientAccount("client123", "securePass1", client1, fitnessCenter, (msg, color) => { });
            var clientAccount2 = new ClientAccount("client123", "securePass2", client2, fitnessCenter, (msg, color) => { });

            // Act
            clientAccount1.Register();
            clientAccount2.Register();

            // Assert
            Assert.AreEqual(1, fitnessCenter.Accounts.Count);
            Assert.AreEqual(clientAccount1, fitnessCenter.Accounts[0]);
        }
    }
}
