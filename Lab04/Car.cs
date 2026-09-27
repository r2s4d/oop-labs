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

    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}";
    }
}
