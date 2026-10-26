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
// IEquatable<CVector> это интерфейс: класс обещает метод Equals для сравнения по значению.
public class CVector : IEquatable<CVector>
{
    // Массив является внутренним хранилищем элементов вектора.
    // Подчеркивание в имени _items только обозначает закрытое поле, readonly значит "задается один раз в конструкторе".
    private readonly int[] _items;

    // Создает пустой вектор.
    // Конструктор без параметров.
    public CVector()
    {
        _items = [];
    }

    // Создает вектор указанного размера и заполняет его нулями.
    // Конструктор с параметром: размер.
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
    // Конструктор копирования: принимает объект своего же класса.
    public CVector(CVector other)
    {
        ArgumentNullException.ThrowIfNull(other);
        _items = (int[])other._items.Clone();
    }

    // Возвращает количество элементов.
    // Стрелка => это короткая запись свойства только для чтения.
    public int Count => _items.Length;

    // Индексатор предоставляет доступ к элементу как к элементу массива.
    // Это перегрузка операции []: запись this[int index], работает v[2] и v[2] = 100.
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
    // Перегрузка операции: метод static с ключевым словом operator и знаком операции.
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
    // != из задания, C# требует перегружать его в паре с ==.
    public static bool operator !=(CVector? left, CVector? right)
    {
        return !(left == right);
    }

    public bool Equals(CVector? other)
    {
        return this == other;
    }

    // override методов базового класса Object, чтобы == и Equals работали одинаково.
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

    // Финализатор (~) это аналог деструктора из задания, вызывается сборщиком мусора.
    // Финализатор добавлен для демонстрации требования задания.
    // Вектор не содержит неуправляемых ресурсов, поэтому практической необходимости в нем нет.
    ~CVector()
    {
        Console.WriteLine($"Финализирован вектор размером {Count}.");
    }
}
