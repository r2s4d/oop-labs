/*
Лабораторная работа № 1
Тема: Классы и объекты
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать класс времени с полями для часов, минут и секунд. Реализовать
конструкторы, методы установки и получения значений, проверку данных,
два формата вывода и демонстрацию создания и уничтожения объектов.
*/

namespace Lab01;

/// <summary>
/// Представляет время суток и следит за корректностью его компонентов.
/// </summary>
public class TimeOfDay
{
    // Поля закрыты от прямого изменения. Доступ к ним выполняется через свойства.
    private int _hours;
    private int _minutes;
    private int _seconds;

    /// <summary>
    /// Создает объект со временем 00:00:00.
    /// </summary>
    public TimeOfDay() : this(0, 0, 0)
    {
        Console.WriteLine("Вызван конструктор без параметров.");
    }

    /// <summary>
    /// Создает объект с указанным временем.
    /// </summary>
    /// <param name="hours">Часы от 0 до 23.</param>
    /// <param name="minutes">Минуты от 0 до 59.</param>
    /// <param name="seconds">Секунды от 0 до 59.</param>
    public TimeOfDay(int hours, int minutes, int seconds)
    {
        // Свойства используются вместо прямой записи, чтобы сразу проверить значения.
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;

        Console.WriteLine($"Вызван конструктор с параметрами: {ToNumericString()}.");
    }

    /// <summary>
    /// Создает независимую копию другого объекта времени.
    /// </summary>
    /// <param name="other">Объект, значения которого требуется скопировать.</param>
    public TimeOfDay(TimeOfDay other)
    {
        ArgumentNullException.ThrowIfNull(other);

        _hours = other._hours;
        _minutes = other._minutes;
        _seconds = other._seconds;

        Console.WriteLine($"Вызван конструктор копирования: {ToNumericString()}.");
    }

    /// <summary>
    /// Возвращает или изменяет часы.
    /// </summary>
    public int Hours
    {
        get => _hours;
        set
        {
            ValidateRange(value, 0, 23, nameof(Hours));
            _hours = value;
        }
    }

    /// <summary>
    /// Возвращает или изменяет минуты.
    /// </summary>
    public int Minutes
    {
        get => _minutes;
        set
        {
            ValidateRange(value, 0, 59, nameof(Minutes));
            _minutes = value;
        }
    }

    /// <summary>
    /// Возвращает или изменяет секунды.
    /// </summary>
    public int Seconds
    {
        get => _seconds;
        set
        {
            ValidateRange(value, 0, 59, nameof(Seconds));
            _seconds = value;
        }
    }

    /// <summary>
    /// Одновременно устанавливает все части времени.
    /// </summary>
    public void SetTime(int hours, int minutes, int seconds)
    {
        // Сначала проверяем все аргументы. При ошибке объект не изменится частично.
        ValidateRange(hours, 0, 23, nameof(hours));
        ValidateRange(minutes, 0, 59, nameof(minutes));
        ValidateRange(seconds, 0, 59, nameof(seconds));

        _hours = hours;
        _minutes = minutes;
        _seconds = seconds;
    }

    /// <summary>
    /// Возвращает строку в формате "15 часов 57 минут 30 секунд".
    /// </summary>
    public string ToRussianString()
    {
        return $"{_hours} часов {_minutes} минут {_seconds} секунд";
    }

    /// <summary>
    /// Возвращает время в 12-часовом формате с обозначением a.m. или p.m.
    /// </summary>
    public string ToTwelveHourString()
    {
        string period = _hours < 12 ? "a.m." : "p.m.";

        // Остаток от деления переводит 24-часовое значение в диапазон от 0 до 11.
        // Значение 0 в 12-часовой записи должно отображаться как 12.
        int twelveHour = _hours % 12;
        if (twelveHour == 0)
        {
            twelveHour = 12;
        }

        return $"{twelveHour} {period} {_minutes} минут {_seconds} секунд";
    }

    /// <summary>
    /// Возвращает время в привычном цифровом формате.
    /// </summary>
    public string ToNumericString()
    {
        return $"{_hours:D2}:{_minutes:D2}:{_seconds:D2}";
    }

    /// <summary>
    /// Проверяет, входит ли число в допустимый закрытый диапазон.
    /// </summary>
    private static void ValidateRange(int value, int minimum, int maximum, string parameterName)
    {
        if (value < minimum || value > maximum)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                $"Значение должно находиться в диапазоне от {minimum} до {maximum}.");
        }
    }

    /// <summary>
    /// Финализатор демонстрирует уничтожение объекта сборщиком мусора.
    /// В реальной программе он не нужен, так как класс не хранит неуправляемые ресурсы.
    /// </summary>
    ~TimeOfDay()
    {
        Console.WriteLine($"Финализирован объект времени {ToNumericString()}.");
    }
}
