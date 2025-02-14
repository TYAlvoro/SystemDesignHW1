namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс хищника.
/// </summary>
/// <param name="name">Имя животного</param>
/// <param name="food">Количество потребляемой еды животного</param>
/// <param name="number">Инвентаризационный номер животного</param>
public abstract class Predator : Animal
{
    protected Predator(string name, int food, int number)
        : base(name, food, number)
    { }
}