public interface ILogger {
    void Log(object message);
    void LogInformation(object message);
    void LogError(string message);
    void LogError(string message, Exception ex);
}