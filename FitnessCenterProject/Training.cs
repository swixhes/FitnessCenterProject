using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Training : IPrintable
    {
        public TrainingType Type { get; set; }
        public Trainer Trainer { get; set; }
        public Hall Hall { get; set; }
        public DateTime Date { get; set; }
        public List<Client> Clients { get; set; }
        public int MaxParticipants { get; set; }
        public bool IsIndividual { get; set; }

        // Делегат для виводу повідомлень
        private readonly Action<string, ConsoleColor> OnMessage;

        public Training(TrainingType type, Trainer trainer, Hall hall, DateTime date, int maxParticipants, bool isIndividual = false, Action<string, ConsoleColor> onMessage = null)
        {
            Type = type;
            Trainer = trainer;
            Hall = hall;
            Date = date;
            MaxParticipants = maxParticipants;
            Clients = new List<Client>();
            IsIndividual = isIndividual;
            OnMessage = onMessage;
        }

        public Training(TrainingType strength, Trainer chosenTrainer, Hall hall, DateTime dateTime)
        {
            Hall = hall;
            Clients = new List<Client>();
        }
        public bool AddClient(Client client)
        {
            if (Clients.Contains(client))
            {
                OnMessage?.Invoke($"Клієнт {client.FirstName} вже зареєстрований на це тренування.", ConsoleColor.Red);
                return false;
            }

            if (Clients.Count >= MaxParticipants)
            {
                OnMessage?.Invoke("Цей тренінг повний. Більше клієнтів не можна додати.", ConsoleColor.Red);
                throw new InvalidOperationException("Цей тренінг повний.");
            }

            Clients.Add(client);
            OnMessage?.Invoke($"Клієнт {client.FirstName} доданий на це тренування.", ConsoleColor.Green);
            return true;
        }

        public void PrintToDisplay()
        {
            if (IsIndividual)
            {
                OnMessage?.Invoke($"Тренування: {Type}, Дата: {Date:dd.MM.yyyy}, Час: {Date:HH:mm}, Тренер: {Trainer.FirstName} {Trainer.LastName}, Індивідуальне тренування", ConsoleColor.Black);
            }
            else
            {
                int spotsLeft = MaxParticipants - Clients.Count;
                OnMessage?.Invoke($"Тренування: {Type}, Дата: {Date:dd.MM.yyyy}, Час: {Date:HH:mm}, Тренер: {Trainer.FirstName} {Trainer.LastName}, Місць лишилось: {spotsLeft}", ConsoleColor.Black);
            }
        }
    }
}
