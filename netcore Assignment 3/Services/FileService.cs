using System.Text;
using Microsoft.Extensions.Logging.Abstractions;

namespace netcore_Assignment3.Services
{
    public class FileService: IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        // Saving the data to file
        public async Task SaveToFileAsync(string fileName, string content)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                
                await File.AppendAllTextAsync(filePath, content + Environment.NewLine, Encoding.UTF8);

                _logger.LogInformation($"Data saved to file: {filePath}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to file: {ex.Message}");
            }
        }

        // Reading data from the file
        public async Task<string> ReadFromFileAsync(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    _logger.LogWarning($"File not found: {filePath}");
                    return "File does not exist.";
                }

                string content = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
                _logger.LogInformation($"Data read from file: {filePath}");
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading from file: {ex.Message}");
                return "Error reading file.";
            }
        }

        // Method to read logs and return them as structured json
        public async Task<List<LogEntry>> ReadLogEntriesAsync(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    _logger.LogWarning($"File not found: {filePath}");
                    return new List<LogEntry>();
                }

                var lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
                var logs = new List<LogEntry>();

                foreach (var line in lines)
                {
                    var parts = line.Split(": ", 2);
                    if (parts.Length == 2 && DateTime.TryParse(parts[0], out DateTime timestamp))
                    {
                        logs.Add(new LogEntry { Timestamp = timestamp, Message = parts[1] });
                    }
                }

                _logger.LogInformation($"Logs successfully read from file: {filePath}");
                return logs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading from file: {ex.Message}");
                return new List<LogEntry>();
            }
        }
    }
    
    
}
