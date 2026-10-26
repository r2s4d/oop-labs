/*
Лабораторная работа № 8
Тема: Контейнеры и алгоритмы
Вариант: 9
Выполнил: Рассадин Егор

Задание (полный текст):
Проект должен содержать демонстрации: использования контейнерных классов для
хранения встроенных типов данных; использования контейнерных классов для
хранения пользовательских типов данных; использования алгоритмов STL.
Выполнить пункты а)-з):
а. Создать объект-контейнер в соответствии с вариантом задания и заполнить его
   данными согласно варианту. Просмотреть контейнер.
б. Изменить контейнер, удалив из него одни элементы и заменив другие.
в. Просмотреть контейнер, используя для доступа к элементам итераторы.
г. Изменить контейнер, удалив из него n элементов после заданного.
   Просмотреть контейнер.
д. Отсортировать контейнер по убыванию элементов. Просмотреть контейнер.
е. Выполнить пункты а)-г), но для данных пользовательского типа (класс
   лабораторной работы № 1). Для вставки и удаления элементов использовать
   операции, определенные в классе контейнера. Для ввода-вывода объектов
   пользовательского класса перегрузить операции >> и <<.
ж. Используя подходящий алгоритм, найти в контейнере пользовательского типа
   заданный элемент (задать значение самостоятельно).
з. Подсчитать, сколько элементов, удовлетворяющих заданному условию, содержат
   все контейнеры проекта.
Вариант 9: контейнер STL vector, встроенный тип данных double.
(В C# vector это List<T>. Операторов >> и << для ввода-вывода в C# нет, их
роль выполняют ToString для вывода и Parse для ввода в классе TimeOfDay.)

Что сделано (по файлам):
- ContainerAlgorithms.cs: общие методы RemoveAfter (удалить n элементов после
  заданного, пункт г) и Print (просмотр контейнера).
- TimeOfDay.cs: пользовательский класс из лабы 1. Реализует IComparable
  (для сортировки) и IEquatable (для поиска), есть Parse и ToString.
- Program.cs (этот файл): пункты а)-з) сначала для List<double>, затем для
  List<TimeOfDay>.

Вывод программы (сокращенно):
Исходный List<double>: [4,2, 8,5, 1,1, 7,1, 3,6, 9]
Сортировка по убыванию: [9, 8,5, 1,1]
Исходные объекты: [08:30:00, 12:00:00, 16:45:00, 20:10:00, 10:15:00]
Время по убыванию: [16:45:00, 12:30:00, 10:15:00]
Найдено значение: 16:45:00.
Чисел больше 5 и объектов времени после полудня: 4
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
// Лямбда (left, right) => right.CompareTo(left): аргументы поменяны местами, поэтому по убыванию.
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
// Тип со знаком ? (TimeOfDay?) значит "может быть null", если элемент не найден.
TimeOfDay? found = times.Find(time => time.Equals(target));
Console.WriteLine(found is null
    ? $"Значение {target} не найдено."
    : $"Найдено значение: {found}.");

// Условия отличаются для разных типов, поэтому результаты складываются отдельно.
// Count(условие) считает элементы, для которых лямбда вернула true (пункт з).
int matchingNumbers = numbers.Count(number => number > 5.0);
int matchingTimes = times.Count(time => time.Hours >= 12);
Console.WriteLine($"Чисел больше 5 и объектов времени после полудня: {matchingNumbers + matchingTimes}");
