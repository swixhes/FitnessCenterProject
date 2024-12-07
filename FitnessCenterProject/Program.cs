using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitnessCenterProject
{
    class Program
    {
        static FitnessCenter fitnessCenter = new FitnessCenter("MyFitness");

        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            InitializeFitnessCenter();
            MainMenu();
        }

        static void InitializeFitnessCenter()
        {
            // Додавання залів
            fitnessCenter.Halls.Add(new Hall("Кардіо зал", 20));
            fitnessCenter.Halls.Add(new Hall("Силовий зал", 15));
            fitnessCenter.Halls.Add(new Hall("Студія йоги", 10));
            fitnessCenter.Halls.Add(new Hall("Басейн", 25));
            fitnessCenter.Halls.Add(new Hall("Зал аеробіки", 18));
            fitnessCenter.Halls.Add(new Hall("Студія пілатесу", 12));

            // Додавання тренерів
            fitnessCenter.Trainers.Add(new Trainer("John", "Doe", 28, "USA", 3000, ClientLevel.Початковець));
            fitnessCenter.Trainers.Add(new Trainer("Anna", "Smith", 35, "Canada", 4000, ClientLevel.Середній));
            fitnessCenter.Trainers.Add(new Trainer("Paul", "Brown", 50, "UK", 5000, ClientLevel.Професійний));
            fitnessCenter.Trainers.Add(new Trainer("Mary", "Johnson", 30, "USA", 3500, ClientLevel.Початковець));
            fitnessCenter.Trainers.Add(new Trainer("Robert", "Taylor", 45, "Canada", 4500, ClientLevel.Середній));
            fitnessCenter.Trainers.Add(new Trainer("Emily", "Clark", 40, "UK", 4200, ClientLevel.Професійний));

            // Додавання початкових тренувань
            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Кардіо,
                fitnessCenter.Trainers[0],
                fitnessCenter.Halls.First(h => h.Name == "Кардіо зал"),
                new DateTime(2024, 12, 6, 12, 0, 0),
                20 // Максимальна кількість місць для цього тренування
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Силові,
                fitnessCenter.Trainers[1],
                fitnessCenter.Halls.First(h => h.Name == "Силовий зал"),
                new DateTime(2024, 12, 6, 14, 0, 0),
                15
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Йога,
                fitnessCenter.Trainers[2],
                fitnessCenter.Halls.First(h => h.Name == "Студія йоги"),
                new DateTime(2024, 12, 7, 10, 0, 0),
                10
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Плавання,
                fitnessCenter.Trainers[3],
                fitnessCenter.Halls.First(h => h.Name == "Басейн"),
                new DateTime(2024, 12, 8, 16, 0, 0),
                25
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Аеробіка,
                fitnessCenter.Trainers[4],
                fitnessCenter.Halls.First(h => h.Name == "Зал аеробіки"),
                new DateTime(2024, 12, 9, 18, 0, 0),
                18
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Пілатес,
                fitnessCenter.Trainers[5],
                fitnessCenter.Halls.First(h => h.Name == "Студія пілатесу"),
                new DateTime(2024, 12, 10, 9, 0, 0),
                12
            ));
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nЗапрошуємо до MyFitness!");
                Console.ResetColor();
                Console.WriteLine("1. Реєстрація клієнта");
                Console.WriteLine("2. Резюме тренера");
                Console.WriteLine("3. Вхід");
                Console.WriteLine("4. Розклад фітнес центру");
                Console.WriteLine("5. Переглянути клієнтів і тренінги");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть варіант: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterClient();
                        break;
                    case "2":
                        ApplyAsTrainer();
                        break;
                    case "3":
                        Login();
                        break;
                    case "4":
                        ViewSchedule();
                        break;
                    case "5":
                        fitnessCenter.DisplayClientsWithTrainings();
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Допобачення!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Недійсний варіант. Спробуйте знову.");
                        Console.ResetColor();
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися до меню...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        static void RegisterClient()
        {
            try
            {
                Console.Clear();
                Console.Write("Введіть Ім'я користувача: ");
                string username = Console.ReadLine();
                Console.Write("Введіть Пароль: ");
                string password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    throw new InvalidOperationException("Ім'я користувача та пароль не можуть бути пустими.");

                if (fitnessCenter.Accounts.Any(a => a.Username == username))
                    throw new InvalidOperationException("Це ім'я користувача вже існує. Виберіть інший.");

                Console.Write("Введіть ім'я: ");
                string firstName = Console.ReadLine();
                Console.Write("Введіть прізвище: ");
                string lastName = Console.ReadLine();
                Console.Write("Введіть вік: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                    throw new ArgumentException("Недійсний вік. Вік має бути додатним числом.");
                Console.Write("Введіть національність: ");
                string nationality = Console.ReadLine();

                Console.WriteLine("Виберіть рівень навчання: 1. Початківець, 2. Середній, 3. Професіонал");
                int levelChoice;
                while (!int.TryParse(Console.ReadLine(), out levelChoice) || levelChoice < 1 || levelChoice > 3)
                {
                    Console.WriteLine("Не правильний вибір. Спробуйте знову.");
                }

                ClientLevel level = (ClientLevel)(levelChoice - 1);

                var client = new Client(firstName, lastName, age, nationality, level);
                var account = new ClientAccount(username, password, client, fitnessCenter);

                fitnessCenter.RegisterAccount(account);

                fitnessCenter.Clients.Add(client);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Клієнт {firstName} успішно зареєстрований з логіном: {username}\n");
                Console.ResetColor();

                fitnessCenter.ChooseTraining(client);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка введення: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Несподівана помилка: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ApplyAsTrainer()
        {
            try
            {
                Console.Clear();
                Console.Write("Введіть Ім'я користувача: ");
                string username = Console.ReadLine();
                Console.Write("Введіть Пароль: ");
                string password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    throw new InvalidOperationException("Ім'я користувача та пароль не можуть бути пустими.");

                if (fitnessCenter.Accounts.Any(a => a.Username == username))
                    throw new InvalidOperationException("Це ім'я користувача вже існує. Виберіть інший.");

                Console.Write("Введіть ім'я: ");
                string firstName = Console.ReadLine();
                Console.Write("Введіть прізвище: ");
                string lastName = Console.ReadLine();
                Console.Write("Введіть вік: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                    throw new ArgumentException("Недійсний вік. Вік має бути додатним числом.");

                Console.Write("Введіть національність: ");
                string nationality = Console.ReadLine();
                Console.Write("Введіть зарплату: ");
                if (!int.TryParse(Console.ReadLine(), out int salary) || salary <= 0)
                    throw new ArgumentException("Недійсна зарплата. Введіть додатнє число.");

                Console.WriteLine("Виберіть для кого ви зможите проводити заняття: 1. Початківець, 2. Середній, 3. Професіонал");
                int levelChoice;
                while (!int.TryParse(Console.ReadLine(), out levelChoice) || levelChoice < 1 || levelChoice > 3)
                {
                    Console.WriteLine("Invalid level. Try again.");
                }

                ClientLevel level = (ClientLevel)(levelChoice - 1);

                Trainer trainer = new Trainer(firstName, lastName, age, nationality, salary, level);
                TrainerAccount account = new TrainerAccount(username, password, trainer);
                fitnessCenter.RegisterAccount(account);

                fitnessCenter.Trainers.Add(trainer);

                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine($"Тренер {firstName} успішно зареєстрований з логіном: {username}");
                //Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка введення: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Несподівана помилка: {ex.Message}");
                Console.ResetColor();
            }
        }


        static void Login()
        {
            Console.Clear();
            Console.Write("Введіть Ім'я користувача: ");
            string username = Console.ReadLine();
            Console.Write("Введіть Пароль: ");
            string password = Console.ReadLine();

            var account = fitnessCenter.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (account is ClientAccount clientAccount)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вхід успішно виконано.\n");
                Console.ResetColor();
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\nВаші тренування:");
                    Console.ResetColor();
                    var clientTrainings = fitnessCenter.Trainings
                        .Where(t => t.Clients.Contains(clientAccount.Client))
                        .ToList();

                    if (!clientTrainings.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Тренувань не знайдено.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("-------------------------------------------------------------------------------------------");
                        Console.WriteLine("| №   | Тренування      | Дата       | Час   | Тренер               | Статус              |");
                        Console.WriteLine("-------------------------------------------------------------------------------------------");

                        int index = 1;
                        foreach (var training in clientTrainings)
                        {
                            string status = training.IsIndividual ? "Індивідуальні тренування" : $"Місць лишилося: {training.Hall.Capacity - training.Clients.Count}";
                            string trainerName = $"{training.Trainer.FirstName} {training.Trainer.LastName}".PadRight(20);
                            Console.WriteLine($"| {index++,-3} | {training.Type,-15} | {training.Date:dd.MM.yyyy} | {training.Date:HH:mm} | {trainerName,-20} | {status,-20} |");
                        }

                        Console.WriteLine("-------------------------------------------------------------------------------------------");

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Бажаєте зареєструватися на інший тренінг? (так/ні)");
                    Console.ResetColor();
                    string choice = Console.ReadLine()?.ToLower();
                    if (choice != "так")
                    {
                        break;
                    }

                    fitnessCenter.ChooseTraining(clientAccount.Client);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Недійсні облікові дані.");
                Console.ResetColor();
            }
        }
        static void ViewSchedule()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Оберіть дату (від сьогодні до 7 днів включно):");
            Console.ResetColor();
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}");
            }

            int daySelection;
            while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Console.ResetColor();
            }

            DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            fitnessCenter.DisplayTrainings(selectedDate);
        }
    }
}
