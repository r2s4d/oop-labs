/*
Лабораторная работа № 7
Тема: Потоки и обработка исключений
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать иерархию исключений и обработать не менее пяти различных
ошибочных ситуаций при работе с векторами, временем и файлами.
*/

namespace Lab07;

/// <summary>
/// Базовое исключение проекта. По нему можно перехватить любую
/// предусмотренную прикладную ошибку лабораторной работы.
/// </summary>
public class LabException : Exception
{
    public LabException(string message) : base(message)
    {
    }

    public LabException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Возникает при попытке создать вектор недопустимого размера.
/// </summary>
public sealed class InvalidVectorSizeException : LabException
{
    public InvalidVectorSizeException(int size)
        : base($"Размер вектора не может быть отрицательным. Получено: {size}.")
    {
    }
}

/// <summary>
/// Возникает при обращении за границы вектора.
/// </summary>
public sealed class VectorIndexException : LabException
{
    public VectorIndexException(int index, int count)
        : base($"Индекс {index} выходит за границы вектора размером {count}.")
    {
    }
}

/// <summary>
/// Возникает при операции над векторами разных размеров.
/// </summary>
public sealed class VectorSizeMismatchException : LabException
{
    public VectorSizeMismatchException(int leftSize, int rightSize)
        : base($"Размеры векторов не совпадают: {leftSize} и {rightSize}.")
    {
    }
}

/// <summary>
/// Возникает, если строка файла не соответствует ожидаемому формату.
/// </summary>
public sealed class InvalidVectorDataException : LabException
{
    public InvalidVectorDataException(string message) : base(message)
    {
    }

    public InvalidVectorDataException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Возникает при ошибке открытия, чтения или записи файла.
/// </summary>
public sealed class VectorFileException : LabException
{
    public VectorFileException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Возникает при создании некорректного времени.
/// </summary>
public sealed class InvalidTimeException : LabException
{
    public InvalidTimeException(string message) : base(message)
    {
    }
}
