using ZooErpProject.Interfaces;
using ZooErpProject.Models;

namespace ZooErpProject.Zoo;

/// <summary>
/// Класс, управляющий зоопарком.
/// Отвечает за прием животных, подсчет потребления еды, формирование списков для контактного зоопарка и инвентаризации.
/// </summary>
/// <param name="vetClinic"></param>
public class Zoo(IVetClinic vetClinic)
{
    private readonly IVetClinic _vetClinic = vetClinic;
    private readonly List<Animal> _animals = new();

    /// <summary>
    /// Добавление животных в зоопарк в зависимости от состояния здоровья.
    /// </summary>
    /// <param name="animal">Конкретное животное</param>
    /// <returns>Добавлено ли животное в зоопарк</returns>
    public bool AddAnimal(Animal animal)
    {
        if (!_vetClinic.CheckHealth(animal)) return false;
        _animals.Add(animal);
        return true;

    }
    
    /// <summary>
    /// Вычисляет общий расход еды всеми животными.
    /// </summary>
    /// <returns>Общий расход еды</returns>
    public int TotalFoodConsumption() => _animals.Sum(animal => animal.Food);

    /// <summary>
    /// Возвращает животных, подходящих для контактного зоопарка (травоядные с добротой > 5).
    /// </summary>
    /// <returns>Коллекция животных в контактном зоопарке</returns>
    public IEnumerable<Animal> GetContactZooAnimals()
    {
        return _animals.Where(animal => animal is Herbo { KindnessLevel: > 5 });
    }

    /// <summary>
    /// Возвращает информацию для инвентаризации (имя и номер каждого животного).
    /// </summary>
    /// <returns>Коллекция пар (Имя, инвентаризационный номер)</returns>
    public IEnumerable<(string Name, int Number)> GetInventoryInfo()
    {
        return _animals.Select(animal => (animal.Name, animal.Number));
    }
}