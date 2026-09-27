/*
Лабораторная работа № 8
Тема: Контейнеры и алгоритмы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Продемонстрировать контейнер vector для типа double и пользовательского
класса времени: заполнение, изменение, перебор, удаление, сортировку,
поиск и подсчет элементов по условию.
*/

using Lab08;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 8. Контейнеры и алгоритмы.");
Console.WriteLine();

// В C# ближайшим аналогом динамического vector является List<T>.
List<double> numbers = [4.2, 8.5, 1.1, 7.1, 3.6, 9.0];
ContainerAlgorithms.Print("Исходный List<double>", numbers);

// Удаляем одно значение и заменяем другое.
numbers.Remove(4.2);
int replacementIndex = numbers.IndexOf(7.1);
if (replacementIndex >= 0)
{
    numbers[replacementIndex] = 7.5;
}

ContainerAlgorithms.Print("После удаления и замены", numbers);

// foreach получает элементы через перечислитель коллекции.
Console.Write("Просмотр через итератор: ");
foreach (double number in numbers)
{
    Console.Write($"{number:F1} ");
}
Console.WriteLine();

// Удаляем два элемента после элемента с индексом 1.
ContainerAlgorithms.RemoveAfter(numbers, index: 1, count: 2);
ContainerAlgorithms.Print("После удаления двух элементов", numbers);

// List<T>.Sort принимает функцию сравнения. Здесь задается обратный порядок.
numbers.Sort((left, right) => right.CompareTo(left));
ContainerAlgorithms.Print("Сортировка по убыванию", numbers);

Console.WriteLine();
Console.WriteLine("Контейнер пользовательского типа:");

List<TimeOfDay> times =
[
    TimeOfDay.Parse("08:30:00"),
    TimeOfDay.Parse("12:00:00"),
    TimeOfDay.Parse("16:45:00"),
    TimeOfDay.Parse("20:10:00"),
    TimeOfDay.Parse("10:15:00")
];

ContainerAlgorithms.Print("Исходные объекты", times);

// Методы List<T> работают с пользовательскими объектами так же, как с числами.
times.RemoveAt(0);
times[0] = new TimeOfDay(12, 30, 0);
ContainerAlgorithms.Print("После удаления и замены", times);

Console.WriteLine("Просмотр объектов через foreach:");
foreach (TimeOfDay time in times)
{
    Console.WriteLine($"  {time}");
}

ContainerAlgorithms.RemoveAfter(times, index: 1, count: 1);
ContainerAlgorithms.Print("После удаления одного объекта", times);

times.Sort((left, right) => right.CompareTo(left));
ContainerAlgorithms.Print("Время по убыванию", times);

// Ищем объект по значению. Метод Equals класса TimeOfDay сравнивает время.
TimeOfDay target = new(16, 45, 0);
TimeOfDay? found = times.Find(time => time.Equals(target));
Console.WriteLine(found is null
    ? $"Значение {target} не найдено."
    : $"Найдено значение: {found}.");

// Условия отличаются для разных типов, поэтому результаты складываются отдельно.
int matchingNumbers = numbers.Count(number => number > 5.0);
int matchingTimes = times.Count(time => time.Hours >= 12);
Console.WriteLine($"Чисел больше 5 и объектов времени после полудня: {matchingNumbers + matchingTimes}");
