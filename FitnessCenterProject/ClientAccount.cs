using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class ClientAccount : Account
    {
        public override string Username { get; set; }
        public override string Password { get; set; }
        public Client Client { get; set; }
        private FitnessCenter FitnessCenter { get; }

        // Делегат для повідомлень
        private readonly Action<string, ConsoleColor> onMessage;

        public ClientAccount(string username, string password, Client client, FitnessCenter fitnessCenter, Action<string, ConsoleColor> onMessage)
        {
            Username = username;
            Password = password;
            Client = client;
            FitnessCenter = fitnessCenter;
            this.onMessage = onMessage;
        }
        public override void Register()
        {
            if (ValidateInput(Username, Password))
            {
                if (!FitnessCenter.Accounts.Any(a => string.Equals(a.Username, Username, StringComparison.OrdinalIgnoreCase)))
                {
                    FitnessCenter.Accounts.Add(this);
                    //onMessage?.Invoke($"Клієнт {Client.FirstName} успішно зареєстрований з логіном: {Username}", ConsoleColor.Green);
                }
                else
                {
                    onMessage?.Invoke("Цей логін вже існує. Виберіть інший.", ConsoleColor.Red);
                }
            }
            else
            {
                onMessage?.Invoke("Невірний логін або пароль. Реєстрація не вдалася.", ConsoleColor.Red);
            }
        }
    }
}

