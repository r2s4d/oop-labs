/*
Лабораторная работа № 6
Тема: Обобщенные классы и методы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать обобщенный метод, который возвращает элемент вектора,
максимально близкий к заданному значению.
*/

namespace Lab06;

/// <summary>
/// Содержит алгоритмы, которые не зависят от конкретного типа данных.
/// </summary>
public static class GenericAlgorithms
{
    /// <summary>
    /// Находит элемент с наименьшим расстоянием до заданного значения.
    /// Способ вычисления расстояния передается отдельной функцией.
    /// </summary>
    public static T FindClosest<T>(
        IEnumerable<T> source,
        T target,
        Func<T, T, double> distance)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(distance);

        using IEnumerator<T> enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new InvalidOperationException("Нельзя искать ближайший элемент в пустом векторе.");
        }

        T closest = enumerator.Current;
        double minimumDistance = Math.Abs(distance(closest, target));

        while (enumerator.MoveNext())
        {
            double currentDistance = Math.Abs(distance(enumerator.Current, target));
            if (currentDistance < minimumDistance)
            {
                closest = enumerator.Current;
                minimumDistance = currentDistance;
            }
        }

        return closest;
    }
}
