/*
Лабораторная работа № 2
Тема: Наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Построить иерархию классов: транспортное средство, автомобиль и поезд.
Задать поля и методы, затем создать объекты разных классов.
*/

namespace Lab02;

/// <summary>
/// Базовый класс с общими характеристиками транспортного средства.
/// </summary>
public class TransportVehicle
{
    private double _averageSpeed;
    private int _yearOfManufacture;

    /// <summary>
    /// Создает транспорт с безопасными начальными значениями.
    /// </summary>
    public TransportVehicle() : this(1, "Не указано", DateTime.Now.Year)
    {
    }

    /// <summary>
    /// Создает транспорт с заданными общими характеристиками.
    /// </summary>
    public TransportVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
    {
        AverageSpeed = averageSpeed;
        FuelType = fuelType;
        YearOfManufacture = yearOfManufacture;
    }

    /// <summary>
    /// Средняя скорость в километрах в час.
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
    /// Название используемого топлива.
    /// </summary>
    public string FuelType { get; set; }

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
    /// Возвращает текстовое описание транспортного средства.
    /// Метод virtual разрешает производным классам изменить результат.
    /// </summary>
    public virtual string GetDescription()
    {
        return $"Транспорт: скорость {AverageSpeed:F1} км/ч, топливо {FuelType}, год {YearOfManufacture}";
    }
}
