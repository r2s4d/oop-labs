/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Сохранить промежуточный абстрактный класс наземного транспорта,
чтобы запросы можно было выполнять и по базовым типам иерархии.
*/

namespace Lab04;

// Промежуточный абстрактный класс для транспорта, который движется по суше.
public abstract class GroundVehicle : TransportVehicle
{
    protected GroundVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
    }

    // override абстрактного свойства из TransportVehicle.
    public override string MovementEnvironment => "Суша";

    public abstract string RoadType { get; }
}
