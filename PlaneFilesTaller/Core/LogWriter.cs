namespace Core;

public class LogWriter : IDisposable
{
    private readonly StreamWriter _writer;

    public LogWriter(string path)
    {
        _writer = new StreamWriter(path, append: true);
    }

    public void WriteLog(string level, string user, string operation)
    {
        var entry = $"{DateTime.Now:s} | {level,-5} | User: {user,-15} | {operation}";
        _writer.WriteLine(entry);
        _writer.Flush();
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}