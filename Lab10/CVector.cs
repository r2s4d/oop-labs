/*
Лабораторная работа № 10
Тема: Обобщения и перегрузка операторов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать обобщенный CVector с индексатором, конструкторами, финализатором,
операциями разности, сложения с числом, равенства и неравенства.
Использовать ref, out, is, foreach и обработку исключений.
*/

using System.Collections;
using System.Numerics;

namespace Lab10;

// Обобщенный вектор числовых элементов.
// Ограничение INumber позволяет выполнять арифметику без dynamic.
public class CVector<T> : IEnumerable<T>, IEquatable<CVector<T>>
    where T : INumber<T>
{
    private readonly T[] _items;

    // Создает пустой вектор.
    public CVector()
    {
        _items = [];
    }

    // Создает вектор указанного размера и заполняет его нулями типа T.
    public CVector(int size)
    {
        if (size < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Размер вектора не может быть отрицательным.");
        }

        _items = new T[size];
        Array.Fill(_items, T.Zero);
    }

    // Создает вектор из последовательности элементов.
    public CVector(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    // Создает независимую копию другого вектора.
    public CVector(CVector<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);
        _items = (T[])other._items.Clone();
    }

    public int Count => _items.Length;

    // Индексатор предоставляет проверяемый доступ к элементам.
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

    // Вычисляет поэлементную разность двух векторов.
    public static CVector<T> operator -(CVector<T> left, CVector<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        EnsureEqualSize(left, right);

        T[] result = new T[left.Count];
        for (int index = 0; index < result.Length; index++)
        {
            result[index] = left[index] - right[index];
        }

        return new CVector<T>(result);
    }

    // Прибавляет скаляр к каждому элементу вектора.
    public static CVector<T> operator +(CVector<T> vector, T value)
    {
        ArgumentNullException.ThrowIfNull(vector);

        T[] result = new T[vector.Count];
        for (int index = 0; index < result.Length; index++)
        {
            result[index] = vector[index] + value;
        }

        return new CVector<T>(result);
    }

    // Поддерживает сложение, когда скаляр записан слева.
    public static CVector<T> operator +(T value, CVector<T> vector)
    {
        return vector + value;
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

        for (int index = 0; index < left.Count; index++)
        {
            if (left[index] != right[index])
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

    // Заменяет элемент. Отрицательный индекс отсчитывается от конца.
    // Нормализованный индекс возвращается через ref, старое значение через out.
    public void ReplaceAt(ref int index, T newValue, out T previousValue)
    {
        if (index < 0)
        {
            index = Count + index;
        }

        ValidateIndex(index);
        previousValue = _items[index];
        _items[index] = newValue;
    }

    // Проверяет объект оператором is и формирует описание через out.
    public static bool TryDescribe(object? value, out string description)
    {
        if (value is CVector<T> vector)
        {
            description = $"Объект является CVector<{typeof(T).Name}> размером {vector.Count}.";
            return true;
        }

        description = $"Объект не является CVector<{typeof(T).Name}>.";
        return false;
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
            throw new VectorIndexException(index, Count);
        }
    }

    private static void EnsureEqualSize(CVector<T> left, CVector<T> right)
    {
        if (left.Count != right.Count)
        {
            throw new VectorSizeMismatchException(left.Count, right.Count);
        }
    }

    // Финализатор включен по условию лабораторной работы.
    // Управляемый массив не требует ручного освобождения памяти.
    ~CVector()
    {
        Console.WriteLine($"Финализирован CVector<{typeof(T).Name}> размером {Count}.");
    }
}
