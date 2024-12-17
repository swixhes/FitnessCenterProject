using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitnessCenterProject
{
    class Program
    {
        static FitnessCenter fitnessCenter = new FitnessCenter("MyFitness");

        // Делегат для кольорового виводу
        static Action<string, ConsoleColor> DisplayMessage = (message, color) =>
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        };

        // Func для обчислення середнього віку клієнтів
        static Func<List<Client>, double> CalculateAverageAge = (clients) =>
        {
            return clients.Any() ? clients.Average(c => c.Age) : 0;
        };

        // Власний делегат для привітання клієнта
        delegate void WelcomeClient(Client client);
        static WelcomeClient OnWelcomeClient = (client) =>
        {
            DisplayMessage($"Власний делегат - Вітаємо нового клієнта: {client.FirstName} {client.LastName}!", ConsoleColor.Yellow);
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            // Підписка на події
            fitnessCenter.OnClientRegistered += (client) =>
            {
                DisplayMessage($"Подія: Клієнт {client.FirstName} {client.LastName} зареєстрований!", ConsoleColor.DarkGreen);
                OnWelcomeClient(client);
            };

            fitnessCenter.OnTrainerRegistered += (trainer) =>
            {
                DisplayMessage($"Подія: Тренер {trainer.FirstName} {trainer.LastName} доданий!", ConsoleColor.DarkGreen);
            };

            InitializeFitnessCenter();
            MainMenu();
        }

        static void InitializeFitnessCenter()
        {
            fitnessCenter.OnMessage = DisplayMessage; 

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
                new DateTime(2024, 12, 18, 12, 0, 0),
                20 // Максимальна кількість місць для цього тренування
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Силові,
                fitnessCenter.Trainers[1],
                fitnessCenter.Halls.First(h => h.Name == "Силовий зал"),
                new DateTime(2024, 12, 19, 14, 0, 0),
                15
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Йога,
                fitnessCenter.Trainers[2],
                fitnessCenter.Halls.First(h => h.Name == "Студія йоги"),
                new DateTime(2024, 12, 20, 10, 0, 0),
                10
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Плавання,
                fitnessCenter.Trainers[3],
                fitnessCenter.Halls.First(h => h.Name == "Басейн"),
                new DateTime(2024, 12, 21, 16, 0, 0),
                25
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Аеробіка,
                fitnessCenter.Trainers[4],
                fitnessCenter.Halls.First(h => h.Name == "Зал аеробіки"),
                new DateTime(2024, 12, 22, 18, 0, 0),
                18
            ));

            fitnessCenter.Trainings.Add(new Training(
                TrainingType.Пілатес,
                fitnessCenter.Trainers[5],
                fitnessCenter.Halls.First(h => h.Name == "Студія пілатесу"),
                new DateTime(2024, 12, 23, 9, 0, 0),
                12
            ));
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                DisplayMessage("\nЗапрошуємо до MyFitness!", ConsoleColor.Green);
                Console.WriteLine("1. Реєстрація клієнта");
                Console.WriteLine("2. Резюме тренера");
                Console.WriteLine("3. Вхід");
                Console.WriteLine("4. Розклад фітнес центру");
                Console.WriteLine("5. Переглянути клієнтів і тренінги");
                Console.WriteLine("6. Підрахувати середній вік клієнтів");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть варіант: ");

                switch (Console.ReadLine())
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
                    case "6":
                        double averageAge = CalculateAverageAge(fitnessCenter.Clients);
                        DisplayMessage($"Середній вік клієнтів: {averageAge:F2} років", ConsoleColor.DarkYellow);
                        break;
                    case "0":
                        DisplayMessage("Допобачення!", ConsoleColor.DarkGreen);
                        return;
                    default:
                        DisplayMessage("Недійсний варіант. Спробуйте знову.", ConsoleColor.Red);
                        break;
                }
                DisplayMessage("\nНатисніть будь-яку клавішу, щоб повернутися до меню...", ConsoleColor.Blue);
                Console.ReadKey();
            }
        }
        static bool ContainsOnlyLetters(string input)
        {
            return input.All(char.IsLetter);
        }
        static void RegisterClient()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    string username;
                    do
                    {
                        Console.Write("Введіть Nickname: ");
                        username = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(username))
                            DisplayMessage("Nickname не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(username) || fitnessCenter.Accounts.Any(a => a.Username == username));

                    string password;
                    do
                    {
                        Console.Write("Введіть Password: ");
                        password = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(password))
                            DisplayMessage("Password не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(password));

                    string firstName;
                    do
                    {
                        Console.Write("Введіть ім'я: ");
                        firstName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(firstName))
                            DisplayMessage("Ім'я не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(firstName))
                            DisplayMessage("Ім'я повинно містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(firstName) || !ContainsOnlyLetters(firstName));

                    string lastName;
                    do
                    {
                        Console.Write("Введіть прізвище: ");
                        lastName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(lastName))
                            DisplayMessage("Прізвище не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(lastName))
                            DisplayMessage("Прізвище повинно містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(lastName) || !ContainsOnlyLetters(lastName));

                    int age;
                    do
                    {
                        Console.Write("Введіть вік (15-85): ");
                        if (!int.TryParse(Console.ReadLine(), out age) || age < 15 || age > 85)
                            DisplayMessage("Недійсний вік. Вік має бути від 15 до 85 років.", ConsoleColor.Red);
                    } while (age < 15 || age > 85);

                    string nationality;
                    do
                    {
                        Console.Write("Введіть національність: ");
                        nationality = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nationality))
                            DisplayMessage("Національність не може бути порожньою. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(nationality))
                            DisplayMessage("Національність повинна містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(nationality) || !ContainsOnlyLetters(nationality));

                    int levelChoice;
                    do
                    {
                        Console.WriteLine("Виберіть рівень навчання: 1. Початківець, 2. Середній, 3. Професіонал");
                        if (!int.TryParse(Console.ReadLine(), out levelChoice) || levelChoice < 1 || levelChoice > 3)
                            DisplayMessage("Невірний вибір. Введіть число від 1 до 3.", ConsoleColor.Red);
                    } while (levelChoice < 1 || levelChoice > 3);

                    ClientLevel level = (ClientLevel)(levelChoice - 1);

                    var client = new Client(firstName, lastName, age, nationality, level);
                    var account = new ClientAccount(username, password, client, fitnessCenter, DisplayMessage);

                    fitnessCenter.RegisterAccount(account);
                    fitnessCenter.ChooseTraining(client);

                    break;
                }
                catch (Exception ex)
                {
                    DisplayMessage($"Помилка: {ex.Message}. Спробуйте ще раз.", ConsoleColor.Red);
                }
            }
        }


        static void ApplyAsTrainer()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    string username;
                    do
                    {
                        Console.Write("Введіть Nickname: ");
                        username = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(username))
                            DisplayMessage("Nickname не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(username) || fitnessCenter.Accounts.Any(a => a.Username == username));


                    string password;
                    do
                    {
                        Console.Write("Введіть Password: ");
                        password = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(password))
                            DisplayMessage("Password не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(password));

                    string firstName;
                    do
                    {
                        Console.Write("Введіть ім'я: ");
                        firstName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(firstName))
                            DisplayMessage("Ім'я не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(firstName))
                            DisplayMessage("Ім'я повинно містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(firstName) || !ContainsOnlyLetters(firstName));

                    string lastName;
                    do
                    {
                        Console.Write("Введіть прізвище: ");
                        lastName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(lastName))
                            DisplayMessage("Прізвище не може бути порожнім. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(lastName))
                            DisplayMessage("Прізвище повинно містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(lastName) || !ContainsOnlyLetters(lastName));

                    int age;
                    do
                    {
                        Console.Write("Введіть вік (18-85): ");
                        if (!int.TryParse(Console.ReadLine(), out age) || age < 18 || age > 85)
                            DisplayMessage("Недійсний вік. Вік має бути від 18 до 85 років.", ConsoleColor.Red);
                    } while (age < 18 || age > 85);

                    string nationality;
                    do
                    {
                        Console.Write("Введіть національність: ");
                        nationality = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nationality))
                            DisplayMessage("Національність не може бути порожньою. Спробуйте ще раз.", ConsoleColor.Red);
                        else if (!ContainsOnlyLetters(nationality))
                            DisplayMessage("Національність повинна містити лише літери. Спробуйте ще раз.", ConsoleColor.Red);
                    } while (string.IsNullOrWhiteSpace(nationality) || !ContainsOnlyLetters(nationality));

                    int salary;
                    do
                    {
                        Console.Write("Введіть зарплату: ");
                        if (!int.TryParse(Console.ReadLine(), out salary) || salary <= 0)
                            DisplayMessage("Недійсна зарплата. Введіть додатнє число.", ConsoleColor.Red);
                    } while (salary <= 0);

                    int levelChoice;
                    do
                    {
                        Console.WriteLine("Виберіть рівень спеціалізації: 1. Початківець, 2. Середній, 3. Професіонал");
                        if (!int.TryParse(Console.ReadLine(), out levelChoice) || levelChoice < 1 || levelChoice > 3)
                            DisplayMessage("Невірний вибір. Введіть число від 1 до 3.", ConsoleColor.Red);
                    } while (levelChoice < 1 || levelChoice > 3);

                    ClientLevel level = (ClientLevel)(levelChoice - 1);

                    var trainer = new Trainer(firstName, lastName, age, nationality, salary, level);
                    var account = new TrainerAccount(username, password, trainer, DisplayMessage);

                    fitnessCenter.RegisterAccount(account);

                    break;
                }
                catch (Exception ex)
                {
                    DisplayMessage($"Помилка: {ex.Message}. Спробуйте ще раз.", ConsoleColor.Red);
                }
            }
        }


        static void Login()
        {
            Console.Clear();
            Console.Write("Введіть Nickname: ");
            string username = Console.ReadLine();
            Console.Write("Введіть Password: ");
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
            Console.WriteLine("Оберіть дату (від сьогодні до 7 днів включно):");

            for (int i = 0; i < 7; i++)
                DisplayMessage($"{i + 1}. {DateTime.Today.AddDays(i):dd.MM.yyyy}", ConsoleColor.Black);
            int daySelection = int.Parse(Console.ReadLine());
            fitnessCenter.DisplayTrainings(DateTime.Today.AddDays(daySelection - 1));
            //int daySelection;
            //while (!int.TryParse(Console.ReadLine(), out daySelection) || daySelection < 1 || daySelection > 7)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            //    Console.ResetColor();
            //}

            //DateTime selectedDate = DateTime.Today.AddDays(daySelection - 1);
            //fitnessCenter.DisplayTrainings(selectedDate);
        }
    }
}
