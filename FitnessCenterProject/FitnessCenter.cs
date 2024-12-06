using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class FitnessCenter : IPrintable
    {
        public string Name { get; set; }
        public List<Hall> Halls { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Client> Clients { get; set; }
        public List<Trainer> Trainers { get; set; }
        public List<Account> Accounts { get; set; }

        public FitnessCenter(string name)
        {
            Name = name;
            Halls = new List<Hall>();
            Trainings = new List<Training>();
            Clients = new List<Client>();
            Trainers = new List<Trainer>();
            Accounts = new List<Account>();
        }

        public void RegisterAccount(Account account)
        {
            if (Accounts.Any(a => string.Equals(a.Username, account.Username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Цей логін вже використовується. Виберіть інший.");
                return;
            }

            account.Register();
            Accounts.Add(account);
        }

        public void ChooseTraining(Client client)
        {
            Console.WriteLine("Оберіть дату (від сьогодні до 7 днів включно):");
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}");
            }

            int daySelection;
            while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
                throw new ArgumentException("Невірний вибір. Спробуйте ще раз.");
           
            DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            DisplayTrainings(selectedDate);

            Console.WriteLine("Оберіть номер групового тренування або введіть 0 для індивідуального тренування:");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > Trainings.Count)
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }

            if (choice == 0)
            {
                ScheduleIndividualTraining(client, selectedDate);
            }
            else
            {
                var availableTrainings = Trainings.Where(t => t.Date.Date == selectedDate.Date).ToList();
                if (choice > 0 && choice <= availableTrainings.Count)
                {
                    var selectedTraining = availableTrainings[choice - 1];

                    if (!selectedTraining.Clients.Contains(client))
                    {
                        if (selectedTraining.AddClient(client))
                        {
                            Console.WriteLine("Клієнт успішно зареєстрований на групове тренування.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Клієнт вже зареєстрований на це тренування.");
                    }
                }
                else
                {
                    Console.WriteLine("Вибране тренування недоступне.");
                }
            }
        }
        private void ScheduleIndividualTraining(Client client, DateTime date)
        {
            Console.WriteLine("Оберіть вид тренування:");
            foreach (var type in Enum.GetValues(typeof(TrainingType)))
            {
                Console.WriteLine($"{(int)type + 1}. {type}");
            }

            int trainingTypeChoice;
            while (!int.TryParse(Console.ReadLine(), out trainingTypeChoice) || trainingTypeChoice < 1 || trainingTypeChoice > Enum.GetValues(typeof(TrainingType)).Length)
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }

            var chosenTrainingType = (TrainingType)(trainingTypeChoice - 1);

            Console.WriteLine("Введіть час для індивідуального тренування (формат HH:mm):");
            TimeSpan time;
            while (!TimeSpan.TryParse(Console.ReadLine(), out time))
            {
                Console.WriteLine("Невірний час. Спробуйте ще раз.");
            }

            DateTime trainingDateTime = date.Add(time);

            var existingTraining = Trainings.FirstOrDefault(t => t.Date == trainingDateTime && t.Clients.Contains(client));
            if (existingTraining != null)
            {
                Console.WriteLine("Ви вже зареєстровані на тренування в цей час.");
                return;
            }

            var availableTrainers = Trainers.ToList();
            Console.WriteLine("Доступні тренери:");
            for (int i = 0; i < availableTrainers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableTrainers[i].FirstName} {availableTrainers[i].LastName}");
            }

            Console.Write("Оберіть тренера: ");
            int trainerChoice;
            while (!int.TryParse(Console.ReadLine(), out trainerChoice) || trainerChoice < 1 || trainerChoice > availableTrainers.Count)
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }

            var chosenTrainer = availableTrainers[trainerChoice - 1];
            var hall = Halls.FirstOrDefault(h => h.Capacity > 0);
            if (hall == null)
            {
                Console.WriteLine("Немає доступних залів для індивідуального тренування.");
                return;
            }

            var training = new Training(chosenTrainingType, chosenTrainer, hall, trainingDateTime, 1, true); // Індивідуальне тренування
            Trainings.Add(training);
            training.AddClient(client);

            Console.WriteLine($"Індивідуальне тренування успішно заброньоване на {training.Date:dd.MM.yyyy HH:mm} з тренером {chosenTrainer.FirstName} {chosenTrainer.LastName}.");
        }
        public void DisplayTrainings(DateTime date)
        {
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("| № | Тренування       | Дата       | Час    | Тренер        | Місць залишилось |");
            Console.WriteLine("-----------------------------------------------------------------------------");

            var availableTrainings = Trainings.Where(t => t.Date.Date == date.Date && !t.IsIndividual).ToList();
            int index = 1;

            foreach (var training in availableTrainings)
            {
                int spotsLeft = training.Hall.Capacity - training.Clients.Count;
                Console.WriteLine($"| {index++,-2} | {training.Type,-15} | {training.Date:dd.MM.yyyy} | {training.Date:HH:mm} | {training.Trainer.FirstName} {training.Trainer.LastName,-10} | {spotsLeft,-16} |");
            }

            if (!availableTrainings.Any())
            {
                Console.WriteLine("| Немає доступних тренувань на вибрану дату.                               |");
            }

            Console.WriteLine("-----------------------------------------------------------------------------\n");
        }

        public void PrintToDisplay()
        {
            Console.WriteLine($"Fitness Center: {Name}");
            Console.WriteLine("Clients:");
            foreach (var client in Clients)
            {
                client.PrintToDisplay();
            }

            Console.WriteLine("Trainers:");
            foreach (var trainer in Trainers)
            {
                trainer.PrintToDisplay();
            }
        }

    }
}
