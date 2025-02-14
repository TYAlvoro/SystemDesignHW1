using Microsoft.Extensions.DependencyInjection;
using ZooErpProject.Interfaces;
using ZooErpProject.Models;
using ZooErpProject.Services;

namespace ZooErpProject
{
    /// <summary>
    /// Консольное приложение для учета животных Московского зоопарка.
    /// </summary>
    class Program
    {
        private static ServiceCollection? _services;
        private static Zoo.Zoo? _zoo;

        static void Main()
        {
            try
            {
                InitServices();

                // Используем using для корректного освобождения ресурсов ServiceProvider
                using (var serviceProvider = _services!.BuildServiceProvider())
                {
                    // Используем GetRequiredService, чтобы сразу выбросилось исключение,
                    // если зависимость не зарегистрирована
                    _zoo = serviceProvider.GetRequiredService<Zoo.Zoo>();

                    bool exit = false;
                    while (!exit)
                    {
                        ShowMenu();
                        string? choice = Console.ReadLine();
                        exit = ProcessMenuChoice(choice);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Инициализация DI-контейнера.
        /// </summary>
        static void InitServices()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IVetClinic, VetClinic>();
            _services.AddSingleton<Zoo.Zoo>();
        }

        /// <summary>
        /// Отображает меню действий.
        /// </summary>
        static void ShowMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить новое животное");
            Console.WriteLine("2. Вывести общий расход еды");
            Console.WriteLine("3. Показать животных для контактного зоопарка");
            Console.WriteLine("4. Вывести инвентаризационную информацию животных");
            Console.WriteLine("0. Выход");
        }

        /// <summary>
        /// Обрабатывает выбор пользователя из меню.
        /// </summary>
        /// <param name="choice">Введённый выбор</param>
        /// <returns>true, если пользователь выбрал выход, иначе false</returns>
        static bool ProcessMenuChoice(string? choice)
        {
            switch (choice)
            {
                case "1":
                    ExecuteAddAnimal();
                    break;
                case "2":
                    ExecuteShowTotalFoodConsumption();
                    break;
                case "3":
                    ExecuteShowContactAnimals();
                    break;
                case "4":
                    ExecuteShowInventoryInfo();
                    break;
                case "0":
                    return true;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
            return false;
        }

        /// <summary>
        /// Выполняет добавление нового животного.
        /// </summary>
        static void ExecuteAddAnimal()
        {
            if (_zoo == null)
            {
                Console.WriteLine("Ошибка: Зоопарк не инициализирован.");
                return;
            }

            Animal? animal = CreateAnimalFromInput();
            if (animal != null && _zoo.AddAnimal(animal))
            {
                Console.WriteLine($"Животное {animal.Name} успешно добавлено в зоопарк.");
            }
            else
            {
                Console.WriteLine("Животное не прошло проверку здоровья либо введены неверные данные.");
            }
        }

        /// <summary>
        /// Выполняет вывод общего расхода еды.
        /// </summary>
        static void ExecuteShowTotalFoodConsumption()
        {
            if (_zoo == null)
            {
                Console.WriteLine("Ошибка: Зоопарк не инициализирован.");
                return;
            }

            int totalFoodConsumption = _zoo.TotalFoodConsumption();
            if (totalFoodConsumption > 0)
            {
                Console.WriteLine($"Общий расход еды: {totalFoodConsumption} кг в сутки");
            }
            else
            {
                Console.WriteLine("Либо животные питаются святым духом, либо зоопарк пока пуст!");
            }
        }

        /// <summary>
        /// Выполняет вывод списка животных, подходящих для контактного зоопарка.
        /// </summary>
        static void ExecuteShowContactAnimals()
        {
            if (_zoo == null)
            {
                Console.WriteLine("Ошибка: Зоопарк не инициализирован.");
                return;
            }

            var contactAnimals = _zoo.GetContactZooAnimals();
            if (!contactAnimals.Any())
            {
                Console.WriteLine("Нет доступных животных для контактного зоопарка!");
                return;
            }

            Console.WriteLine("Животные для контактного зоопарка:");
            foreach (var animal in contactAnimals)
            {
                Console.WriteLine($"- {animal.Name} (номер {animal.Number})");
            }
        }

        /// <summary>
        /// Выполняет вывод инвентаризационной информации животных.
        /// </summary>
        static void ExecuteShowInventoryInfo()
        {
            if (_zoo == null)
            {
                Console.WriteLine("Ошибка: Зоопарк не инициализирован.");
                return;
            }

            var inventory = _zoo.GetInventoryInfo();
            if (!inventory.Any())
            {
                Console.WriteLine("На данный момент нет доступных предметов для инвентаризации!");
                return;
            }

            Console.WriteLine("Инвентаризационная информация:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"- {item.Name}, номер: {item.Number}");
            }
        }

        /// <summary>
        /// Считывает с консоли данные и создает объект животного.
        /// Производится проверка корректности ввода и повтор запроса, если данные некорректны.
        /// </summary>
        /// <returns>Созданное животное или null, если выбор типа некорректен</returns>
        static Animal? CreateAnimalFromInput()
        {
            string typeChoice = GetValidatedInput("Выберите тип животного:\n1. Обезьяна\n2. Кролик\n3. Тигр\n4. Волк\nВаш выбор: ",
                                                  input => new[] { "1", "2", "3", "4" }.Contains(input),
                                                  "Неверный выбор типа животного. Пожалуйста, введите 1, 2, 3 или 4.");

            string name = GetValidatedInput("Введите имя животного: ",
                                            input => !string.IsNullOrWhiteSpace(input),
                                            "Имя не может быть пустым. Попробуйте снова.");

            int food = GetValidatedInt("Введите количество кг еды в сутки: ",
                                        value => value > 0,
                                        "Количество еды должно быть положительным числом. Попробуйте снова.");

            int number = GetValidatedInt("Введите инвентаризационный номер: ",
                                          value => value > 0,
                                          "Инвентаризационный номер должен быть положительным числом. Попробуйте снова.");

            switch (typeChoice)
            {
                // Для обезьяны и кролика требуется дополнительный ввод уровня доброты (0-10)
                case "1":
                case "2":
                {
                    int kindness = GetValidatedInt("Введите уровень доброты (0-10): ",
                        value => value >= 0 && value <= 10,
                        "Уровень доброты должен быть от 0 до 10. Попробуйте снова.");
                    return typeChoice == "1"
                        ? new Monkey(name, food, number, kindness)
                        : new Rabbit(name, food, number, kindness);
                }
                case "3":
                case "4":
                    return typeChoice == "3"
                        ? new Tiger(name, food, number)
                        : new Wolf(name, food, number);
                default:
                    Console.WriteLine("Неверный выбор типа животного.");
                    return null;
            }
        }

        /// <summary>
        /// Получает строковый ввод от пользователя и валидирует его по условию.
        /// Повторяет запрос до получения корректного значения.
        /// </summary>
        static string GetValidatedInput(string prompt, Func<string, bool> validate, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input != null && validate(input))
                {
                    return input;
                }
                Console.WriteLine(errorMessage);
            }
        }

        /// <summary>
        /// Получает числовой ввод от пользователя и валидирует его по условию.
        /// Повторяет запрос до получения корректного значения.
        /// </summary>
        static int GetValidatedInt(string prompt, Func<int, bool> validate, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value) && validate(value))
                {
                    return value;
                }
                Console.WriteLine(errorMessage);
            }
        }
    }
}
