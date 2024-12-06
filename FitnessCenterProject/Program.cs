using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCenterProject
{
    class Program
    {
        static FitnessCenter fitnessCenter = new FitnessCenter("MyFitness");

        static void Main(string[] args)
        {
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
                Console.WriteLine("\nЗапрошуємо до MyFitness!");
                Console.WriteLine("1. Реєстрація клієнта");
                Console.WriteLine("2. Резюме тренера");
                Console.WriteLine("3. Вхід");
                Console.WriteLine("4. Розклад флітнес центру");
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
                    case "0":
                        Console.WriteLine("Доподачення!");
                        return;
                    default:
                        Console.WriteLine("Недійсний варіант. Спробуйте знову.");
                        break;
                }
            }
        }

        static void RegisterClient()
        {
            try
            {
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
            if (!int.TryParse(Console.ReadLine(), out int levelChoice) || levelChoice < 1 || levelChoice > 3)
                throw new ArgumentException("Недійсний рівень. Виберіть дійсний варіант.");
            ClientLevel level = (ClientLevel)(int.Parse(Console.ReadLine()) - 1);

            Client client = new Client(firstName, lastName, age, nationality, level);
            ClientAccount account = new ClientAccount(username, password, client, fitnessCenter);
            fitnessCenter.RegisterAccount(account);

            fitnessCenter.ChooseTraining(client);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка введення: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Несподівана помилка: {ex.Message}");
            }
        }
        static void ApplyAsTrainer()
        {
            Console.Write("Введіть Ім'я користувача: ");
            string username = Console.ReadLine();
            Console.Write("Введіть Пароль: ");
            string password = Console.ReadLine();

            Console.Write("Введіть ім'я: ");
            string firstName = Console.ReadLine();
            Console.Write("Введіть прізвище: ");
            string lastName = Console.ReadLine();
            Console.Write("Введіть вік: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введіть національність: ");
            string nationality = Console.ReadLine();
            Console.Write("Введіть зарплату: ");
            int salary = int.Parse(Console.ReadLine());

            Console.WriteLine("Виберіть рівень знань: 1. Початківець, 2. Середній, 3. Професіонал");
            ClientLevel level = (ClientLevel)(int.Parse(Console.ReadLine()) - 1);

            Trainer trainer = new Trainer(firstName, lastName, age, nationality, salary, level);
            TrainerAccount account = new TrainerAccount(username, password, trainer);
            fitnessCenter.RegisterAccount(account);
        }

        static void Login()
        {
            Console.Write("Введіть Ім'я користувача: ");
            string username = Console.ReadLine();
            Console.Write("Введіть Пароль: ");
            string password = Console.ReadLine();

            var account = fitnessCenter.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (account is ClientAccount clientAccount)
            {
                Console.WriteLine("Вхід успішно виконано.");
                while (true)
                {
                    Console.WriteLine("Ваші тренування:");
                    var clientTrainings = fitnessCenter.Trainings
                        .Where(t => t.Clients.Contains(clientAccount.Client))
                        .ToList();

                    if (!clientTrainings.Any())
                    {
                        Console.WriteLine("Тренувань не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------");
                        Console.WriteLine("| № | Тренування       | Дата       | Час    | Тренер        | Статус          |");
                        Console.WriteLine("-----------------------------------------------------------------------------");

                        int index = 1;
                        foreach (var training in clientTrainings)
                        {
                            string status = training.IsIndividual ? "Індивідуальні тренування" : $"Місць лишилося: {training.Hall.Capacity - training.Clients.Count}";
                            Console.WriteLine($"| {index++,-2} | {training.Type,-15} | {training.Date:dd.MM.yyyy} | {training.Date:HH:mm} | {training.Trainer.FirstName} {training.Trainer.LastName,-10} | {status,-16} |");
                        }

                        Console.WriteLine("-----------------------------------------------------------------------------");
                    }

                    Console.WriteLine("Бажаєте зареєструватися на інший тренінг? (так/ні)");
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
                Console.WriteLine("Недійсні облікові дані.");
            }
        }
        static void ViewSchedule()
        {
            Console.WriteLine("Оберіть дату (від сьогодні до 7 днів включно):");
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}");
            }

            int daySelection;
            while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }

            DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            fitnessCenter.DisplayTrainings(selectedDate);
        }
    }
}
