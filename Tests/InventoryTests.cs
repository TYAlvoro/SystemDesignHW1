using ZooErpProject.Inventory;

namespace Tests;

public class InventoryTests
{
    [Fact]
    public void Thing_Table_Computer_HaveCorrectProperties()
    {
        // Arrange
        var thing = new Thing("Generic Thing", 1);
        var table = new Table("Office Table", 2);
        var computer = new Computer("Dell", 3);

        // Act & Assert: проверка корректности инициализации свойств
        Assert.Equal("Generic Thing", thing.Name);
        Assert.Equal(1, thing.Number);
        Assert.Equal("Office Table", table.Name);
        Assert.Equal(2, table.Number);
        Assert.Equal("Dell", computer.Name);
        Assert.Equal(3, computer.Number);
    }
}