using System.Text;

namespace LightWeightLogger;

public class Logger {
    private readonly string _fileName;

    public Logger(string logname, string path)
    {
        if (!path.EndsWith("\\"))
            path += "\\";

        _fileName = !string.IsNullOrEmpty(path) ? $"{path}{logname}_log_{DateTime.Now:ddMMyyyy}.txt" : "";
    }

    public void LogInformation(object message)
    {
        Log(message);
    }

    public void Log(object message)
    {
        var logEntry = $"{DateTime.Now} - INFO: {message}";
        LogToFile(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogError(string message)
    {
        var logEntry = $"{DateTime.Now} - ERROR: {message}";
        LogToFile(logEntry);
        Console.WriteLine(logEntry);
    }
    
    public void LogError(string message, Exception ex)
    {
        var logEntry = $"{DateTime.Now} - ERROR: {message}\r\n{ex}";
        LogToFile(logEntry);
        Console.WriteLine(logEntry);
    }

    void LogToFile(string message)
    {
        if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(_fileName))
            return;

        var messageSpan = new ReadOnlySpan<byte>(Encoding.ASCII.GetBytes($"{message}\r\n"));
        var fileMode = File.Exists(_fileName) ? FileMode.Append : FileMode.Create;

        using var fs = new FileStream(_fileName, fileMode, FileAccess.Write, FileShare.ReadWrite);
        fs.Write(messageSpan);
    }
}