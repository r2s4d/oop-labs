/*
Лабораторная работа № 3
Тема: Абстрактные классы
Вариант: 9
Выполнил: Рассадин Егор

Задание:
Создать объекты разных производных классов, добавить их в статический
связанный список и просмотреть список через метод базового класса.
*/

using Lab03;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторная работа № 3. Абстрактные классы.");
Console.WriteLine();

Car firstCar = new("Lada Vesta", "А123БВ30", 80, "Бензин", 2021);
Train train = new("109Ж", 12, 54, 95, "Электричество", 2019);
Car secondCar = new("УАЗ Патриот", "В456ГД30", 70, "Бензин", 2020);

// Каждый объект добавляется через статический метод абстрактного класса.
TransportVehicle.AddToRegistry(firstCar);
TransportVehicle.AddToRegistry(train);
TransportVehicle.AddToRegistry(secondCar);

// Статический метод вызывается через класс, а не через отдельный объект.
TransportVehicle.ShowRegistry();
