/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать конкретный класс лодки в абстрактной иерархии транспорта.
*/

namespace Lab03;

// Конкретный вид транспорта, который представляет лодку.
public sealed class Boat : WaterVehicle
{
    public Boat(
        string name,
        int passengerCapacity,
        double loadCapacityKg,
        double averageSpeed,
        string fuelType,
        int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture, loadCapacityKg)
    {
        if (passengerCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(passengerCapacity), "Вместимость должна быть больше нуля.");
        }

        Name = name;
        PassengerCapacity = passengerCapacity;
    }

    public string Name { get; }

    public int PassengerCapacity { get; }

    // Реализует обязательный абстрактный метод базового класса.
    public override string GetDescription()
    {
        return $"Лодка «{Name}», среда: {MovementEnvironment}, мест {PassengerCapacity}, " +
               $"грузоподъемность {LoadCapacityKg:F0} кг, скорость {AverageSpeed:F1} км/ч, " +
               $"топливо {FuelType}, год {YearOfManufacture}";
    }
}
