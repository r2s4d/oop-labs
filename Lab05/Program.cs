/*
Лабораторная работа № 5
Тема: Перегрузка операций
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Проверить все перегруженные операции класса CVector.
*/

using Lab05;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 5. Перегрузка операций.");
Console.WriteLine();

CVector first = new([10, 20, 30, 40]);
CVector second = new([1, 2, 3, 4]);

Console.WriteLine($"Первый вектор: {first}");
Console.WriteLine($"Второй вектор: {second}");

// Проверяем поэлементное вычитание двух векторов.
CVector difference = first - second;
Console.WriteLine($"Разность: {difference}");

// Проверяем прибавление одного числа ко всем элементам.
CVector increased = second + 5;
Console.WriteLine($"Второй вектор + 5: {increased}");

// Индексатор используется для чтения и записи элемента.
Console.WriteLine($"Элемент с индексом 2: {increased[2]}");
increased[2] = 100;
Console.WriteLine($"После изменения элемента: {increased}");

// Сравнение выполняется по значениям элементов, а не по ссылкам.
CVector copyOfFirst = new(first);
Console.WriteLine($"Первый вектор не равен копии: {first != copyOfFirst}");
Console.WriteLine($"Первый вектор не равен второму: {first != second}");

// Создаем временный объект для учебной демонстрации финализатора.
CreateTemporaryVector();
GC.Collect();
GC.WaitForPendingFinalizers();

GC.KeepAlive(first);
GC.KeepAlive(second);
GC.KeepAlive(difference);
GC.KeepAlive(increased);
GC.KeepAlive(copyOfFirst);

static void CreateTemporaryVector()
{
    CVector temporary = new([7, 8, 9]);
    Console.WriteLine($"Временный вектор: {temporary}");
}
