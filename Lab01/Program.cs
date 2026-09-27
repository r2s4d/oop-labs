/*
Лабораторная работа № 1
Тема: Классы и объекты
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Продемонстрировать работу класса времени, его конструкторов, методов,
проверок, форматов вывода и финализатора.
*/

using Lab01;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 1. Классы и объекты.");
Console.WriteLine();

// Все объекты создаются внутри метода. После выхода из него на объекты
// не остается ссылок, и сборщик мусора может их уничтожить.
RunDemonstration();

// Финализатор запускается сборщиком мусора, а не вызывается напрямую.
// Эти две команды используются только для учебной демонстрации.
Console.WriteLine();
Console.WriteLine("Запускаем сборщик мусора:");
GC.Collect();
GC.WaitForPendingFinalizers();

static void RunDemonstration()
{
    // Создаем объект конструктором без параметров.
    TimeOfDay midnight = new();
    Console.WriteLine($"Начальное значение: {midnight.ToNumericString()}");

    // Создаем объект конструктором с параметрами и выводим его двумя способами.
    TimeOfDay lessonTime = new(15, 57, 30);
    Console.WriteLine(lessonTime.ToRussianString());
    Console.WriteLine(lessonTime.ToTwelveHourString());

    // Свойства позволяют получить каждую часть времени отдельно.
    Console.WriteLine($"Час: {lessonTime.Hours}, минуты: {lessonTime.Minutes}, секунды: {lessonTime.Seconds}");

    // Конструктор копирования создает новый объект с теми же значениями.
    TimeOfDay copiedTime = new(lessonTime);
    copiedTime.SetTime(21, 10, 5);
    Console.WriteLine($"Измененная копия: {copiedTime.ToNumericString()}");
    Console.WriteLine($"Исходный объект: {lessonTime.ToNumericString()}");

    // Проверяем реакцию класса на недопустимое значение.
    try
    {
        lessonTime.Hours = 25;
    }
    catch (ArgumentOutOfRangeException exception)
    {
        Console.WriteLine($"Ошибка проверки данных: {exception.Message}");
    }
}
