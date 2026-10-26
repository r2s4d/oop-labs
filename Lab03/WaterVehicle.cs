/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Добавить в абстрактную иерархию промежуточный абстрактный класс
водного транспорта.
*/

namespace Lab03;

// Промежуточный абстрактный класс для транспорта, который движется по воде.
public abstract class WaterVehicle : TransportVehicle
{
    private double _loadCapacityKg;

    protected WaterVehicle(double averageSpeed, string fuelType, int yearOfManufacture, double loadCapacityKg)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
        LoadCapacityKg = loadCapacityKg;
    }

    // Реализует абстрактное свойство базового класса для всех видов водного транспорта.
    public override string MovementEnvironment => "Вода";

    // Грузоподъемность в килограммах.
    public double LoadCapacityKg
    {
        get => _loadCapacityKg;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Грузоподъемность должна быть больше нуля.");
            }

            _loadCapacityKg = value;
        }
    }
}
