namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс для тигра.
/// </summary>
/// <param name="name">Имя тигра</param>
/// <param name="food">Количество потребляемой еды тигра</param>
/// <param name="number">Инвентаризационный номер тигра</param>
public class Tiger : Predator
{
    public Tiger(string name, int food, int number)
        : base(name, food, number)
    { }
}