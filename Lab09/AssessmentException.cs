/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Обрабатывать ошибки иерархии тестов и экзаменов с помощью
пользовательского исключения, try, catch и finally.
*/

namespace Lab09;

// Описывает ошибку при создании или изменении формы контроля.
public sealed class AssessmentException : Exception
{
    public AssessmentException(string message) : base(message)
    {
    }
}
