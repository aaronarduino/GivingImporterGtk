using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Donation
{
    public class DonationsModel : RootModel
    {
        [JsonProperty("data")]
        public List<DonationModel> Data {get; set;}
    }
}