/*
Лабораторная работа № 2
Тема: Наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать производный класс автомобиля с маркой и регистрационным номером.
*/

namespace Lab02;

/// <summary>
/// Представляет автомобиль и наследует общие свойства транспорта.
/// </summary>
public class Car : TransportVehicle
{
    /// <summary>
    /// Создает автомобиль со стандартными значениями.
    /// </summary>
    public Car() : this("Не указана", "Не указан", 1, "Не указано", DateTime.Now.Year)
    {
    }

    /// <summary>
    /// Создает автомобиль со всеми необходимыми характеристиками.
    /// </summary>
    public Car(
        string brand,
        string registrationNumber,
        double averageSpeed,
        string fuelType,
        int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
        Brand = brand;
        RegistrationNumber = registrationNumber;
    }

    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Государственный регистрационный номер.
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    /// Дополняет общее описание сведениями об автомобиле.
    /// </summary>
    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}, " + base.GetDescription();
    }
}
