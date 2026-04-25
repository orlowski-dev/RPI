public interface ILogger
{
    public void Write(LogLevel level, string service, string message);
}
