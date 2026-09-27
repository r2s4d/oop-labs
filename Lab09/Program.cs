/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать объекты классов тест, экзамен, выпускной экзамен, испытание
и ООП-экзамен. Добавить объекты в System.Collections, применить
ref, out, исключения и показать работу sealed-класса.
*/

using System.Collections;
using Lab09;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 9. Интерфейсы и наследование.");
Console.WriteLine();

// ArrayList взят из пространства имен System.Collections по условию задания.
ArrayList assessments = new()
{
    new Exam("Экзамен по математике", new DateTime(2026, 6, 10), 100, 74, "Математика", 60),
    new FinalExam("Выпускной экзамен", new DateTime(2026, 6, 20), 100, 81, "Программирование", 70, "П-42"),
    new Trial("Вступительное испытание", new DateTime(2026, 5, 25), 50, 22, 2),
    new OopExam(new DateTime(2026, 6, 25), 100, 88, 70, "ООП-19", "Разработать иерархию классов")
};

// Отдельно вызываем конструктор без параметров и заполняем свойства.
Exam defaultExam = new()
{
    Title = "Дополнительный экзамен",
    Subject = "Информатика",
    Date = new DateTime(2026, 6, 30),
    Score = 65
};
assessments.Add(defaultExam);

foreach (object item in assessments)
{
    // ArrayList хранит object, поэтому перед использованием проверяем тип.
    if (item is Test test)
    {
        Console.WriteLine(test.GetInfo());
    }
}

Console.WriteLine();
Test.PrintCreatedCount();

try
{
    // ref передает переменную scoreForUpdate по ссылке.
    // out требует, чтобы метод обязательно присвоил переменной passed значение.
    int scoreForUpdate = 92;
    OopExam oopExam = (OopExam)assessments[3]!;
    oopExam.UpdateScore(ref scoreForUpdate, out bool passed);
    Console.WriteLine($"Новый балл: {scoreForUpdate}. Экзамен сдан: {passed}.");

    // Следующее значение специально выходит за допустимый диапазон.
    int invalidScore = 150;
    oopExam.UpdateScore(ref invalidScore, out _);
}
catch (AssessmentException exception)
{
    Console.WriteLine($"Обработана ошибка: {exception.Message}");
}
finally
{
    Console.WriteLine("Блок finally выполнен. Проверка изменения балла завершена.");
}
