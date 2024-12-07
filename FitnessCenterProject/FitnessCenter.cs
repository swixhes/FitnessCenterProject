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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Цей логін вже використовується. Виберіть інший.");
                Console.ResetColor();
                return;
            }

            account.Register();
            Accounts.Add(account);
        }

        public void ChooseTraining(Client client)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nОберіть дату (від сьогодні до 7 днів включно):");
            Console.ResetColor();
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}");
            }

            int daySelection;
            while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("Невірний вибір. Спробуйте ще раз.");
                
            }
            Console.ResetColor();
            DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            DisplayTrainings(selectedDate);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Оберіть номер групового тренування або введіть 0 для індивідуального тренування:");
            Console.ResetColor();
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > Trainings.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Console.ResetColor();
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
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nКлієнт {client.FirstName} успішно зареєстрований на це групове тренування.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Клієнт вже зареєстрований на це тренування.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вибране тренування недоступне.");
                    Console.ResetColor();
                }
            }
        }
        private void ScheduleIndividualTraining(Client client, DateTime date)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nОберіть вид тренування:");
            Console.ResetColor();
            foreach (var type in Enum.GetValues(typeof(TrainingType)))
            {
                Console.WriteLine($"{(int)type + 1}. {type}");
            }

            int trainingTypeChoice;
            while (!int.TryParse(Console.ReadLine(), out trainingTypeChoice) || trainingTypeChoice < 1 || trainingTypeChoice > Enum.GetValues(typeof(TrainingType)).Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Console.ResetColor();
            }

            var chosenTrainingType = (TrainingType)(trainingTypeChoice - 1);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nВведіть час для індивідуального тренування (формат HH:mm):");
            Console.ResetColor();
            TimeSpan time;
            while (!TimeSpan.TryParse(Console.ReadLine(), out time))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний час. Спробуйте ще раз.");
                Console.ResetColor();
            }

            DateTime trainingDateTime = date.Add(time);

            var existingTraining = Trainings.FirstOrDefault(t => t.Date == trainingDateTime && t.Clients.Contains(client));
            if (existingTraining != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви вже зареєстровані на тренування в цей час.");
                Console.ResetColor();
                return;
            }

            var availableTrainers = Trainers.ToList();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nДоступні тренери:");
            Console.ResetColor();
            for (int i = 0; i < availableTrainers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableTrainers[i].FirstName} {availableTrainers[i].LastName}");
            }

            Console.Write("Оберіть тренера: ");
            int trainerChoice;
            while (!int.TryParse(Console.ReadLine(), out trainerChoice) || trainerChoice < 1 || trainerChoice > availableTrainers.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Console.ResetColor();
            }

            var chosenTrainer = availableTrainers[trainerChoice - 1];
            var hall = Halls.FirstOrDefault(h => h.Capacity > 0);
            if (hall == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Немає доступних залів для індивідуального тренування.");
                Console.ResetColor();
                return;

            }

            var training = new Training(chosenTrainingType, chosenTrainer, hall, trainingDateTime, 1, true); // Індивідуальне тренування
            Trainings.Add(training);
            training.AddClient(client);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Індивідуальне тренування успішно заброньоване на {training.Date:dd.MM.yyyy HH:mm} з тренером {chosenTrainer.FirstName} {chosenTrainer.LastName}.");
            Console.ResetColor();
        }
        public void DisplayTrainings(DateTime date)
        {
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine("| №   | Тренування      | Дата       | Час   | Тренер               | Місць залишилось |");
            Console.WriteLine("----------------------------------------------------------------------------------------");

            var availableTrainings = Trainings.Where(t => t.Date.Date == date.Date && !t.IsIndividual).ToList();
            int index = 1;

            foreach (var training in availableTrainings)
            {
                int spotsLeft = training.Hall.Capacity - training.Clients.Count;
                string trainerName = $"{training.Trainer.FirstName} {training.Trainer.LastName}".PadRight(20);
                Console.WriteLine($"| {index++,-3} | {training.Type,-15} | {training.Date:dd.MM.yyyy} | {training.Date:HH:mm} | {trainerName,-20} | {spotsLeft,-16} |");
            }

            if (!availableTrainings.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("| Немає доступних тренувань на вибрану дату.                                 |");
                Console.ResetColor();
            }

            Console.WriteLine("----------------------------------------------------------------------------------------\n");
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
        public void DisplayClientsWithTrainings()
        {
            Console.Clear();
            if (!Clients.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No registered clients.");
                Console.ResetColor();
                return;
            }

            var sortedClients = Clients.OrderBy(c => c).ToList();

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("| №   | Ім'я клієнта       | Рівень          | Тренування                                    |");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            int index = 1;
            foreach (var client in sortedClients)
            {
                var clientTrainings = Trainings.Where(t => t.Clients.Contains(client)).ToList();

                if (!clientTrainings.Any())
                {
                    Console.WriteLine($"| {index++,-3} | {client.FirstName,-18} | {client.Level,-15} | Немає тренувань                               |");
                }
                else
                {
                    foreach (var training in clientTrainings)
                    {
                        string trainingInfo = $"{training.Type}, {training.Date:dd.MM.yyyy HH:mm}, {training.Trainer.FirstName} {training.Trainer.LastName}";
                        Console.WriteLine($"| {index++,-3} | {client.FirstName,-18} | {client.Level,-15} | {trainingInfo,-45} |");
                    }
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");

        }


    }
}
