using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PcoAPI.Interfaces;
using PcoAPI.Models.PaymentSource;
using PcoAPI.Models.Person;

namespace PcoAPI.Models.Donation
{
    public class DonationRelationshipsModel: DonationModel
    {
        [JsonProperty("relationships")]
        public NewDonationRelationshipsModel Relationships { get; set; }
    }

    public class NewDonationRelationshipsModel
    {
        [JsonProperty("person")]
        public SingleDataModel<PersonModel> Person { get; set; }
        [JsonProperty("payment_source")]
        public SingleDataModel<PaymentSourceModel> PaymentSource { get; set; }
    }
}
