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

// Представляет поезд и хранит данные о его составе.
public class Train : TransportVehicle
{
    private int _carriageCount;
    private int _passengersPerCarriage;

    // Создает поезд со стандартными значениями.
    public Train() : this("Не указан", 1, 1, 1, "Электричество", DateTime.Now.Year)
    {
    }

    // Создает поезд с заданными характеристиками.
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

    // Номер поезда.
    public string TrainNumber { get; set; }

    // Количество вагонов в составе.
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

    // Количество пассажиров, которое помещается в одном вагоне.
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

    // Возвращает общее количество пассажирских мест в поезде.
    public int GetTotalSeats()
    {
        return CarriageCount * PassengersPerCarriage;
    }

    // Дополняет общее описание сведениями о поезде.
    public override string GetDescription()
    {
        return $"Поезд № {TrainNumber}, вагонов {CarriageCount}, мест {GetTotalSeats()}, " + base.GetDescription();
    }
}
