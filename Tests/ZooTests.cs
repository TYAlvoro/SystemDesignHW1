using ZooErpProject.Models;
using ZooErpProject.Zoo;

namespace Tests;

public class ZooTests
{
    [Fact]
    public void AddAnimal_HealthyAnimal_ReturnsTrueAndAddsAnimal()
    {
        // Arrange
        var fakeVet = new FakeVetClinic(true);
        var zoo = new Zoo(fakeVet);
        var monkey = new Monkey("George", 5, 101, 7);

        // Act
        bool result = zoo.AddAnimal(monkey);

        // Assert
        Assert.True(result);
        Assert.Equal(5, zoo.TotalFoodConsumption());
        var contactAnimals = zoo.GetContactZooAnimals().ToList();
        Assert.Contains(monkey, contactAnimals);

        var inventoryInfo = zoo.GetInventoryInfo().ToList();
        Assert.Contains(inventoryInfo, item => item.Name == "George" && item.Number == 101);
    }

    [Fact]
    public void AddAnimal_UnhealthyAnimal_ReturnsFalseAndDoesNotAddAnimal()
    {
        // Arrange
        var fakeVet = new FakeVetClinic(false);
        var zoo = new Zoo(fakeVet);
        var tiger = new Tiger("Sher Khan", 10, 202);

        // Act
        bool result = zoo.AddAnimal(tiger);

        // Assert
        Assert.False(result);
        Assert.Equal(0, zoo.TotalFoodConsumption());
        var contactAnimals = zoo.GetContactZooAnimals().ToList();
        Assert.Empty(contactAnimals);
    }

    [Fact]
    public void TotalFoodConsumption_CorrectlySumsFoodForMultipleAnimals()
    {
        // Arrange
        var fakeVet = new FakeVetClinic(true);
        var zoo = new Zoo(fakeVet);
        var monkey = new Monkey("George", 5, 101, 7);
        var rabbit = new Rabbit("Bunny", 3, 102, 8);
        var tiger = new Tiger("Sher Khan", 10, 202);
        zoo.AddAnimal(monkey);
        zoo.AddAnimal(rabbit);
        zoo.AddAnimal(tiger);

        // Act
        int totalFood = zoo.TotalFoodConsumption();

        // Assert
        Assert.Equal(18, totalFood);
    }

    [Fact]
    public void GetContactZooAnimals_ReturnsOnlyHerboAnimalsWithKindnessAboveFive()
    {
        // Arrange
        var fakeVet = new FakeVetClinic(true);
        var zoo = new Zoo(fakeVet);
        var monkey = new Monkey("George", 5, 101, 7);          // подходит
        var rabbitLowKind = new Rabbit("Flopsy", 3, 102, 4);      // не подходит
        var rabbitHighKind = new Rabbit("Bunny", 3, 103, 8);       // подходит
        var tiger = new Tiger("Sher Khan", 10, 202);              // не травоядное
        zoo.AddAnimal(monkey);
        zoo.AddAnimal(rabbitLowKind);
        zoo.AddAnimal(rabbitHighKind);
        zoo.AddAnimal(tiger);

        // Act
        var contactAnimals = zoo.GetContactZooAnimals().ToList();

        // Assert
        Assert.Equal(2, contactAnimals.Count);
        Assert.Contains(monkey, contactAnimals);
        Assert.Contains(rabbitHighKind, contactAnimals);
        Assert.DoesNotContain(rabbitLowKind, contactAnimals);
        Assert.DoesNotContain(tiger, contactAnimals);
    }

    [Fact]
    public void GetInventoryInfo_ReturnsCorrectInventoryDataForAnimals()
    {
        // Arrange
        var fakeVet = new FakeVetClinic(true);
        var zoo = new Zoo(fakeVet);
        var monkey = new Monkey("George", 5, 101, 7);
        var tiger = new Tiger("Sher Khan", 10, 202);
        zoo.AddAnimal(monkey);
        zoo.AddAnimal(tiger);

        // Act
        var inventory = zoo.GetInventoryInfo().ToList();

        // Assert
        Assert.Equal(2, inventory.Count);
        Assert.Contains(inventory, i => i.Name == "George" && i.Number == 101);
        Assert.Contains(inventory, i => i.Name == "Sher Khan" && i.Number == 202);
    }
}