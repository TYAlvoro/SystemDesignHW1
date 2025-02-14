namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс для обезьяны.
/// </summary>
/// <param name="name">Имя обезьяны</param>
/// <param name="food">Количество потребляемой еды обезьяны</param>
/// <param name="number">Инвентаризационный номер обезьяны</param>
/// <param name="kindnessLevel">Уровень доброты обезьяны</param>
public class Monkey : Herbo
{
    public Monkey(string name, int food, int number, int kindnessLevel)
        : base(name, food, number, kindnessLevel)
    { }
}