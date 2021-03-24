using System;
using System.Collections.Generic;
using System.IO;


namespace Logger
{
    public class Logger : ILog
    {
        private readonly string path = Path.Combine("logs", DateTime.Today.ToShortDateString());
        private List<string> UniqueErrors { get; set;} = new List<string>();
        private List<string> UniqueWarnings { get; set; } = new List<string>();

        public void Fatal(string message)
        {
            IsCreateFolder();
            IsCreateFile("errors.txt");
            WriteMessage("errors.txt", "FATAL", message);
        }

        public void Fatal(string message, Exception e)
        {
            IsCreateFolder();
            IsCreateFile("errors.txt");
            WriteErrorMessage("errors.txt", "FATAL", message, e);
        }

        public void Error(string message)
        {
            IsCreateFolder();
            IsCreateFile("errors.txt");
            WriteMessage("errors.txt", "ERROR", message);
        }

        public void Error(string message, Exception e)
        {
            IsCreateFolder();
            IsCreateFile("errors.txt");
            WriteErrorMessage("errors.txt", "ERROR", message, e);
            if (!UniqueErrors.Contains(e.Message))
                UniqueErrors.Add(e.Message);
        }

        public void Error(Exception ex)
        {
            IsCreateFolder();
            IsCreateFile("errors.txt");
            string pathFile = Path.Combine(path, "errors.txt");
            using (StreamWriter sw = new StreamWriter(pathFile, true))
            {
                sw.WriteLine($"{DateTime.Now} (ERRORS): Тип: {ex.GetType().Name}, Исключение: {ex.Message}, Метод: {ex.TargetSite}");
            }
        }

        public void ErrorUnique(string message, Exception e)
        {
            if (!UniqueErrors.Contains(e.Message))
                Error(message, e);
        }

        public void Warning(string message)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteMessage("info.txt", "WARNING", message);
            if (!UniqueWarnings.Contains(message))
                UniqueWarnings.Add(message);
        }

        public void Warning(string message, Exception e)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteErrorMessage("info.txt", "WARNING", message, e);
        }

        public void WarningUnique(string message)
        {
            if (!UniqueWarnings.Contains(message))
                Warning(message);
        }

        public void Info(string message)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteMessage("info.txt", "INFO", message);
        }

        public void Info(string message, Exception e)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteErrorMessage("info.txt", "INFO", message, e);
        }

        public void Info(string message, params object[] args)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteFormatMessage("info.txt", "INFO", message, args);
        }

        public void Debug(string message)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteMessage("info.txt", "DEBUG", message);
        }

        public void Debug(string message, Exception e)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteErrorMessage("info.txt", "DEBUG", message, e);
        }

        public void DebugFormat(string message, params object[] args)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            WriteFormatMessage("info.txt", "DEBUG", message, args);
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            IsCreateFolder();
            IsCreateFile("info.txt");
            string pathFile = Path.Combine(path, "info.txt");
            using (StreamWriter sw = new StreamWriter(pathFile, true))
            {
                sw.Write($"{DateTime.Now} ({"SYSTEM INFO"}): {message}; ");
                if (properties != null)
                {
                    foreach(KeyValuePair<object, object> valuePair in properties)
                    {
                        sw.Write($"Key = {valuePair.Key}, Value = {valuePair.Value} | ");
                    }
                }
                sw.WriteLine("");
            }
        }

        private void IsCreateFolder()
        {
            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);
        }

        private void IsCreateFile(string file)
        {
            string pathFile = Path.Combine(path, file);
            if (!File.Exists(pathFile))
                using (File.Create(pathFile)) ;
        }

        private void WriteMessage(string file, string operation, string message)
        {
            string pathFile = Path.Combine(path, file);
            using (StreamWriter sw = new StreamWriter(pathFile, true))
            {
                sw.WriteLine($"{DateTime.Now} ({operation}): {message}");
            }
        }

        private void WriteErrorMessage(string file, string operation, string message, Exception e)
        {
            string pathFile = Path.Combine(path, file);
            using (StreamWriter sw = new StreamWriter(pathFile, true))
            {
                sw.WriteLine($"{DateTime.Now} ({operation}): {message}; Тип: {e.GetType().Name}, Исключение: {e.Message}");
            }
        }

        private void WriteFormatMessage(string file, string operation, string message, params object[] args)
        {
            string pathFile = Path.Combine(path, file);
            using (StreamWriter sw = new StreamWriter(pathFile, true))
            {
                sw.Write($"{DateTime.Now} ({operation}): {message}: ");
                foreach(var value in args)
                {
                    sw.Write($"{value} | ");
                }
                sw.WriteLine("");
            }
        }
    }
}
