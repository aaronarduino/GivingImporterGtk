using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using HCCInfrastructure.Data;
using HCCInfrastructure.Helpers;
using HCCInfrastructure.Models;
using PcoAPI.Models.Donation;
using PcoAPI.Services;

namespace HCCInfrastructure.Services
{
    public class GivingImporterService
    {
        private readonly string DataDirectoryName = "workingData";
        private readonly string ExceptionFileDirectoryName = "exceptionData";
        private readonly string DefaultPaymentSource = "Servant Keeper";
        private readonly string ApiUrl;
        private readonly string ClientId;
        private readonly string ClientSecret;
        private readonly int RowSplitLimit;
        private readonly Repository Repository;
        private readonly ExceptionFileService ExceptionFileService;
        private readonly Action<string> WriteToScreen;
        private readonly Action<double> UpdateUploadProgressBar;
        private readonly Dictionary<string, string> FundMapping;

        public string DataDirectory;

        public GivingImporterService(string apiUrl, string clientId, string clientSecret, int rowSplitLimit, Dictionary<string, string> fundMap, Repository repository, Action<string> writeToScreen, Action<double> updateUploadProgressBar)
        {
            DataDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), DataDirectoryName);
            ApiUrl = apiUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
            RowSplitLimit = rowSplitLimit;
            WriteToScreen = writeToScreen;
            UpdateUploadProgressBar = updateUploadProgressBar;
            Repository = repository;
            ExceptionFileService = new ExceptionFileService(ExceptionFileDirectoryName, writeToScreen);
            FundMapping = fundMap;
        }

        public void EnsureWorkingDirectoryExists()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            if (!currentDirectory.EnumerateDirectories().ToList().Where(d => d.Name == DataDirectoryName).Any())
            {
                WriteToScreen("Creating Working Directory");
                Directory.CreateDirectory(DataDirectory);
                WriteToScreen("Working Directory Created");
                return;
            }
            WriteToScreen("Working Directory Exists");
        }

        public void SyncFileSystemAndBatchFileQueue()
        {
            DirectoryInfo workingDirectory = new DirectoryInfo(DataDirectory);
            Dictionary<string, bool> batchFileInfoHashMap = new Dictionary<string, bool>();
            List<FileInfo> csvFilesToAddToDatabase = new List<FileInfo>();
            List<BatchFileInfoModel> csvFilesNotFound = new List<BatchFileInfoModel>();

            // 1. Get csv files in working directory
            var csvFilesInWorkingDirectory = workingDirectory.EnumerateFiles("*.csv", SearchOption.TopDirectoryOnly);

            // 2. Get file info from database
            var batchFileInfosFromDatabase = Repository.GetBatchFileQueue();

            // 3. Diff the working dir and database
            foreach(var batchFileInfo in batchFileInfosFromDatabase)
            {
                // 3.1 Create list of not found csv files
                if (!csvFilesInWorkingDirectory.Any(f => f.FullName == batchFileInfo.FilePath + "/" + batchFileInfo.FileName))
                {
                    csvFilesNotFound.Add(batchFileInfo);
                }
                batchFileInfoHashMap[batchFileInfo.FilePath+"/"+batchFileInfo.FileName] = true;
            }

            // 4. Create list of files to add to database
            foreach(var csvFile in csvFilesInWorkingDirectory)
            {
                if (!batchFileInfoHashMap.ContainsKey(csvFile.FullName))
                {
                    csvFilesToAddToDatabase.Add(csvFile);
                }
            }

            // 5. Change status of not found files in database
            foreach(var batchFileInfo in csvFilesNotFound)
            {
                Repository.SetBatchFileExists(batchFileInfo, false);
            }

            // 6. Insert new file info into database
            foreach (var scvFile in csvFilesToAddToDatabase.OrderBy(f => f.Name))
            {
                BatchFileInfoModel batchFileInfo = new BatchFileInfoModel()
                {
                    Status = "New",
                    Exists = true,
                    FileName = scvFile.Name,
                    FilePath = scvFile.Directory.FullName,
                    DateAdded = DateTime.Now,
                    LastPollTime = DateTime.Now
                };
                Repository.AddBatchFileToQueue(batchFileInfo);
            }
        }

        public void ImportFromServantKeeper(string dataFile)
        {
            WriteToScreen("Reading File...");

            // 1. Read Servant Keeper CSV file.
            var lineCount = File.ReadLines(dataFile).Count();
            var file = new FileInfo(dataFile);

            // 2.1 Either copy file or:
            if (lineCount <= RowSplitLimit)
            {
                WriteToScreen("File is less than 500 rows. Copying to data directory.");

                try
                {
                    File.Copy(dataFile, System.IO.Path.Combine(DataDirectory, file.Name), true);
                }
                catch (IOException ex)
                {
                    WriteToScreen("[ERROR]: " + ex.Message);
                }

                WriteToScreen("Copy done.");
                return;
            }

            // 2.2 Write X Number of records out to individual Batch Files.
            WriteToScreen("File is greater than 500 rows. Writing split data to data directory.");
            var linesToWrite = FileHelpers.ReadFileAsLineSets(dataFile).ToList();

            for(var i = 0; i < linesToWrite.Count(); i++)
            {
                File.WriteAllLines(System.IO.Path.Combine(DataDirectory, $"{i}-{file.Name}"), linesToWrite[i]);
            }
            WriteToScreen("File write done.");
        }

        public async void UploadBatchToGiving(BatchFileInfoModel batchFileInfo, string batchName, Action<string> displayErrorMessage)
        {
            if(batchFileInfo.Status == EnumHelper.BatchFileStatus.Uploaded.ToString() || batchFileInfo.Status == EnumHelper.BatchFileStatus.UploadedWithErrors.ToString())
            {
                WriteToScreen("[ERROR]: Already uploaded file.");
                displayErrorMessage("Already uploaded file."); // Display error to user with callback
                return;
            }

            PcoAPI.Models.Batch.BatchModel batch;
            bool exceptions = false;
            DonationService donationService = new DonationService(DefaultPaymentSource, batchName, ApiUrl, ClientId, ClientSecret, WriteToScreen);

            // 1. Check if batch has been created in Giving.
            UpdateUploadProgressBar(0.01); // Show user that progress is being made

            // 1.1 Get all batches from Giving
            var batches = await donationService.GetBatches();

            // 1.2 Find batch in list
            if (batches.Data.Where(b => b.Attributes.Status != "committed").Any(b => b.Attributes.Description == donationService.BatchDescription))
            {
                batch = batches.Data.FirstOrDefault(b => b.Attributes.Description == donationService.BatchDescription);
                UpdateUploadProgressBar(0.02); // Show user that progress is being made
            }
            else
            {
                // 2. Create batch in Giving if needed.
                var resultBatches = await donationService.CreateBatch();
                try
                {
                    batch = resultBatches.Data;
                }
                catch (Exception ex)
                {
                    WriteToScreen("[ERROR]: " + ex.Message);
                    Repository.SetBatchFileStatus(batchFileInfo.ID, EnumHelper.BatchFileStatus.Error);
                    return;
                }
                UpdateUploadProgressBar(0.02); // Show user that progress is being made
            }

            // 3. Read batch file records
            var records = FileHelpers.ReadBatchFileAsLineModels($"{batchFileInfo.FilePath}/{batchFileInfo.FileName}");
            for (int i = 0; i < records.Count; i++)
            {
                NewDonationModel donation;

                // 3.1 Map csv record to donation data
                var batchData = DonationHelper.MapCsvRecordToDonationData(records[i], FundMapping);

                try
                {
                    // 3.1 Create Donation
                    donation = await donationService.CreateDonation(batchData);
                }
                catch (Exception ex)
                {
                    exceptions = true;

                    // 3.1.1 Handle errors by displaying error message to user
                    //       and writing record to exception file.
                    ExceptionFileService.WriteLine(records[i], "Exceptions-" + batchFileInfo.FileName);
                    displayErrorMessage(" unable to upload. Error message was: " + ex.Message); // Display error to user with callback
                    continue;
                }

                // 3.2 Add Donation to Batch
                var addComplete = await donationService.AddToBatch(batch, donation);

                // 3.3 Show user progress
                double fraction = (double)(i + 1) / (double)records.Count;
                UpdateUploadProgressBar(fraction);
            }

            // 4. Maybe reset UI
            if (exceptions)
            {
                Repository.SetBatchFileStatus(batchFileInfo.ID, EnumHelper.BatchFileStatus.UploadedWithErrors);
            }
            else
            {
                Repository.SetBatchFileStatus(batchFileInfo.ID, EnumHelper.BatchFileStatus.Uploaded);
            }
            UpdateUploadProgressBar(0.0);
        }
    }
}
