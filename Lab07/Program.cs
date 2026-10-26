/*
Лабораторная работа № 7
Тема: Потоки и обработка исключений
Вариант: 9
Выполнил: Рассадин Егор

Задание (полный текст):
Модифицировать проект, созданный в лабораторной работе № 6. Реализовать
ввод-вывод данных на экран и в файл через потоковые классы. Например,
считать из текстового файла количество и типы создаваемых объектов, а также
их значения. Сгенерировать и обработать как минимум пять различных
исключительных ситуаций. Например, не позволять при инициализации объектов
передавать конструкторам неверные данные, обрабатывать ошибки памяти и
ошибки работы с файлами. Построить иерархию классов исключений.

Что сделано (по файлам):
- LabExceptions.cs: иерархия исключений. Базовый LabException, от него шесть
  наследников (размер вектора, индекс, несовпадение размеров, формат данных,
  файл, время).
- VectorFileService.cs: запись и чтение файла через StreamWriter и
  StreamReader. Формат: первая строка количество объектов, затем строки
  вида "int|1;2;3", где до "|" тип, после значения.
- CVector.cs, TimeOfDay.cs: классы из прошлых лаб, теперь бросают свои исключения.
- Program.cs (этот файл): пишет файл, читает его обратно, затем по очереди
  вызывает шесть ошибок и ловит каждую.

Вывод программы (сокращенно):
Из файла прочитано объектов: 3
Тип CVector<Int32>: [1, 2, 3]
Тип CVector<Double>: [1,5, 2,75, 4]
Тип CVector<TimeOfDay>: [08:30:00, 14:15:30]
Отрицательный размер: InvalidVectorSizeException - Размер вектора не может быть отрицательным...
Проверка 'Отрицательный размер' завершена.
(так же для ошибок: некорректное время, неверный индекс, разные размеры,
отсутствующий файл, неверный формат)
*/

using Lab07;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 7. Потоки и обработка исключений.");
Console.WriteLine();

// Файл создается рядом с исполняемой программой.
string dataPath = Path.Combine(AppContext.BaseDirectory, "vectors.txt");

List<object> sourceVectors =
[
    new CVector<int>([1, 2, 3]),
    new CVector<double>([1.5, 2.75, 4.0]),
    new CVector<TimeOfDay>([new TimeOfDay(8, 30, 0), new TimeOfDay(14, 15, 30)])
];

try
{
    // Сначала записываем объекты, затем читаем их из того же файла.
    VectorFileService.Write(dataPath, sourceVectors);
    List<object> loadedVectors = VectorFileService.Read(dataPath);

    Console.WriteLine($"Из файла прочитано объектов: {loadedVectors.Count}");
    foreach (object vector in loadedVectors)
    {
        // Для обобщенного типа выводим и параметр, например CVector<Int32>.
        Type type = vector.GetType();
        string typeName = $"CVector<{type.GetGenericArguments()[0].Name}>";
        Console.WriteLine($"Тип {typeName}: {vector}");
    }
}
// Один catch по базовому классу LabException ловит любую из наших ошибок.
catch (LabException exception)
{
    Console.WriteLine($"Ошибка файловой операции: {exception.Message}");
}

Console.WriteLine();
Console.WriteLine("Демонстрация исключений:");

// 1. Недопустимый размер вектора.
ShowException("Отрицательный размер", () => _ = new CVector<int>(-2));

// 2. Недопустимое время.
ShowException("Некорректное время", () => _ = new TimeOfDay(25, 0, 0));

// 3. Выход за границы вектора.
ShowException("Неверный индекс", () => Console.WriteLine(new CVector<int>([1, 2])[5]));

// 4. Операция над векторами разных размеров.
ShowException("Разные размеры", () =>
{
    _ = new CVector<int>([1, 2, 3]) - new CVector<int>([1]);
});

// 5. Попытка прочитать отсутствующий файл.
ShowException("Отсутствующий файл", () =>
{
    _ = VectorFileService.Read(Path.Combine(AppContext.BaseDirectory, "missing.txt"));
});

// 6. Ошибка формата входных данных.
ShowException("Неверный формат", () => _ = TimeOfDay.Parse("не время"));

// Выполняет действие и единообразно показывает перехваченное исключение.
// Action это любое действие без результата, его передаем в виде лямбды () => ...
static void ShowException(string title, Action action)
{
    try
    {
        action();
    }
    catch (LabException exception)
    {
        Console.WriteLine($"{title}: {exception.GetType().Name} - {exception.Message}");
    }
    finally
    {
        // Блок finally выполняется независимо от наличия ошибки.
        Console.WriteLine($"Проверка '{title}' завершена.");
    }
}
