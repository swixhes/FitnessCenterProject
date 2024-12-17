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

        // Делегат для повідомлень
        private readonly Action<string, ConsoleColor> onMessage;
        public TrainerAccount(string username, string password, Trainer trainer, Action<string, ConsoleColor> onMessage)
        {
            Username = username;
            Password = password;
            Trainer = trainer;
            this.onMessage = onMessage;
        }
        public override void Register()
        {
            if (ValidateInput(Username, Password))
            {
                //onMessage?.Invoke($"\nТренер {Trainer.FirstName} успішно зареєстрований логіном: {Username}", ConsoleColor.Green);
                onMessage?.Invoke($"\nВи успішно влаштовані на випробувальний термін.", ConsoleColor.Cyan);
            }
            else
            {
                onMessage?.Invoke("Недійсне ім'я користувача або пароль. Помилка реєстрації.", ConsoleColor.Red);
            }
        }
    }
}
