/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание (полный текст):
Использовать проект, созданный в лабораторной работе № 3. Определить
методы-итераторы в неабстрактных классах на основе следующих запросов.
Вариант 9: количество указанных транспортных средств в автопарке (на
автостоянке), количество мест во всех вагонах поездов.

Что сделано (по файлам):
- TransportVehicle.cs: абстрактная основа и связанный список из лабы 3.
  Новый метод EnumerateRegistry, итератор (yield return) по списку.
- GroundVehicle.cs, WaterVehicle.cs: абстрактные промежуточные классы.
- Car.cs, Train.cs, Boat.cs: конкретные классы. Train умеет считать свои места.
- VehicleParkAnalyzer.cs: запросы. SelectByType<T> выбирает объекты нужного
  типа через оператор is, CountByType<T> считает их, CountAllTrainSeats
  суммирует места во всех вагонах всех поездов.
- Program.cs (этот файл): заполняет список и выводит ответы на запросы.

Вывод программы:
Содержимое автопарка: лодка, поезд 005Г, УАЗ Патриот, поезд 109Ж, Lada Vesta
Количество автомобилей: 2
Количество поездов: 2
Количество лодок: 1
Наземного транспорта: 4
Водного транспорта: 1
Количество мест во всех поездах: 1188
*/

using Lab04;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 4. Динамическая идентификация типов.");
Console.WriteLine();

// Заполняем общий связанный список объектами разных производных классов.
TransportVehicle.AddToRegistry(new Car("Lada Vesta", "А123БВ30", 80, "Бензин", 2021));
TransportVehicle.AddToRegistry(new Train("109Ж", 12, 54, 95, "Электричество", 2019));
TransportVehicle.AddToRegistry(new Car("УАЗ Патриот", "В456ГД30", 70, "Бензин", 2020));
TransportVehicle.AddToRegistry(new Train("005Г", 15, 36, 110, "Электричество", 2022));
TransportVehicle.AddToRegistry(new Boat("Волга-1", 6, 600, 25, "Бензин", 2018));

IEnumerable<TransportVehicle> vehicles = TransportVehicle.EnumerateRegistry();
VehicleParkAnalyzer analyzer = new();

Console.WriteLine("Содержимое автопарка:");
foreach (TransportVehicle vehicle in vehicles)
{
    Console.WriteLine($"- {vehicle.GetDescription()}");
}

Console.WriteLine();
// Тип в угловых скобках <Car> говорит, какие объекты считать (запрос 1 из задания).
Console.WriteLine($"Количество автомобилей: {analyzer.CountByType<Car>(vehicles)}");
Console.WriteLine($"Количество поездов: {analyzer.CountByType<Train>(vehicles)}");
Console.WriteLine($"Количество лодок: {analyzer.CountByType<Boat>(vehicles)}");

// Запрос по промежуточным абстрактным классам: учитываются все их наследники.
Console.WriteLine($"Наземного транспорта: {analyzer.CountByType<GroundVehicle>(vehicles)}");
Console.WriteLine($"Водного транспорта: {analyzer.CountByType<WaterVehicle>(vehicles)}");
// Запрос 2 из задания: места во всех вагонах всех поездов.
Console.WriteLine($"Количество мест во всех поездах: {analyzer.CountAllTrainSeats(vehicles)}");
