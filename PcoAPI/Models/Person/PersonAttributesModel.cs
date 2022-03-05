using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Person
{
    public class PersonAttributesModel {
        [JsonProperty("permissions")]
        public string Permissions {get; set;}
        [JsonProperty("first_name")]
        public string FirstName {get; set;}
        [JsonProperty("last_name")]
        public string LastName {get; set;}
        [JsonProperty("donor_number")]
        public int? DonorNumber {get; set;}
    }
}