/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Продолжить проект с абстрактной иерархией транспорта и связанным списком.
Реализовать запросы по фактическим типам объектов.
*/

namespace Lab04;

// Абстрактный базовый класс всех транспортных средств.
public abstract class TransportVehicle
{
    // Начало общего односвязного списка транспорта.
    private static TransportNode? _head;

    protected TransportVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
    {
        if (averageSpeed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(averageSpeed), "Скорость должна быть больше нуля.");
        }

        if (yearOfManufacture < 1800 || yearOfManufacture > DateTime.Now.Year)
        {
            throw new ArgumentOutOfRangeException(nameof(yearOfManufacture), "Указан недопустимый год выпуска.");
        }

        AverageSpeed = averageSpeed;
        FuelType = fuelType;
        YearOfManufacture = yearOfManufacture;
    }

    public double AverageSpeed { get; }

    public string FuelType { get; }

    public int YearOfManufacture { get; }

    public abstract string GetDescription();

    // Добавляет объект в начало общего связанного списка.
    public static void AddToRegistry(TransportVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        _head = new TransportNode(vehicle, _head);
    }

    // Последовательно возвращает объекты связанного списка.
    // Ключевое слово yield позволяет не создавать дополнительную коллекцию.
    public static IEnumerable<TransportVehicle> EnumerateRegistry()
    {
        TransportNode? current = _head;

        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    private sealed class TransportNode
    {
        public TransportNode(TransportVehicle value, TransportNode? next)
        {
            Value = value;
            Next = next;
        }

        public TransportVehicle Value { get; }

        public TransportNode? Next { get; }
    }
}
