namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс для волка.
/// </summary>
/// <param name="name">Имя волка</param>
/// <param name="food">Количество потребляемой еды волка</param>
/// <param name="number">Инвентаризационный номер волка</param>
public class Wolf : Predator
{
    public Wolf(string name, int food, int number)
        : base(name, food, number)
    { }
}