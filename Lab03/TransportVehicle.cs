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

// Абстрактная основа иерархии транспортных средств.
// Создать объект этого класса напрямую нельзя (abstract class, new не сработает).
public abstract class TransportVehicle
{
    // Ссылка на первый узел общего связанного списка.
    // Поле static является единым для всех объектов и производных классов.
    // Подчеркивание в имени _head только обозначает закрытое поле, знак ? значит "может быть null".
    private static TransportNode? _head;

    // Ссылка на последний узел позволяет добавлять объекты в конец без обхода списка.
    private static TransportNode? _tail;

    private double _averageSpeed;
    private int _yearOfManufacture;

    // Создает транспорт с общими характеристиками.
    // Конструктор protected: вызвать его могут только наследники через base(...).
    protected TransportVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
    {
        AverageSpeed = averageSpeed;
        FuelType = string.IsNullOrWhiteSpace(fuelType)
            ? throw new ArgumentException("Тип топлива не должен быть пустым.", nameof(fuelType))
            : fuelType;
        YearOfManufacture = yearOfManufacture;
    }

    // Средняя скорость транспорта.
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

    // Тип топлива или источник энергии.
    public string FuelType { get; }

    // Год выпуска транспортного средства.
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

    // Среда движения: промежуточные абстрактные классы задают ее для своей ветви иерархии.
    // abstract-свойство без тела, переопределяется через override в GroundVehicle и WaterVehicle.
    public abstract string MovementEnvironment { get; }

    // Производный класс обязан сформировать собственное описание объекта.
    // abstract-метод без тела (аналог чисто виртуальной функции), override в Car, Train, Boat.
    public abstract string GetDescription();

    // Добавляет транспорт в конец общего связанного списка.
    // static-метод: вызывается через класс, TransportVehicle.AddToRegistry(...).
    public static void AddToRegistry(TransportVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        TransportNode node = new(vehicle);

        if (_tail is null)
        {
            // Список был пустым: новый узел одновременно начало и конец.
            _head = node;
        }
        else
        {
            // Прежний последний узел ссылается на новый.
            _tail.Next = node;
        }

        _tail = node;
    }

    // Выводит все объекты из общего связанного списка.
    // Метод вызывается через имя класса, так как он статический.
    // GetDescription() ниже вызывается полиморфно: срабатывает версия реального класса.
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

    // Внутренний узел односвязного списка.
    // Пользователю класса не требуется работать с узлами напрямую.
    // Вложенный класс узла: хранит объект транспорта и ссылку Next на следующий узел.
    private sealed class TransportNode
    {
        public TransportNode(TransportVehicle value)
        {
            Value = value;
        }

        public TransportVehicle Value { get; }

        // Ссылка на следующий узел заполняется при добавлении очередного объекта.
        public TransportNode? Next { get; set; }
    }
}
