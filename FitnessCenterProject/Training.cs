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

        public Training(TrainingType type, Trainer trainer, Hall hall, DateTime date, int maxParticipants, bool isIndividual = false)
        {
            Type = type;
            Trainer = trainer;
            Hall = hall;
            Date = date;
            MaxParticipants = maxParticipants;
            Clients = new List<Client>();
            IsIndividual = isIndividual;
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
                Console.WriteLine($"Клієнт {client.FirstName} вже зареєстрований на цей тренінг.");
                return false;
            }

            if (Clients.Count >= MaxParticipants)
                throw new InvalidOperationException("Цей тренінг повний. Більше клієнтів не можна додати.");

            Clients.Add(client);
            Console.WriteLine($"Клієнт {client.FirstName} доданий на це тренування.");
            return true;
        }
        public void PrintToDisplay()
        {
            if (IsIndividual)
            {
                Console.WriteLine($"Тренування: {Type}, Дата: {Date:dd.MM.yyyy}, Час: {Date:HH:mm}, Тренер: {Trainer.FirstName} {Trainer.LastName}, Індивідуальне тренування");
            }
            else
            {
                int spotsLeft = MaxParticipants - Clients.Count;
                Console.WriteLine($"Тренування: {Type}, Дата: {Date:dd.MM.yyyy}, Час: {Date:HH:mm}, Тренер: {Trainer.FirstName} {Trainer.LastName}, Місць лишилось: {spotsLeft}");
            }
        }
    }
}
