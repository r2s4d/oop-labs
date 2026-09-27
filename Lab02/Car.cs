/*
Лабораторная работа № 2
Тема: Наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать производный класс автомобиля с маркой и регистрационным номером.
*/

namespace Lab02;

// Представляет автомобиль и наследует общие свойства транспорта.
public class Car : TransportVehicle
{
    // Создает автомобиль со стандартными значениями.
    public Car() : this("Не указана", "Не указан", 1, "Не указано", DateTime.Now.Year)
    {
    }

    // Создает автомобиль со всеми необходимыми характеристиками.
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

    // Марка автомобиля.
    public string Brand { get; set; }

    // Государственный регистрационный номер.
    public string RegistrationNumber { get; set; }

    // Дополняет общее описание сведениями об автомобиле.
    public override string GetDescription()
    {
        return $"Автомобиль {Brand}, номер {RegistrationNumber}, " + base.GetDescription();
    }
}
