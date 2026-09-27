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

// Базовый класс с общими характеристиками транспортного средства.
public class TransportVehicle
{
    private double _averageSpeed;
    private int _yearOfManufacture;

    // Создает транспорт с безопасными начальными значениями.
    public TransportVehicle() : this(1, "Не указано", DateTime.Now.Year)
    {
    }

    // Создает транспорт с заданными общими характеристиками.
    public TransportVehicle(double averageSpeed, string fuelType, int yearOfManufacture)
    {
        AverageSpeed = averageSpeed;
        FuelType = fuelType;
        YearOfManufacture = yearOfManufacture;
    }

    // Средняя скорость в километрах в час.
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

    // Название используемого топлива.
    public string FuelType { get; set; }

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

    // Возвращает текстовое описание транспортного средства.
    // Метод virtual разрешает производным классам изменить результат.
    public virtual string GetDescription()
    {
        return $"Транспорт: скорость {AverageSpeed:F1} км/ч, топливо {FuelType}, год {YearOfManufacture}";
    }
}
