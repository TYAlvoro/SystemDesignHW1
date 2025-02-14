using ZooErpProject.Interfaces;
using ZooErpProject.Models;

namespace Tests;

/// <summary>
/// Фейковая реализация IVetClinic для юнит-тестирования.
/// Позволяет задать результат проверки здоровья.
/// </summary>
public class FakeVetClinic : IVetClinic
{
    private readonly bool _healthResult;

    public FakeVetClinic(bool healthResult)
    {
        _healthResult = healthResult;
    }

    public bool CheckHealth(Animal animal)
    {
        return _healthResult;
    }
}