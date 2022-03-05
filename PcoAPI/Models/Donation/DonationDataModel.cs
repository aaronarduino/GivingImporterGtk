using System;
using static PcoAPI.Enums.Enums;

namespace PcoAPI.Models.Donation
{
    public class DonationDataModel
    {
        public PaymentMethods PaymentMethod { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public int AmountCents { get; set; }
        public string FundName { get; set; }
    }
}
