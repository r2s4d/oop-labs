/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Сохранить промежуточный абстрактный класс водного транспорта,
чтобы запросы можно было выполнять и по базовым типам иерархии.
*/

namespace Lab04;

// Промежуточный абстрактный класс для транспорта, который движется по воде.
public abstract class WaterVehicle : TransportVehicle
{
    protected WaterVehicle(double averageSpeed, string fuelType, int yearOfManufacture, double loadCapacityKg)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
        if (loadCapacityKg <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(loadCapacityKg), "Грузоподъемность должна быть больше нуля.");
        }

        LoadCapacityKg = loadCapacityKg;
    }

    // override абстрактного свойства из TransportVehicle.
    public override string MovementEnvironment => "Вода";

    public double LoadCapacityKg { get; }
}
