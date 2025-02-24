namespace netcore_Assignment3.Services
{
    public interface IFileService
    {
        Task SaveToFileAsync(string fileName,  string content);

        Task<string> ReadFromFileAsync(string fileName);

        Task<List<LogEntry>> ReadLogEntriesAsync(string fileName);
    }
}
