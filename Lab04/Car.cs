/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать класс автомобиля в запросах к общей коллекции транспорта.
*/

namespace Lab04;

// Автомобиль с маркой и регистрационным номером.
// Наследует абстрактный GroundVehicle, sealed запрещает наследование от Car.
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

    // override абстрактного свойства из GroundVehicle.
    public override string RoadType => "Автомобильная дорога";

    // override абстрактного метода из TransportVehicle.
    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}, среда: {MovementEnvironment} ({RoadType})";
    }
}
