using ZooErpProject.Interfaces;

namespace ZooErpProject.Inventory;

/// <summary>
/// Базовый класс для инвентарных предметов.
/// Реализует IInventory.
/// </summary>
public class Thing(string name, int number) : IInventory
{
    public string Name { get; set; } = name;
    public int Number { get; set; } = number;
}