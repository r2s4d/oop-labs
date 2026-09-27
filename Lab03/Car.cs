/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать конкретный класс автомобиля в абстрактной иерархии транспорта.
*/

namespace Lab03;

/// <summary>
/// Конкретный вид транспорта, который представляет автомобиль.
/// </summary>
public sealed class Car : TransportVehicle
{
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

    public string Brand { get; }

    public string RegistrationNumber { get; }

    /// <summary>
    /// Реализует обязательный абстрактный метод базового класса.
    /// </summary>
    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}, " +
               $"скорость {AverageSpeed:F1} км/ч, топливо {FuelType}, год {YearOfManufacture}";
    }
}
