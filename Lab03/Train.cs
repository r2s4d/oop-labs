/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать конкретный класс поезда в абстрактной иерархии транспорта.
*/

namespace Lab03;

/// <summary>
/// Конкретный вид транспорта, который представляет поезд.
/// </summary>
public sealed class Train : TransportVehicle
{
    public Train(
        string trainNumber,
        int carriageCount,
        int passengersPerCarriage,
        double averageSpeed,
        string fuelType,
        int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
        if (carriageCount <= 0 || passengersPerCarriage <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carriageCount), "Количество вагонов и мест должно быть положительным.");
        }

        TrainNumber = trainNumber;
        CarriageCount = carriageCount;
        PassengersPerCarriage = passengersPerCarriage;
    }

    public string TrainNumber { get; }

    public int CarriageCount { get; }

    public int PassengersPerCarriage { get; }

    public int GetTotalSeats()
    {
        return CarriageCount * PassengersPerCarriage;
    }

    /// <summary>
    /// Реализует обязательный абстрактный метод базового класса.
    /// </summary>
    public override string GetDescription()
    {
        return $"Поезд № {TrainNumber}, вагонов {CarriageCount}, мест {GetTotalSeats()}, " +
               $"скорость {AverageSpeed:F1} км/ч, топливо {FuelType}, год {YearOfManufacture}";
    }
}
