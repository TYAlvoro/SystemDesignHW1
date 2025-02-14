using ZooErpProject.Interfaces;
using ZooErpProject.Models;

namespace ZooErpProject.Services;

/// <summary>
/// Класс реализующий логику ветиринарной клиники.
/// В данном случае логика проверки здоровья упрощена до вечного возврата true.
/// </summary>
public class VetClinic : IVetClinic
{
    /// <summary>
    /// Проверка здоровья животного.
    /// </summary>
    /// <param name="animal">Конкретное животное</param>
    /// <returns>Здорово ли животное</returns>
    public bool CheckHealth(Animal animal)
    {
        // Логика очевидно должна быть более сложной в реальной системе.
        return true;
    }
}