/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать класс лодки в запросах к общей коллекции транспорта.
*/

namespace Lab04;

// Лодка с названием и вместимостью.
// Наследует абстрактный WaterVehicle (водный транспорт), sealed запрещает наследование от Boat.
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

    // override абстрактного метода из TransportVehicle.
    public override string GetDescription()
    {
        return $"Лодка «{Name}», среда: {MovementEnvironment}, мест {PassengerCapacity}";
    }
}
