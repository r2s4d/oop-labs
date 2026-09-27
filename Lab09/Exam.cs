/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать классы экзамена и выпускного экзамена с конструкторами,
свойствами и переопределением методов.
*/

namespace Lab09;

// Экзамен с названием предмета и проходным баллом.
public class Exam : Test
{
    private int _passingScore;

    public Exam() : this("Обычный экзамен", DateTime.Today, 100, 0, "Не указан", 60)
    {
    }

    public Exam(
        string title,
        DateTime date,
        int maximumScore,
        int score,
        string subject,
        int passingScore)
        : base(title, date, maximumScore, score)
    {
        Subject = subject;
        PassingScore = passingScore;
    }

    public string Subject { get; set; }

    public int PassingScore
    {
        get => _passingScore;
        set
        {
            if (value < 0 || value > MaximumScore)
            {
                throw new AssessmentException("Проходной балл должен входить в диапазон экзамена.");
            }

            _passingScore = value;
        }
    }

    public override bool IsPassed => Score >= PassingScore;

    public override string GetResultDescription()
    {
        return IsPassed ? "зачтено: экзамен сдан" : "не зачтено: экзамен не сдан";
    }

    public override string GetInfo()
    {
        return $"Экзамен по предмету '{Subject}': {base.GetInfo()}, {GetResultDescription()}";
    }
}

// Выпускной экзамен дополнительно хранит номер протокола комиссии.
public class FinalExam : Exam
{
    public FinalExam()
        : this("Выпускной экзамен", DateTime.Today, 100, 0, "Не указан", 70, "Нет")
    {
    }

    public FinalExam(
        string title,
        DateTime date,
        int maximumScore,
        int score,
        string subject,
        int passingScore,
        string protocolNumber)
        : base(title, date, maximumScore, score, subject, passingScore)
    {
        ProtocolNumber = protocolNumber;
    }

    public string ProtocolNumber { get; set; }

    public override bool IsPassed => Score >= PassingScore;

    public override string GetResultDescription()
    {
        return IsPassed
            ? "зачтено: выпускной экзамен сдан"
            : "не зачтено: выпускной экзамен не сдан";
    }

    public override string GetInfo()
    {
        return $"{base.GetInfo()}, протокол {ProtocolNumber}";
    }
}
