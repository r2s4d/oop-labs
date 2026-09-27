/*
Лабораторная работа № 7
Тема: Потоки и обработка исключений
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Продолжить работу с обобщенным вектором и добавить собственные
исключения для размера, индекса и несовместимых операций.
*/

using System.Collections;

namespace Lab07;

// Обобщенный вектор с проверкой основных ошибочных ситуаций.
public sealed class CVector<T> : IEnumerable<T>
{
    private readonly T[] _items;

    public CVector(int size)
    {
        if (size < 0)
        {
            throw new InvalidVectorSizeException(size);
        }

        _items = new T[size];
    }

    public CVector(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToArray();
    }

    public int Count => _items.Length;

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

    // Вычитает элементы двух векторов и проверяет равенство размеров.
    public static CVector<T> operator -(CVector<T> left, CVector<T> right)
    {
        if (left.Count != right.Count)
        {
            throw new VectorSizeMismatchException(left.Count, right.Count);
        }

        T[] result = new T[left.Count];
        for (int index = 0; index < result.Length; index++)
        {
            dynamic leftItem = left[index]!;
            dynamic rightItem = right[index]!;
            result[index] = (T)(leftItem - rightItem);
        }

        return new CVector<T>(result);
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
}
