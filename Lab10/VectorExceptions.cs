/*
Лабораторная работа № 10
Тема: Обобщения и перегрузка операторов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Обрабатывать ошибки обобщенного CVector при неверном размере,
индексе и несовместимых размерах операндов.
*/

namespace Lab10;

// Базовое исключение обобщенного вектора.
public class VectorException : Exception
{
    public VectorException(string message) : base(message)
    {
    }
}

// Возникает при обращении к отсутствующему элементу.
public sealed class VectorIndexException : VectorException
{
    public VectorIndexException(int index, int count)
        : base($"Индекс {index} выходит за границы вектора размером {count}.")
    {
    }
}

// Возникает при операции над векторами разного размера.
public sealed class VectorSizeMismatchException : VectorException
{
    public VectorSizeMismatchException(int leftCount, int rightCount)
        : base($"Размеры векторов не совпадают: {leftCount} и {rightCount}.")
    {
    }
}
