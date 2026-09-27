/*
Лабораторная работа № 10
Тема: Обобщения и перегрузка операторов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Проверить обобщенный CVector, индексатор, арифметические операции,
равенство, ref, out, is, foreach и обработку исключений.
*/

using Lab10;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 10. Обобщения и перегрузка операторов.");
Console.WriteLine();

CVector<int> first = new([10, 20, 30]);
CVector<int> second = new([1, 2, 3]);

Console.WriteLine($"Первый вектор: {first}");
Console.WriteLine($"Второй вектор: {second}");
Console.WriteLine($"Разность: {first - second}");
Console.WriteLine($"Первый вектор + 5: {first + 5}");
Console.WriteLine($"Векторы равны: {first == second}");
Console.WriteLine($"Векторы не равны: {first != second}");

// Индекс -1 будет преобразован методом в индекс последнего элемента.
int index = -1;
first.ReplaceAt(ref index, 99, out int previousValue);
Console.WriteLine($"По индексу {index} значение {previousValue} заменено на {first[index]}.");

// Оператор is используется внутри TryDescribe.
CVector<int>.TryDescribe(first, out string description);
Console.WriteLine(description);

// foreach работает благодаря реализации IEnumerable<T>.
Console.Write("Перебор foreach: ");
foreach (int value in first)
{
    Console.Write($"{value} ");
}
Console.WriteLine();

// Тот же класс проверяется с другим числовым типом.
CVector<double> doubles = new([1.5, 2.5, 3.5]);
Console.WriteLine($"Вектор double + 0,5: {doubles + 0.5}");

try
{
    // Векторы разного размера не могут участвовать в поэлементной разности.
    _ = first - new CVector<int>([1]);
}
catch (VectorException exception)
{
    Console.WriteLine($"Обработана ошибка: {exception.Message}");
}

// Создаем временный вектор для учебной демонстрации финализатора.
CreateTemporaryVector();
GC.Collect();
GC.WaitForPendingFinalizers();

GC.KeepAlive(first);
GC.KeepAlive(second);
GC.KeepAlive(doubles);

static void CreateTemporaryVector()
{
    CVector<int> temporary = new([7, 8]);
    Console.WriteLine($"Временный объект: {temporary}");
}
