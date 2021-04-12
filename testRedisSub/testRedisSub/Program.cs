using StackExchange.Redis;
using System;

namespace testRedisSub
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ConfigurationOptions();
            options.EndPoints.Add("localhost", 6379);
            options.ClientName = "TestOutputClient";

            var redis = ConnectionMultiplexer.Connect(options);
            var db = redis.GetDatabase(2);
            var comand = "";

            Console.WriteLine("Введите своё имя пользователя:");
            var user = Console.ReadLine();

            while (comand != "/exit")
            { 
                Console.WriteLine("----------------------");
                Console.WriteLine("/input - записать новое сообщение.");
                Console.WriteLine("/getMes - выгрузить список сообщений.");
                Console.WriteLine("/delMes - удалить список сообщений.");
                Console.WriteLine("/changeUser - сменить пользователя.");
                Console.WriteLine("/exit - завершить работу программы.");
                Console.WriteLine("Введите комманду:");

                comand = Console.ReadLine();
                if (comand == "/input")
                {
                    Console.WriteLine("Введите сообщение:");
                    var message = Console.ReadLine();
                    db.ListRightPush(user, message);
                    Console.WriteLine("Сообщение добавлено в список!");
                }
                else if (comand == "/getMes")
                {
                    if (db.KeyExists(user))
                    {
                        for (int i = 0; i < db.ListLength(user); i++)
                            Console.WriteLine(db.ListGetByIndex(user, i));
                    }
                    else
                        Console.WriteLine("Для данного пользователя нет сообщений.");
                }
                else if (comand == "/delMes")
                {
                    if (db.KeyExists(user))
                    {
                        var len = db.ListLength(user);
                        for (int i = 0; i < len; i++)
                            db.ListRightPop(user);
                        Console.WriteLine("Список сообщений успешно очищен!");
                    }
                    else
                        Console.WriteLine("Для данного пользователя нет сообщений.");
                }
                else if (comand == "/changeUser")
                {
                    Console.WriteLine("Введите новое имя пользователя:");
                    user = Console.ReadLine();
                    Console.WriteLine($"Новое имя пользователя: {user}.");
                }
                else if (comand == "/exit")
                {
                    Console.WriteLine($"Работа чата завершена, спасибо за работу, {user}");
                }
                else
                    Console.WriteLine("Неверный ввод! Повторите попытку!");
            }
        }
    }
}
