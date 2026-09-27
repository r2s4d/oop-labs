/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Расширить иерархию транспорта из лабораторной работы № 2. Создать
абстрактный базовый класс, абстрактные методы и статический связанный
список объектов с методом просмотра.
*/

namespace Lab03;

/// <summary>
/// Абстрактная основа иерархии транспортных средств.
/// Создать объект этого класса напрямую нельзя.
/// </summary>
public abstract class TransportVehicle
{
    // Ссылка на первый узел общего связанного списка.
    // Поле static является единым для всех объектов и производных классов.
    private static TransportNode? _head;

    private double _averageSpeed;
    private int _yearOfManufacture;

    /// <summary>
    /// Создает транспорт с общими характеристиками.
    /// </summary>
    protected TransportVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
    {
        AverageSpeed = averageSpeed;
        FuelType = string.IsNullOrWhiteSpace(fuelType)
            ? throw new ArgumentException("Тип топлива не должен быть пустым.", nameof(fuelType))
            : fuelType;
        YearOfManufacture = yearOfManufacture;
    }

    /// <summary>
    /// Средняя скорость транспорта.
    /// </summary>
    public double AverageSpeed
    {
        get => _averageSpeed;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Скорость должна быть больше нуля.");
            }

            _averageSpeed = value;
        }
    }

    /// <summary>
    /// Тип топлива или источник энергии.
    /// </summary>
    public string FuelType { get; }

    /// <summary>
    /// Год выпуска транспортного средства.
    /// </summary>
    public int YearOfManufacture
    {
        get => _yearOfManufacture;
        set
        {
            if (value < 1800 || value > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Указан недопустимый год выпуска.");
            }

            _yearOfManufacture = value;
        }
    }

    /// <summary>
    /// Производный класс обязан сформировать собственное описание объекта.
    /// </summary>
    public abstract string GetDescription();

    /// <summary>
    /// Добавляет транспорт в начало общего связанного списка.
    /// </summary>
    public static void AddToRegistry(TransportVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        // Новый узел ссылается на прежнее начало списка.
        // После этого новый узел становится началом списка.
        _head = new TransportNode(vehicle, _head);
    }

    /// <summary>
    /// Выводит все объекты из общего связанного списка.
    /// Метод вызывается через имя класса, так как он статический.
    /// </summary>
    public static void ShowRegistry()
    {
        if (_head is null)
        {
            Console.WriteLine("Список транспорта пуст.");
            return;
        }

        TransportNode? current = _head;
        int number = 1;

        // Переходим от текущего узла к следующему, пока список не закончится.
        while (current is not null)
        {
            Console.WriteLine($"{number}. {current.Value.GetDescription()}");
            current = current.Next;
            number++;
        }
    }

    /// <summary>
    /// Внутренний узел односвязного списка.
    /// Пользователю класса не требуется работать с узлами напрямую.
    /// </summary>
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
