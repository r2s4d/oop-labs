/*
Лабораторная работа № 6
Тема: Обобщенные классы и методы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Проверить обобщенный CVector и поиск ближайшего элемента для int,
double и пользовательского класса времени.
*/

using Lab06;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 6. Обобщенные классы и методы.");
Console.WriteLine();

// Проверяем обобщенный класс и алгоритм с целыми числами.
CVector<int> integers = new([2, 8, 15, 21]);
int closestInteger = GenericAlgorithms.FindClosest(integers, 13, (left, right) => left - right);
Console.WriteLine($"Целые числа: {integers}");
Console.WriteLine($"Ближайшее к 13: {closestInteger}");

// Проверяем тот же алгоритм с вещественными числами.
CVector<double> doubles = new([1.5, 3.2, 7.8, 9.1]);
double closestDouble = GenericAlgorithms.FindClosest(doubles, 8.0, (left, right) => left - right);
Console.WriteLine($"Вещественные числа: {doubles}");
Console.WriteLine($"Ближайшее к 8,0: {closestDouble:F1}");

// Пользовательский класс времени также становится параметром CVector<T>.
CVector<TimeOfDay> times = new(
[
    new TimeOfDay(8, 0, 0),
    new TimeOfDay(12, 30, 0),
    new TimeOfDay(18, 45, 0)
]);

TimeOfDay targetTime = new(13, 0, 0);
TimeOfDay closestTime = GenericAlgorithms.FindClosest(
    times,
    targetTime,
    (left, right) => left.TotalSeconds - right.TotalSeconds);

Console.WriteLine($"Объекты времени: {times}");
Console.WriteLine($"Ближайшее к {targetTime}: {closestTime}");

// Перегруженная операция пользовательского класса позволяет прибавить
// одну минуту ко всем элементам обобщенного вектора.
CVector<TimeOfDay> shiftedTimes = times + 60;
Console.WriteLine($"После прибавления 60 секунд: {shiftedTimes}");

// Проверяем сохраненные операции проекта лабораторной работы № 5.
CVector<int> difference = integers - new CVector<int>([1, 1, 1, 1]);
Console.WriteLine($"Результат вычитания: {difference}");
