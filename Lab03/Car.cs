/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать конкретный класс автомобиля в абстрактной иерархии транспорта.
*/

namespace Lab03;

// Конкретный вид транспорта, который представляет автомобиль.
public sealed class Car : GroundVehicle
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

    // Реализует абстрактное свойство класса GroundVehicle.
    public override string RoadType => "Автомобильная дорога";

    // Реализует обязательный абстрактный метод базового класса.
    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}, среда: {MovementEnvironment} ({RoadType}), " +
               $"скорость {AverageSpeed:F1} км/ч, топливо {FuelType}, год {YearOfManufacture}";
    }
}
