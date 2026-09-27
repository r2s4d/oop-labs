/*
Лабораторная работа № 7
Тема: Потоки и обработка исключений
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать запись и чтение количества, типов и значений объектов
с помощью StreamWriter и StreamReader.
*/

using System.Globalization;

namespace Lab07;

/// <summary>
/// Выполняет файловый ввод и вывод поддерживаемых векторов.
/// </summary>
public static class VectorFileService
{
    /// <summary>
    /// Записывает количество объектов, их типы и значения в текстовый файл.
    /// </summary>
    public static void Write(string path, IEnumerable<object> vectors)
    {
        List<object> vectorList = vectors.ToList();

        try
        {
            using StreamWriter writer = new(path, false);
            writer.WriteLine(vectorList.Count);

            foreach (object vector in vectorList)
            {
                string line = vector switch
                {
                    CVector<int> integers => "int|" + string.Join(";", integers),
                    CVector<double> doubles => "double|" + string.Join(";", doubles.Select(
                        value => value.ToString(CultureInfo.InvariantCulture))),
                    CVector<TimeOfDay> times => "time|" + string.Join(";", times),
                    _ => throw new InvalidVectorDataException(
                        $"Тип {vector.GetType().Name} не поддерживается файловым форматом.")
                };

                writer.WriteLine(line);
            }
        }
        catch (IOException exception)
        {
            throw new VectorFileException($"Не удалось записать файл '{path}'.", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new VectorFileException($"Нет доступа для записи файла '{path}'.", exception);
        }
    }

    /// <summary>
    /// Читает объекты из текстового файла и восстанавливает их типы.
    /// </summary>
    public static List<object> Read(string path)
    {
        try
        {
            using StreamReader reader = new(path);

            if (!int.TryParse(reader.ReadLine(), out int expectedCount) || expectedCount < 0)
            {
                throw new InvalidVectorDataException("Первая строка должна содержать неотрицательное количество объектов.");
            }

            List<object> result = [];
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                result.Add(ParseVector(line));
            }

            if (result.Count != expectedCount)
            {
                throw new InvalidVectorDataException(
                    $"В файле заявлено {expectedCount} объектов, но прочитано {result.Count}.");
            }

            return result;
        }
        catch (LabException)
        {
            // Прикладное исключение уже содержит понятное сообщение.
            throw;
        }
        catch (IOException exception)
        {
            throw new VectorFileException($"Не удалось прочитать файл '{path}'.", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new VectorFileException($"Нет доступа для чтения файла '{path}'.", exception);
        }
        catch (Exception exception) when (exception is FormatException or OverflowException)
        {
            throw new InvalidVectorDataException("В файле встретилось некорректное числовое значение.", exception);
        }
    }

    private static object ParseVector(string line)
    {
        string[] parts = line.Split('|', 2);
        if (parts.Length != 2)
        {
            throw new InvalidVectorDataException($"Строка '{line}' не содержит разделитель типа и данных.");
        }

        string[] values = string.IsNullOrWhiteSpace(parts[1])
            ? []
            : parts[1].Split(';');

        return parts[0].ToLowerInvariant() switch
        {
            "int" => new CVector<int>(values.Select(int.Parse)),
            "double" => new CVector<double>(values.Select(value =>
                double.Parse(value, CultureInfo.InvariantCulture))),
            "time" => new CVector<TimeOfDay>(values.Select(TimeOfDay.Parse)),
            _ => throw new InvalidVectorDataException($"Неизвестный тип вектора '{parts[0]}'.")
        };
    }
}
