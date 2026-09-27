/*
Лабораторная работа № 2
Тема: Наследование
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать объекты транспортного средства, автомобиля и поезда,
затем продемонстрировать наследование и переопределение методов.
*/

using Lab02;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 2. Наследование.");
Console.WriteLine();

// Переменная базового типа может хранить объекты всех производных классов.
List<TransportVehicle> vehicles =
[
    new TransportVehicle(45, "Бензин", 2015),
    new Car("Lada Vesta", "А123БВ30", 80, "Бензин", 2021),
    new Train("109Ж", 12, 54, 95, "Электричество", 2019)
];

// Во время выполнения вызывается версия метода реального типа объекта.
foreach (TransportVehicle vehicle in vehicles)
{
    Console.WriteLine(vehicle.GetDescription());
}

// Для поезда дополнительно используем его собственный метод.
Train train = (Train)vehicles[2];
Console.WriteLine();
Console.WriteLine($"Количество мест в поезде: {train.GetTotalSeats()}");
