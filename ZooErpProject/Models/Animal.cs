using ZooErpProject.Interfaces;

namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс для всех животных.
/// Реализует требуемые интерфейсы IAlive и IInventory&
/// </summary>
/// <param name="name">Имя животного</param>
/// <param name="food">Тип еды животного</param>
/// <param name="number">Инвентаризационный номер животного</param>
public abstract class Animal : IAlive, IInventory
{
    public int Food { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }

    protected Animal(string name, int food, int number)
    {
        Name = name;
        Food = food;
        Number = number;
    }
}