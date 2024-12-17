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

        // Делегат для виводу повідомлень
        public Action<string, ConsoleColor> OnMessage;

        // Події
        public event Action<Client> OnClientRegistered;
        public event Action<Trainer> OnTrainerRegistered;

        public void RegisterAccount(Account account)
        {
            if (Accounts.Any(a => string.Equals(a.Username, account.Username, StringComparison.OrdinalIgnoreCase)))
            {
                OnMessage?.Invoke("Цей логін вже використовується. Виберіть інший.", ConsoleColor.Red);
                return;
            }

            account.Register();
            Accounts.Add(account);

            // Відповідно до типу облікового запису викликаємо події
            if (account is ClientAccount clientAccount)
            {
                Clients.Add(clientAccount.Client);
                OnClientRegistered?.Invoke(clientAccount.Client);
            }
            else if (account is TrainerAccount trainerAccount)
            {
                Trainers.Add(trainerAccount.Trainer);
                OnTrainerRegistered?.Invoke(trainerAccount.Trainer);
            }
        }

        public void ChooseTraining(Client client)
        {
            OnMessage?.Invoke("\nОберіть дату (від сьогодні до 7 днів включно):", ConsoleColor.DarkMagenta);
            for (int i = 0; i < 7; i++)
            {
                OnMessage?.Invoke($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}", ConsoleColor.Black);
            }

            int daySelection;
            while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
            {
                OnMessage?.Invoke("Невірний вибір. Спробуйте ще раз.", ConsoleColor.Red);
            }

            DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            DisplayTrainings(selectedDate);

            OnMessage?.Invoke("Оберіть номер групового тренування або введіть 0 для індивідуального тренування:", ConsoleColor.Black);
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > Trainings.Count)
            {
                OnMessage?.Invoke("Невірний вибір. Спробуйте ще раз.", ConsoleColor.Red);
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
                            OnMessage?.Invoke($"\nКлієнт {client.FirstName} успішно зареєстрований на це групове тренування.", ConsoleColor.Green);
                        }
                    }
                    else
                    {
                        OnMessage?.Invoke("Клієнт вже зареєстрований на це тренування.", ConsoleColor.Red);
                    }
                }
                else
                {
                    OnMessage?.Invoke("Вибране тренування недоступне.", ConsoleColor.Red);
                }
            }
        }
        private void ScheduleIndividualTraining(Client client, DateTime date)
        {
            OnMessage?.Invoke("\nОберіть вид тренування:", ConsoleColor.DarkMagenta);
            foreach (var type in Enum.GetValues(typeof(TrainingType)))
            {
                OnMessage?.Invoke($"{(int)type + 1}. {type}", ConsoleColor.Black);
            }

            int trainingTypeChoice;
            while (!int.TryParse(Console.ReadLine(), out trainingTypeChoice) || trainingTypeChoice < 1 || trainingTypeChoice > Enum.GetValues(typeof(TrainingType)).Length)
            {
                OnMessage?.Invoke("Невірний вибір. Спробуйте ще раз.", ConsoleColor.Red);
            }

            var chosenTrainingType = (TrainingType)(trainingTypeChoice - 1);
            OnMessage?.Invoke("\nВведіть час для індивідуального тренування (формат HH:mm):", ConsoleColor.DarkMagenta);

            TimeSpan time;
            while (!TimeSpan.TryParse(Console.ReadLine(), out time) || time.Hours < 8 || time.Hours >= 21)
            {
                OnMessage?.Invoke("Фітнес центр відкритий з 08:00 до 21:00. Введіть коректний час.", ConsoleColor.Red);
            }

            DateTime trainingDateTime = date.Add(time);

            var existingTraining = Trainings.FirstOrDefault(t => t.Date == trainingDateTime && t.Clients.Contains(client));
            if (existingTraining != null)
            {
                OnMessage?.Invoke("Ви вже зареєстровані на тренування в цей час.", ConsoleColor.Red);
                return;
            }

            var availableTrainers = Trainers.ToList();
            OnMessage?.Invoke("\nДоступні тренери:", ConsoleColor.DarkMagenta);
            for (int i = 0; i < availableTrainers.Count; i++)
            {
                OnMessage?.Invoke($"{i + 1}. {availableTrainers[i].FirstName} {availableTrainers[i].LastName}", ConsoleColor.Black);
            }

            OnMessage?.Invoke("Оберіть тренера: ", ConsoleColor.Black);
            int trainerChoice;
            while (!int.TryParse(Console.ReadLine(), out trainerChoice) || trainerChoice < 1 || trainerChoice > availableTrainers.Count)
            {
                OnMessage?.Invoke("Невірний вибір. Спробуйте ще раз.", ConsoleColor.Red);
            }

            var chosenTrainer = availableTrainers[trainerChoice - 1];
            var hall = Halls.FirstOrDefault(h => h.Capacity > 0);
            if (hall == null)
            {
                OnMessage?.Invoke("Немає доступних залів для індивідуального тренування.", ConsoleColor.Red);
                return;

            }

            var training = new Training(chosenTrainingType, chosenTrainer, hall, trainingDateTime, 1, true);
            Trainings.Add(training);
            training.AddClient(client);
            OnMessage?.Invoke($"Індивідуальне тренування успішно заброньоване на {training.Date:dd.MM.yyyy HH:mm} з тренером {chosenTrainer.FirstName} {chosenTrainer.LastName}.", ConsoleColor.DarkGreen);
        }
        public void DisplayTrainings(DateTime date)
        {
            OnMessage?.Invoke("----------------------------------------------------------------------------------------", ConsoleColor.Black);
            OnMessage?.Invoke("| №   | Тренування      | Дата       | Час   | Тренер               | Місць залишилось |", ConsoleColor.Black);
            OnMessage?.Invoke("----------------------------------------------------------------------------------------", ConsoleColor.Black);

            var availableTrainings = Trainings.Where(t => t.Date.Date == date.Date && !t.IsIndividual).ToList();
            int index = 1;

            foreach (var training in availableTrainings)
            {
                int spotsLeft = training.Hall.Capacity - training.Clients.Count;
                string trainerName = $"{training.Trainer.FirstName} {training.Trainer.LastName}".PadRight(20);
                OnMessage?.Invoke($"| {index++,-3} | {training.Type,-15} | {training.Date:dd.MM.yyyy} | {training.Date:HH:mm} | {trainerName,-20} | {spotsLeft,-16} |", ConsoleColor.Black);
            }

            if (!availableTrainings.Any())
            {
                OnMessage?.Invoke("| Немає доступних тренувань на вибрану дату.                                 |", ConsoleColor.Red);
            }

            OnMessage?.Invoke("----------------------------------------------------------------------------------------\n", ConsoleColor.Black);
        }


        public void PrintToDisplay()
        {
            OnMessage?.Invoke($"Fitness Center: {Name}", ConsoleColor.Cyan);
            OnMessage?.Invoke("Clients:", ConsoleColor.Black);
            foreach (var client in Clients)
            {
                client.PrintToDisplay();
            }

            OnMessage?.Invoke("Trainers:", ConsoleColor.Black);
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
                OnMessage?.Invoke("Немає зареєстрованих клієнтів.", ConsoleColor.Red);
                return;
            }

            OnMessage?.Invoke("------------------------------------------------------------------------------------------------------", ConsoleColor.Black);
            OnMessage?.Invoke("| №   | Ім'я клієнта       | Вік   | Рівень          | Тренування                                    |", ConsoleColor.Black);
            OnMessage?.Invoke("------------------------------------------------------------------------------------------------------", ConsoleColor.Black);

            int index = 1;
            foreach (var client in Clients.OrderBy(c => c))
            {
                var clientTrainings = Trainings.Where(t => t.Clients.Contains(client)).ToList();

                if (!clientTrainings.Any())
                {
                    OnMessage?.Invoke($"| {index++,-3} | {client.FirstName,-18} | {client.Age,-5} | {client.Level,-15} | Немає тренувань                               |", ConsoleColor.Black);
                }
                else
                {
                    foreach (var training in clientTrainings)
                    {
                        string trainingInfo = $"{training.Type}, {training.Date:dd.MM.yyyy HH:mm}, {training.Trainer.FirstName} {training.Trainer.LastName}";
                        OnMessage?.Invoke($"| {index++,-3} | {client.FirstName,-18} | {client.Age,-5} | {client.Level,-15} | {trainingInfo,-45} |", ConsoleColor.Black);
                    }
                }
            }

            OnMessage?.Invoke("------------------------------------------------------------------------------------------------------", ConsoleColor.Black);
        }


    }
}
