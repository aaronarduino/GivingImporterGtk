using System;
using CsvHelper.Configuration.Attributes;

namespace HCCInfrastructure.Models
{
    public class BatchFileLineModel
    {
        [Name("Pay Type")] // Old: [Name("Payment Method")]
        public string PayType { get; set; }
        [Name("Batch Date")] // Old: [Name("Date Received")]
        public DateTime BatchDate { get; set; }
        [Name("First Name")] // Old: [Name("First Name")]
        public string FirstName { get; set; }
        [Name("Last Name")] // Old: [Name("Last Name")]
        public string LastName { get; set; }
        [Name("Amount")] // Old: [Name("Amount")]
        public decimal Amount { get; set; }
        [Name("Account Name")] // Old: [Name("Fund Name")]
        public string AccountName { get; set; }

        public string ToCsvLine()
        {
            // Payment Method,Date Received,First Name,Last Name,Amount,Fund Name
            return $"{PayType},{BatchDate},{FirstName},{LastName},{Amount},{AccountName}";
        }
    }
}
