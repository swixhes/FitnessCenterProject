using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Client : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public ClientLevel Level { get; set; }
        public Training Training { get; set; }

        public Client(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            throw new NotImplementedException();
        }
        public void UpdateLevel(ClientLevel newLevel)
        {
            throw new NotImplementedException();
        }
        public void PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
