namespace ZooErpProject.Inventory;

/// <summary>
/// Класс, представляющий компьютер.
/// Наследуется от Thing.
/// </summary>
public class Computer(string name, int number) : Thing(name, number);