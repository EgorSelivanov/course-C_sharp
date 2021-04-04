using System;

namespace ClassLibrary1
{
    class Program
    {
        static void Main(string[] args)
        {
            SubsidyCalculation subCalculation = new SubsidyCalculation();

            //правильные значения
            Tariff tariff1 = new Tariff()
            {
                ServiceId = 10,
                HouseId = 10,
                PeriodBegin = new DateTime(2021, 3, 16),
                PeriodEnd = new DateTime(2021, 4, 4),
                Value = 100
            };

            Volume volume1 = new Volume()
            { 
                ServiceId = 10,
                HouseId = 10,
                Month = new DateTime(2021, 3, 20),
                Value = 5
            };

            //не совпадает ServiceId
            Tariff tariff2 = new Tariff()
            {
                ServiceId = 1,
                HouseId = 10,
                PeriodBegin = new DateTime(2021, 3, 16),
                PeriodEnd = new DateTime(2021, 4, 4),
                Value = 100
            };

            Volume volume2 = new Volume()
            {
                ServiceId = 10,
                HouseId = 10,
                Month = new DateTime(2021, 3, 20),
                Value = 5
            };

            //Период истек
            Tariff tariff3 = new Tariff()
            {
                ServiceId = 20,
                HouseId = 20,
                PeriodBegin = new DateTime(2021, 2, 16),
                PeriodEnd = new DateTime(2021, 4, 10),
                Value = 100
            };

            Volume volume3 = new Volume()
            {
                ServiceId = 20,
                HouseId = 20,
                Month = new DateTime(2021, 4, 20),
                Value = 5
            };

            //Некорректные значения
            Tariff tariff4 = new Tariff()
            {
                ServiceId = 20,
                HouseId = 20,
                PeriodBegin = new DateTime(2021, 2, 16),
                PeriodEnd = new DateTime(2021, 4, 10),
                Value = 0
            };

            Volume volume4 = new Volume()
            {
                ServiceId = 20,
                HouseId = 20,
                Month = new DateTime(2021, 4, 5),
                Value = -5
            };

            Charge charge1 = subCalculation.CalculateSubsidy(volume1, tariff1);
            Console.WriteLine($"ServiceId1 = {charge1.ServiceId}");
            Console.WriteLine($"HouseId = {charge1.HouseId}");
            Console.WriteLine($"Month = {charge1.Month}");
            Console.WriteLine($"Value = {charge1.Value}");
            Console.WriteLine("-----------------------");

            //Charge charge2 = subCalculation.CalculateSubsidy(volume2, tariff2);
            //Console.WriteLine($"ServiceId1 = {charge2.ServiceId}");
            //Console.WriteLine($"HouseId = {charge2.HouseId}");
            //Console.WriteLine($"Month = {charge2.Month}");
            //Console.WriteLine($"Value = {charge2.Value}");
            //Console.WriteLine("-----------------------");

            //Charge charge3 = subCalculation.CalculateSubsidy(volume3, tariff3);
            //Console.WriteLine($"ServiceId1 = {charge3.ServiceId}");
            //Console.WriteLine($"HouseId = {charge3.HouseId}");
            //Console.WriteLine($"Month = {charge3.Month}");
            //Console.WriteLine($"Value = {charge3.Value}");
            //Console.WriteLine("-----------------------");

            //Charge charge4 = subCalculation.CalculateSubsidy(volume4, tariff4);
            //Console.WriteLine($"ServiceId1 = {charge4.ServiceId}");
            //Console.WriteLine($"HouseId = {charge4.HouseId}");
            //Console.WriteLine($"Month = {charge4.Month}");
            //Console.WriteLine($"Value = {charge4.Value}");
            //Console.WriteLine("-----------------------");

            Console.ReadKey();
        }
    }
}
