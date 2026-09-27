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

// Представляет одномерный вектор целых чисел.
public class CVector : IEquatable<CVector>
{
    // Массив является внутренним хранилищем элементов вектора.
    private readonly int[] _items;

    // Создает пустой вектор.
    public CVector()
    {
        _items = [];
    }

    // Создает вектор указанного размера и заполняет его нулями.
    public CVector(int size)
    {
        if (size < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Размер вектора не может быть отрицательным.");
        }

        _items = new int[size];
    }

    // Создает вектор из готовой последовательности чисел.
    public CVector(IEnumerable<int> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    // Создает независимую копию другого вектора.
    public CVector(CVector other)
    {
        ArgumentNullException.ThrowIfNull(other);
        _items = (int[])other._items.Clone();
    }

    // Возвращает количество элементов.
    public int Count => _items.Length;

    // Индексатор предоставляет доступ к элементу как к элементу массива.
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

    // Вычитает из каждого элемента левого вектора соответствующий элемент правого.
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

    // Прибавляет целое число к каждому элементу вектора.
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

    // Поддерживает запись, в которой число находится слева от вектора.
    public static CVector operator +(int value, CVector vector)
    {
        return vector + value;
    }

    // Проверяет поэлементное равенство двух векторов.
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

    // Возвращает противоположный результат проверки на равенство.
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

    // Финализатор добавлен для демонстрации требования задания.
    // Вектор не содержит неуправляемых ресурсов, поэтому практической необходимости в нем нет.
    ~CVector()
    {
        Console.WriteLine($"Финализирован вектор размером {Count}.");
    }
}
