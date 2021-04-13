using StackExchange.Redis;
using System;

namespace testRedisSub
{
    class Program
    {
        static void Main(string[] args)
        {
            var chat1 = new Chat();
            chat1.StartChat();
        }
    }
}
