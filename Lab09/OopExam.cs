/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать бесплодный класс ООП-экзамена. В C# такому требованию
соответствует модификатор sealed, запрещающий дальнейшее наследование.
*/

namespace Lab09;

// Конкретный выпускной экзамен по объектно-ориентированному программированию.
public sealed class OopExam : FinalExam
{
    public OopExam()
        : this(DateTime.Today, 100, 0, 70, "Нет", "Не задано")
    {
    }

    public OopExam(
        DateTime date,
        int maximumScore,
        int score,
        int passingScore,
        string protocolNumber,
        string practicalTask)
        : base(
            "ООП-экзамен",
            date,
            maximumScore,
            score,
            "Объектно-ориентированное программирование",
            passingScore,
            protocolNumber)
    {
        PracticalTask = practicalTask;
    }

    public string PracticalTask { get; set; }

    public override bool IsPassed => Score >= PassingScore;

    public override string GetResultDescription()
    {
        return IsPassed
            ? "зачтено: ООП-экзамен сдан"
            : "не зачтено: ООП-экзамен не сдан";
    }

    public override string GetInfo()
    {
        return $"{base.GetInfo()}, практическое задание: {PracticalTask}";
    }
}
