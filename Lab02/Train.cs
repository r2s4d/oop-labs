/*
Лабораторная работа № 2
Тема: Наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать производный класс поезда с номером, количеством вагонов
и количеством пассажиров в одном вагоне.
*/

namespace Lab02;

/// <summary>
/// Представляет поезд и хранит данные о его составе.
/// </summary>
public class Train : TransportVehicle
{
    private int _carriageCount;
    private int _passengersPerCarriage;

    /// <summary>
    /// Создает поезд со стандартными значениями.
    /// </summary>
    public Train() : this("Не указан", 1, 1, 1, "Электричество", DateTime.Now.Year)
    {
    }

    /// <summary>
    /// Создает поезд с заданными характеристиками.
    /// </summary>
    public Train(
        string trainNumber,
        int carriageCount,
        int passengersPerCarriage,
        double averageSpeed,
        string fuelType,
        int yearOfManufacture)
        : base(averageSpeed, fuelType, yearOfManufacture)
    {
        TrainNumber = trainNumber;
        CarriageCount = carriageCount;
        PassengersPerCarriage = passengersPerCarriage;
    }

    /// <summary>
    /// Номер поезда.
    /// </summary>
    public string TrainNumber { get; set; }

    /// <summary>
    /// Количество вагонов в составе.
    /// </summary>
    public int CarriageCount
    {
        get => _carriageCount;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Количество вагонов должно быть больше нуля.");
            }

            _carriageCount = value;
        }
    }

    /// <summary>
    /// Количество пассажиров, которое помещается в одном вагоне.
    /// </summary>
    public int PassengersPerCarriage
    {
        get => _passengersPerCarriage;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Количество пассажиров должно быть больше нуля.");
            }

            _passengersPerCarriage = value;
        }
    }

    /// <summary>
    /// Возвращает общее количество пассажирских мест в поезде.
    /// </summary>
    public int GetTotalSeats()
    {
        return CarriageCount * PassengersPerCarriage;
    }

    /// <summary>
    /// Дополняет общее описание сведениями о поезде.
    /// </summary>
    public override string GetDescription()
    {
        return $"Поезд № {TrainNumber}, вагонов {CarriageCount}, мест {GetTotalSeats()}, " + base.GetDescription();
    }
}
