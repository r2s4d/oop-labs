/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать класс поезда для подсчета мест во всех вагонах поездов.
*/

namespace Lab04;

// Поезд с количеством вагонов и мест в каждом вагоне.
// Наследует абстрактный GroundVehicle, sealed запрещает наследование от Train.
public sealed class Train : GroundVehicle
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

    // override абстрактного свойства из GroundVehicle.
    public override string RoadType => "Железная дорога";

    // override абстрактного метода из TransportVehicle.
    public override string GetDescription()
    {
        return $"Поезд № {TrainNumber}, среда: {MovementEnvironment} ({RoadType}), вагонов {CarriageCount}, мест {GetTotalSeats()}";
    }
}
