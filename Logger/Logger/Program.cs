using System;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Logger starts");
            Logger myLog = new Logger();
            myLog.Fatal("This is a Fatal message!");
            myLog.Fatal("This is a Fatal error message", new DivideByZeroException("Divide by zero!!!"));

            myLog.Error("This is Error message!");
            myLog.Error(new TimeoutException("Timeout exception is here!"));
            myLog.Error("This is Error exception message", new ArgumentException("Argument exception!"));
            myLog.ErrorUnique("This is unique error", new FieldAccessException("Field access exception!"));
            myLog.ErrorUnique("This is not unique error", new ArgumentException("Argument exception!"));

            myLog.Warning("This is Warning message!");
            myLog.Warning("This is Warning exception message!", new ArgumentNullException("Argument == null"));
            myLog.WarningUnique("This is unique warning");
            myLog.WarningUnique("This is unique warning");

            myLog.Info("This is Info message");
            myLog.Info("This is Info exception message", new FormatException("Format exc"));
            myLog.Info("This is Info with argc", 10, 20, "Hello", new int[] { 0, 1, 2 });

            myLog.Debug("This is Debug message");
            myLog.Debug("This is Debug exception message", new InvalidOperationException("Invalid operation"));
            myLog.DebugFormat("This is Debug format message", 25, 90.3, "world", new char[] { 'a', 'b', 'c' });

            myLog.SystemInfo("This is System info");
            myLog.SystemInfo("This is System Info message with properties", new System.Collections.Generic.Dictionary<object, object>(2) { { 1, "Russia" }, { 2, 23.2 } });
            Console.WriteLine("Logger stopped working");
            Console.ReadKey();
        }
    }
}
