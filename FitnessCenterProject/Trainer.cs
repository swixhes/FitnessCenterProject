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
        public ClientLevel ExpertiseLevel { get; set; }

        public Trainer(string firstName, string lastName, int age, string nationality, int salary, ClientLevel expertiseLevel)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Nationality = nationality;
            Salary = salary;
            ExpertiseLevel = expertiseLevel;
        }
        public void PrintToDisplay()
        {
            Console.WriteLine($"{FirstName} {LastName}, Вік: {Age}, Зарплатня: {Salary}, Спеціалізація: {ExpertiseLevel}");
        }
    }
}
