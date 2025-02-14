namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс для кролика.
/// </summary>
/// <param name="name">Имя кролика</param>
/// <param name="food">Количество потребляемой еды кролика</param>
/// <param name="number">Инвентаризационный номер кролика</param>
/// <param name="kindnessLevel">Уровень доброты кролика</param>
public class Rabbit : Herbo
{
    public Rabbit(string name, int food, int number, int kindnessLevel)
        : base(name, food, number, kindnessLevel)
    { }
}