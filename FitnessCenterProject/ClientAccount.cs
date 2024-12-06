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
        public ClientAccount(string username, string password, Client client, FitnessCenter fitnessCenter)
        {
            Username = username;
            Password = password;
            Client = client;
            FitnessCenter = fitnessCenter;
        }
        public override void Register()
        {
            if (ValidateInput(Username, Password))
            {
                if (!FitnessCenter.Accounts.Any(a => string.Equals(a.Username, Username, StringComparison.OrdinalIgnoreCase)))
                {
                    FitnessCenter.Accounts.Add(this);
                    Console.WriteLine($"Клієнт {Client.FirstName} успішно зареєстрований з логіном: {Username}");
                }
                else
                {
                    Console.WriteLine("Цей логін вже існує. Виберіть інший.");
                }
            }
            else
            {
                Console.WriteLine("Невірний логін або пароль. Реєстрація не вдалася.");
            }
        }
    }
}

