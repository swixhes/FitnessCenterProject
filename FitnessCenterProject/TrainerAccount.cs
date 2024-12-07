using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class TrainerAccount : Account
    {
        public override string Username { get; set; }
        public override string Password { get; set; }
        public Trainer Trainer { get; set; }

        public TrainerAccount(string username, string password, Trainer trainer)
        {
            Username = username;
            Password = password;
            Trainer = trainer;
        }
        public override void Register()
        {
            if (ValidateInput(Username, Password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nТренер {Trainer.FirstName} успішно зареєстровано логіном: {Username}");
                Console.ResetColor();
                Console.WriteLine($"\nВи успішно влаштовані на випробувальний термін.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Недійсне ім'я користувача або пароль. Помилка реєстрації.");
                Console.ResetColor();
            }
        }
    }
}
