using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Trainer : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public int Salary { get; set; }

        public Trainer(string firstName, string lastName, int age, string nationality, int salary)
        {
            throw new NotImplementedException();
        }

        public void PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
