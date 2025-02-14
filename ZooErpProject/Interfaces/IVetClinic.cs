using ZooErpProject.Models;

namespace ZooErpProject.Interfaces;

/// <summary>
/// Интерфейс для ветеринарной клиники, отвечающей за проверку здоровья животных.
/// </summary>
public interface IVetClinic
{
    bool CheckHealth(Animal animal);
}