/*
Лабораторная работа № 4
Тема: Динамическая идентификация типов
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Определить количество указанных транспортных средств в автопарке
и количество мест во всех вагонах поездов.
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
Console.WriteLine($"Количество автомобилей: {analyzer.CountByType<Car>(vehicles)}");
Console.WriteLine($"Количество поездов: {analyzer.CountByType<Train>(vehicles)}");
Console.WriteLine($"Количество лодок: {analyzer.CountByType<Boat>(vehicles)}");

// Запрос по промежуточным абстрактным классам: учитываются все их наследники.
Console.WriteLine($"Наземного транспорта: {analyzer.CountByType<GroundVehicle>(vehicles)}");
Console.WriteLine($"Водного транспорта: {analyzer.CountByType<WaterVehicle>(vehicles)}");
Console.WriteLine($"Количество мест во всех поездах: {analyzer.CountAllTrainSeats(vehicles)}");
