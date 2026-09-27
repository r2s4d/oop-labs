/*
Лабораторная работа № 7
Тема: Потоки и обработка исключений
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Использовать пользовательский класс времени в файловом вводе-выводе
и формировать собственное исключение при некорректных значениях.
*/

namespace Lab07;

// Пользовательский тип времени для обобщенного вектора.
public sealed class TimeOfDay
{
    public TimeOfDay(int hours, int minutes, int seconds)
    {
        if (hours is < 0 or > 23 || minutes is < 0 or > 59 || seconds is < 0 or > 59)
        {
            throw new InvalidTimeException("Часы должны быть от 0 до 23, минуты и секунды от 0 до 59.");
        }

        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public int Hours { get; }

    public int Minutes { get; }

    public int Seconds { get; }

    // Создает объект времени из строки формата ЧЧ:ММ:СС.
    public static TimeOfDay Parse(string text)
    {
        string[] parts = text.Split(':');
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out int hours) ||
            !int.TryParse(parts[1], out int minutes) ||
            !int.TryParse(parts[2], out int seconds))
        {
            throw new InvalidTimeException($"Строка '{text}' не является временем в формате ЧЧ:ММ:СС.");
        }

        return new TimeOfDay(hours, minutes, seconds);
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
    }
}
