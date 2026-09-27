/*
Лабораторная работа № 5
Тема: Перегрузка операций
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать класс CVector. Перегрузить разность двух векторов, сложение
элементов вектора с целым числом, проверку на неравенство и доступ
к элементу по индексу.
*/

namespace Lab05;

/// <summary>
/// Представляет одномерный вектор целых чисел.
/// </summary>
public class CVector : IEquatable<CVector>
{
    // Массив является внутренним хранилищем элементов вектора.
    private readonly int[] _items;

    /// <summary>
    /// Создает пустой вектор.
    /// </summary>
    public CVector()
    {
        _items = [];
    }

    /// <summary>
    /// Создает вектор указанного размера и заполняет его нулями.
    /// </summary>
    public CVector(int size)
    {
        if (size < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Размер вектора не может быть отрицательным.");
        }

        _items = new int[size];
    }

    /// <summary>
    /// Создает вектор из готовой последовательности чисел.
    /// </summary>
    public CVector(IEnumerable<int> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    /// <summary>
    /// Создает независимую копию другого вектора.
    /// </summary>
    public CVector(CVector other)
    {
        ArgumentNullException.ThrowIfNull(other);
        _items = (int[])other._items.Clone();
    }

    /// <summary>
    /// Возвращает количество элементов.
    /// </summary>
    public int Count => _items.Length;

    /// <summary>
    /// Индексатор предоставляет доступ к элементу как к элементу массива.
    /// </summary>
    public int this[int index]
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
    /// Вычитает из каждого элемента левого вектора соответствующий элемент правого.
    /// </summary>
    public static CVector operator -(CVector left, CVector right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        EnsureEqualSize(left, right);

        int[] result = new int[left.Count];
        for (int index = 0; index < result.Length; index++)
        {
            result[index] = left[index] - right[index];
        }

        return new CVector(result);
    }

    /// <summary>
    /// Прибавляет целое число к каждому элементу вектора.
    /// </summary>
    public static CVector operator +(CVector vector, int value)
    {
        ArgumentNullException.ThrowIfNull(vector);

        int[] result = new int[vector.Count];
        for (int index = 0; index < result.Length; index++)
        {
            result[index] = vector[index] + value;
        }

        return new CVector(result);
    }

    /// <summary>
    /// Поддерживает запись, в которой число находится слева от вектора.
    /// </summary>
    public static CVector operator +(int value, CVector vector)
    {
        return vector + value;
    }

    /// <summary>
    /// Проверяет поэлементное равенство двух векторов.
    /// </summary>
    public static bool operator ==(CVector? left, CVector? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null || left.Count != right.Count)
        {
            return false;
        }

        for (int index = 0; index < left.Count; index++)
        {
            if (left[index] != right[index])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Возвращает противоположный результат проверки на равенство.
    /// </summary>
    public static bool operator !=(CVector? left, CVector? right)
    {
        return !(left == right);
    }

    public bool Equals(CVector? other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is CVector vector && this == vector;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (int item in _items)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _items)}]";
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException($"Индекс {index} выходит за границы вектора размером {Count}.");
        }
    }

    private static void EnsureEqualSize(CVector left, CVector right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("Для вычитания векторы должны иметь одинаковый размер.");
        }
    }

    /// <summary>
    /// Финализатор добавлен для демонстрации требования задания.
    /// Вектор не содержит неуправляемых ресурсов, поэтому практической необходимости в нем нет.
    /// </summary>
    ~CVector()
    {
        Console.WriteLine($"Финализирован вектор размером {Count}.");
    }
}
