/*
Лабораторная работа № 9
Тема: Интерфейсы и наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать абстрактный класс теста с конструкторами, свойствами,
статическим конструктором, счетчиком объектов и абстрактным методом.
*/

namespace Lab09;

// Абстрактная основа для всех форм проверки знаний.
public abstract class Test
{
    // Поля являются общими для всего класса, а не для отдельного объекта.
    private static int _createdCount;
    private static int _nextObjectNumber;

    private string _title = string.Empty;
    private int _maximumScore;
    private int _score;

    // Статический конструктор выполняется один раз перед первым использованием класса.
    static Test()
    {
        _createdCount = 0;
        _nextObjectNumber = 1;
        Console.WriteLine("Выполнен статический конструктор класса Test.");
    }

    // Создает тест с начальными значениями.
    protected Test() : this("Без названия", DateTime.Today, 100, 0)
    {
    }

    // Создает тест с заданными параметрами.
    protected Test(string title, DateTime date, int maximumScore, int score)
    {
        // Сначала задается максимум, так как проверка Score зависит от него.
        Title = title;
        Date = date;
        MaximumScore = maximumScore;
        Score = score;

        ObjectNumber = _nextObjectNumber++;
        _createdCount++;
    }

    // Уникальный номер объекта в пределах текущего запуска программы.
    public int ObjectNumber { get; }

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AssessmentException("Название проверки не должно быть пустым.");
            }

            _title = value;
        }
    }

    public DateTime Date { get; set; }

    public int MaximumScore
    {
        get => _maximumScore;
        set
        {
            if (value <= 0)
            {
                throw new AssessmentException("Максимальный балл должен быть больше нуля.");
            }

            _maximumScore = value;
        }
    }

    public int Score
    {
        get => _score;
        set
        {
            if (value < 0 || value > MaximumScore)
            {
                throw new AssessmentException($"Балл должен находиться в диапазоне от 0 до {MaximumScore}.");
            }

            _score = value;
        }
    }

    // Производные классы по-разному определяют успешный результат.
    public abstract bool IsPassed { get; }

    // Производные классы формируют понятное текстовое описание результата.
    public abstract string GetResultDescription();

    // Возвращает общую часть описания объекта.
    public virtual string GetInfo()
    {
        return $"№ {ObjectNumber}, {Title}, дата {Date:dd.MM.yyyy}, балл {Score}/{MaximumScore}";
    }

    // Обновляет балл и через out сообщает, пройдена ли проверка.
    // Параметр ref показывает передачу переменной по ссылке.
    public void UpdateScore(ref int newScore, out bool passed)
    {
        Score = newScore;

        // Возвращаем фактически сохраненное значение через тот же ref-параметр.
        newScore = Score;
        passed = IsPassed;
    }

    // Выводит количество созданных объектов всей иерархии.
    public static void PrintCreatedCount()
    {
        Console.WriteLine($"Создано объектов контроля: {_createdCount}.");
    }
}
