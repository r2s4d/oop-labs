/*
Лабораторная работа № 8
Тема: Контейнеры и алгоритмы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать класс времени из лабораторной работы № 1 как
пользовательский тип элементов контейнера.
*/

namespace Lab08;

/// <summary>
/// Время суток, которое можно сравнивать, сортировать и искать в коллекции.
/// </summary>
public sealed class TimeOfDay : IComparable<TimeOfDay>, IEquatable<TimeOfDay>
{
    public TimeOfDay(int hours, int minutes, int seconds)
    {
        if (hours is < 0 or > 23 || minutes is < 0 or > 59 || seconds is < 0 or > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(hours), "Указано недопустимое время.");
        }

        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public int Hours { get; }

    public int Minutes { get; }

    public int Seconds { get; }

    public int TotalSeconds => Hours * 3600 + Minutes * 60 + Seconds;

    /// <summary>
    /// Сравнивает два объекта по количеству секунд от начала суток.
    /// </summary>
    public int CompareTo(TimeOfDay? other)
    {
        return other is null ? 1 : TotalSeconds.CompareTo(other.TotalSeconds);
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

    /// <summary>
    /// Преобразует текст формата ЧЧ:ММ:СС в объект времени.
    /// </summary>
    public static TimeOfDay Parse(string text)
    {
        string[] parts = text.Split(':');
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out int hours) ||
            !int.TryParse(parts[1], out int minutes) ||
            !int.TryParse(parts[2], out int seconds))
        {
            throw new FormatException("Ожидается время в формате ЧЧ:ММ:СС.");
        }

        return new TimeOfDay(hours, minutes, seconds);
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
    }
}
