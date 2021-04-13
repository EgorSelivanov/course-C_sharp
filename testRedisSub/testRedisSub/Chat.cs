using StackExchange.Redis;
using System;

namespace testRedisSub
{
    public class Chat
    {
        private ConfigurationOptions options;
        private ConnectionMultiplexer redis;
        private IDatabase db;
        private string comand;
        public string User { get; private set; }

        /// <summary>
        /// Инициализация чата: установка хоста, получение связи с бд Redis
        /// </summary>
        public Chat()
        {
            options = new ConfigurationOptions();
            options.EndPoints.Add("localhost", 6379);
            options.ClientName = "ChatClient";

            redis = ConnectionMultiplexer.Connect(options);
            db = redis.GetDatabase(2);
            comand = "";
        }

        /// <summary>
        /// Запуск чата: работа с меню и обработка команд
        /// </summary>
        public void StartChat()
        {
            Console.WriteLine("Введите своё имя пользователя:");
            User = Console.ReadLine();
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
                var action = GetActionByComand(comand);
                action?.Invoke();
            }
        }

        /// <summary>
        /// Ввод сообщения пользователем и Push в бд Redis
        /// </summary>
        public void InputMethod()
        {
            Console.WriteLine("Введите сообщение:");
            var message = Console.ReadLine();
            db.ListRightPush(User, message);
            Console.WriteLine("Сообщение добавлено в список!");
        }

        /// <summary>
        /// Запрос всех сообщений из бд Redis и вывод на экран
        /// </summary>
        public void GetMesMethod()
        {
            if (db.KeyExists(User))
            {
                for (int i = 0; i < db.ListLength(User); i++)
                    Console.WriteLine(db.ListGetByIndex(User, i));
            }
            else
                Console.WriteLine("Для данного пользователя нет сообщений.");
        }

        /// <summary>
        /// Очистить весь список сообщений в бд Redis
        /// </summary>
        public void DelMesMethod()
        {
            if (db.KeyExists(User))
            {
                var len = db.ListLength(User);
                for (int i = 0; i < len; i++)
                    db.ListRightPop(User);
                Console.WriteLine("Список сообщений успешно очищен!");
            }
            else
                Console.WriteLine("Для данного пользователя нет сообщений.");
        }

        /// <summary>
        /// Смена пользователя
        /// </summary>
        public void ChangeUserMethod()
        {
            Console.WriteLine("Введите новое имя пользователя:");
            User = Console.ReadLine();
            Console.WriteLine($"Новое имя пользователя: {User}.");
        }

        /// <summary>
        /// Действие при завершении работы чата
        /// </summary>
        public void ExitMethod()
        {
            Console.WriteLine($"Работа чата завершена, спасибо за работу, {User}");
        }

        /// <summary>
        /// Действие при некорректном вводе данных(команды)
        /// </summary>
        public void InvalidInputMethod()
        {
            Console.WriteLine("Неверный ввод! Повторите попытку!");
        }

        /// <summary>
        /// Возвращает ссылку на метод в зависимости от команды пользователя
        /// </summary>
        public Action GetActionByComand(string comand)
        {
            switch (comand)
            {
                case "/input": return InputMethod;
                case "/getMes": return GetMesMethod;
                case "/delMes": return DelMesMethod;
                case "/changeUser": return ChangeUserMethod;
                case "/exit": return ExitMethod;
                default: return InvalidInputMethod;
            }
        }

    }
}

