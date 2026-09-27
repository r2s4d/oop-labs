/*
Лабораторная работа № 8
Тема: Контейнеры и алгоритмы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать общие операции удаления n элементов после заданной позиции
и просмотра контейнера для встроенного и пользовательского типов.
*/

namespace Lab08;

// Содержит вспомогательные обобщенные операции над списками.
public static class ContainerAlgorithms
{
    // Удаляет не более count элементов, расположенных после заданного индекса.
    public static void RemoveAfter<T>(List<T> items, int index, int count)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (index < -1 || index >= items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Заданный индекс отсутствует в контейнере.");
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Количество удаляемых элементов не может быть отрицательным.");
        }

        int firstIndexToRemove = index + 1;
        int availableCount = items.Count - firstIndexToRemove;
        int actualCount = Math.Min(count, availableCount);

        if (actualCount > 0)
        {
            items.RemoveRange(firstIndexToRemove, actualCount);
        }
    }

    // Выводит элементы контейнера в одну строку.
    public static void Print<T>(string title, IEnumerable<T> items)
    {
        Console.WriteLine($"{title}: [{string.Join(", ", items)}]");
    }
}
