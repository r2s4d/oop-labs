/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Реализовать методы-итераторы для подсчета транспорта указанного типа
и общего количества мест во всех вагонах поездов.
*/

namespace Lab04;

/// <summary>
/// Выполняет запросы к набору транспорта в автопарке.
/// </summary>
public sealed class VehicleParkAnalyzer
{
    /// <summary>
    /// Возвращает только те объекты, фактический тип которых совместим с T.
    /// </summary>
    public IEnumerable<T> SelectByType<T>(IEnumerable<TransportVehicle> vehicles)
        where T : TransportVehicle
    {
        foreach (TransportVehicle vehicle in vehicles)
        {
            // Оператор is одновременно проверяет тип и безопасно создает переменную.
            if (vehicle is T requiredVehicle)
            {
                yield return requiredVehicle;
            }
        }
    }

    /// <summary>
    /// Подсчитывает транспорт заданного типа.
    /// </summary>
    public int CountByType<T>(IEnumerable<TransportVehicle> vehicles)
        where T : TransportVehicle
    {
        int count = 0;

        foreach (T unused in SelectByType<T>(vehicles))
        {
            count++;
        }

        return count;
    }

    /// <summary>
    /// Подсчитывает места во всех вагонах всех поездов.
    /// </summary>
    public int CountAllTrainSeats(IEnumerable<TransportVehicle> vehicles)
    {
        int totalSeats = 0;

        foreach (Train train in SelectByType<Train>(vehicles))
        {
            totalSeats += train.GetTotalSeats();
        }

        return totalSeats;
    }
}
