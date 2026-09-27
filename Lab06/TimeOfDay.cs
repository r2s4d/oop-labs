/*
Лабораторная работа № 6
Тема: Обобщенные классы и методы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать класс времени из лабораторной работы № 1 как параметр
обобщенного класса и обобщенного метода.
*/

namespace Lab06;

/// <summary>
/// Пользовательский тип для проверки обобщенного кода.
/// </summary>
public sealed class TimeOfDay : IEquatable<TimeOfDay>
{
    private const int SecondsPerDay = 24 * 60 * 60;

    public TimeOfDay(int hours, int minutes, int seconds)
    {
        if (hours is < 0 or > 23 || minutes is < 0 or > 59 || seconds is < 0 or > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(hours), "Указано недопустимое время.");
        }

        TotalSeconds = hours * 3600 + minutes * 60 + seconds;
    }

    private TimeOfDay(int totalSeconds)
    {
        // Приводим результат к одним суткам, включая отрицательные значения.
        TotalSeconds = ((totalSeconds % SecondsPerDay) + SecondsPerDay) % SecondsPerDay;
    }

    public int TotalSeconds { get; }

    public int Hours => TotalSeconds / 3600;

    public int Minutes => TotalSeconds % 3600 / 60;

    public int Seconds => TotalSeconds % 60;

    /// <summary>
    /// Прибавляет заданное количество секунд и возвращает новый объект.
    /// </summary>
    public static TimeOfDay operator +(TimeOfDay time, int seconds)
    {
        ArgumentNullException.ThrowIfNull(time);
        return new TimeOfDay(time.TotalSeconds + seconds);
    }

    public bool Equals(TimeOfDay? other)
    {
        return other is not null && TotalSeconds == other.TotalSeconds;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TimeOfDay);
    }

    public override int GetHashCode()
    {
        return TotalSeconds;
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
    }
}
