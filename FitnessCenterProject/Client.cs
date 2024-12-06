using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Client : IPerson, IComparable<Client>, IPrintable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public ClientLevel Level { get; set; }
        public Training Training { get; set; }
        public TrainingType PreferredTrainingType { get; internal set; }
        public Client(string firstName, string lastName, int age, string nationality, ClientLevel level)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Nationality = nationality;
            Level = level;
        }
        public int CompareTo(Client other)
        {
            return Level.CompareTo(other.Level);
        }
        public void PrintToDisplay()
        {
            Console.WriteLine($"Клієнт: {FirstName} {LastName}, Вік: {Age}, Рівень: {Level}");
        }
    }
}
