/*
Лабораторная работа № 6
Тема: Обобщенные классы и методы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Преобразовать CVector в обобщенный класс. Проверить его со стандартными
и пользовательскими типами. Сохранить операции предыдущей работы.
*/

using System.Collections;

namespace Lab06;

/// <summary>
/// Обобщенный вектор, способный хранить элементы типа T.
/// </summary>
public class CVector<T> : IEnumerable<T>, IEquatable<CVector<T>>
{
    private readonly T[] _items;

    /// <summary>
    /// Создает пустой вектор.
    /// </summary>
    public CVector()
    {
        _items = [];
    }

    /// <summary>
    /// Создает вектор из переданной последовательности.
    /// </summary>
    public CVector(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    /// <summary>
    /// Создает независимую копию массива ссылок или значений.
    /// </summary>
    public CVector(CVector<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);
        _items = (T[])other._items.Clone();
    }

    public int Count => _items.Length;

    /// <summary>
    /// Предоставляет доступ к элементу по индексу.
    /// </summary>
    public T this[int index]
    {
        get
        {
            ValidateIndex(index);
            return _items[index];
        }
        set
        {
            ValidateIndex(index);
            _items[index] = value;
        }
    }

    /// <summary>
    /// Вычитает соответствующие элементы двух векторов.
    /// dynamic используется потому, что не каждый T имеет арифметический оператор.
    /// Ошибка понятным образом сообщается во время выполнения.
    /// </summary>
    public static CVector<T> operator -(CVector<T> left, CVector<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        if (left.Count != right.Count)
        {
            throw new ArgumentException("Для вычитания векторы должны иметь одинаковый размер.");
        }

        T[] result = new T[left.Count];

        try
        {
            for (int index = 0; index < result.Length; index++)
            {
                dynamic leftItem = left[index]!;
                dynamic rightItem = right[index]!;
                result[index] = (T)(leftItem - rightItem);
            }
        }
        catch (Exception exception) when (exception is not ArgumentException)
        {
            throw new InvalidOperationException($"Тип {typeof(T).Name} не поддерживает вычитание.", exception);
        }

        return new CVector<T>(result);
    }

    /// <summary>
    /// Прибавляет целое число к каждому элементу.
    /// Пользовательский тип может поддержать операцию собственной перегрузкой.
    /// </summary>
    public static CVector<T> operator +(CVector<T> vector, int value)
    {
        ArgumentNullException.ThrowIfNull(vector);
        T[] result = new T[vector.Count];

        try
        {
            for (int index = 0; index < result.Length; index++)
            {
                dynamic item = vector[index]!;
                result[index] = (T)(item + value);
            }
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Тип {typeof(T).Name} не поддерживает сложение с целым числом.", exception);
        }

        return new CVector<T>(result);
    }

    public static bool operator ==(CVector<T>? left, CVector<T>? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null || left.Count != right.Count)
        {
            return false;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        for (int index = 0; index < left.Count; index++)
        {
            if (!comparer.Equals(left[index], right[index]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator !=(CVector<T>? left, CVector<T>? right)
    {
        return !(left == right);
    }

    public bool Equals(CVector<T>? other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is CVector<T> vector && this == vector;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (T item in _items)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in _items)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _items.AsEnumerable())}]";
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException($"Индекс {index} выходит за границы вектора размером {Count}.");
        }
    }
}
