/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Добавить в абстрактную иерархию промежуточный абстрактный класс
наземного транспорта.
*/

namespace Lab03;

// Промежуточный абстрактный класс для транспорта, который движется по суше.
// Метод GetDescription здесь не реализуется, поэтому класс остается абстрактным.
public abstract class GroundVehicle : TransportVehicle
{
    // Конструктор передает общие данные в базовый класс через base(...).
    protected GroundVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
    }

    // Реализует (override) абстрактное свойство базового класса для всех видов наземного транспорта.
    public override string MovementEnvironment => "Суша";

    // Новое абстрактное свойство: каждый конкретный наземный транспорт задает свой путь.
    public abstract string RoadType { get; }
}
