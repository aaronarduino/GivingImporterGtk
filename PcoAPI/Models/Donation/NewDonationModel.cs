using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PcoAPI.Models.Designation;

namespace PcoAPI.Models.Donation
{
    public class NewDonationModel
    {
        [JsonProperty("data")]
        public DonationRelationshipsModel Data { get; set; }
        [JsonProperty("included")]
        public List<DesignationModel> Included { get; set; }
    }
}
