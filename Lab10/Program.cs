/*
Лабораторная работа № 10
Тема: Обобщения и перегрузка операторов
Вариант: 9
Выполнил: Рассадин Егор

Задание (полный текст):
Создать заданный в варианте обобщенный класс. Определить в классе
индексаторы, конструкторы, деструктор, необходимые функции и заданные
перегруженные операции. В методах класса для работы с аргументами используйте
ref- и out-параметры и операторы is или as, foreach. Используйте обработку
исключительных ситуаций.
Вариант 9: класс CVector. Дополнительно перегрузить операции: "-" разность
двух векторов; "+" сложение элементов вектора с целым; "!=" и "==" проверка
на неравенство и равенство.

Что сделано (по файлам):
- CVector.cs: обобщенный класс CVector<T> с ограничением T : INumber<T> (T
  только числовой тип). Индексатор, конструкторы, финализатор, операторы
  -, +, ==, !=. Метод ReplaceAt с ref и out, метод TryDescribe с is и out,
  перебор через foreach (реализован IEnumerable<T>).
- VectorExceptions.cs: свои исключения. Базовый VectorException и два
  наследника (индекс, несовпадение размеров).
- Program.cs (этот файл): проверяет все операции и ловит исключение.

Вывод программы:
Первый вектор: [10, 20, 30]
Второй вектор: [1, 2, 3]
Разность: [9, 18, 27]
Первый вектор + 5: [15, 25, 35]
Векторы равны: False
Векторы не равны: True
По индексу 2 значение 30 заменено на 99.
Объект является CVector<Int32> размером 3.
Перебор foreach: 10 20 99
Вектор double + 0,5: [2, 3, 4]
Обработана ошибка: Размеры векторов не совпадают: 3 и 1.
Временный объект: [7, 8]
Финализирован CVector<Int32> размером 2.
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
// ref передает переменную index в метод, и метод может ее изменить. out int previousValue метод обязан заполнить сам.
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
// Один catch по базовому VectorException ловит все наши ошибки вектора.
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
