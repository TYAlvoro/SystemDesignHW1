namespace ZooErpProject.Models;

/// <summary>
/// Абстрактный класс травоядного.
/// </summary>
/// <param name="name">Имя животного</param>
/// <param name="food">Количество потребляемой еды животного</param>
/// <param name="number">Инвентаризационный номер животного</param>
/// <param name="kindnessLevel">Уровень доброты животного</param>
public abstract class Herbo : Animal
{
    public int KindnessLevel { get; set; }

    protected Herbo(string name, int food, int number, int kindnessLevel)
        : base(name, food, number)
    {
        KindnessLevel = kindnessLevel;
    }
}