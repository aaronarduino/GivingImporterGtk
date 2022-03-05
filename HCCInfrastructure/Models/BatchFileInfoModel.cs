using System;

namespace HCCInfrastructure.Models
{
    public class BatchFileInfoModel
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public bool Exists { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastPollTime { get; set; }
    }
}
