/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать класс испытания как отдельную ветвь иерархии тестов.
*/

namespace Lab09;

/// <summary>
/// Испытание с ограниченным количеством попыток.
/// </summary>
public class Trial : Test
{
    private int _attempts;

    public Trial() : this("Испытание", DateTime.Today, 100, 0, 1)
    {
    }

    public Trial(string title, DateTime date, int maximumScore, int score, int attempts)
        : base(title, date, maximumScore, score)
    {
        Attempts = attempts;
    }

    public int Attempts
    {
        get => _attempts;
        set
        {
            if (value <= 0)
            {
                throw new AssessmentException("Количество попыток должно быть больше нуля.");
            }

            _attempts = value;
        }
    }

    public override bool IsPassed => Score >= MaximumScore / 2.0;

    public override string GetResultDescription()
    {
        return IsPassed
            ? "зачтено: испытание пройдено"
            : "не зачтено: испытание не пройдено";
    }

    public override string GetInfo()
    {
        return $"Испытание: {base.GetInfo()}, попыток {Attempts}, {GetResultDescription()}";
    }
}
