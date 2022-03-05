using System;
using System.IO;
using System.Linq;
using HCCInfrastructure.Models;

namespace HCCInfrastructure.Services
{
    public class ExceptionFileService
    {
        private readonly Action<string> WriteToScreen;
        public string FolderName { get; set; }
        public string HeaderRow = "\"Pay Type\",\"Batch Date\",\"First Name\",\"Last Name\",\"Amount\",\"Account Name\"";

        public ExceptionFileService(string exceptionFileDirectoryName, Action<string> writeToScreen)
        {
            WriteToScreen = writeToScreen;
            FolderName = exceptionFileDirectoryName;

            EnsureWorkingDirectoryExists();
        }

        public void WriteLine(BatchFileLineModel batchFileLine, string fileName)
        {
            EnsureFileExists(fileName);
            using (var file = new StreamWriter(Directory.GetCurrentDirectory() + "/" + FolderName + "/" + fileName, append: true))
            {
                file.WriteLine(batchFileLine.ToCsvLine());
            }
        }

        private void EnsureFileExists(string fileName)
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "/" + FolderName + "/" + fileName))
            {
                using (var file = new StreamWriter(Directory.GetCurrentDirectory() + "/" + FolderName + "/" + fileName, append: true))
                {
                    file.WriteLine(HeaderRow);
                }
            }
        }

        private void EnsureWorkingDirectoryExists()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            if (!currentDirectory.EnumerateDirectories().ToList().Where(d => d.Name == FolderName).Any())
            {
                WriteToScreen("Creating ExceptionFile Directory");
                Directory.CreateDirectory(currentDirectory + "/" + FolderName);
                WriteToScreen("Working ExceptionFile Created");
                return;
            }
            WriteToScreen("Working ExceptionFile Exists");
        }
    }
}
