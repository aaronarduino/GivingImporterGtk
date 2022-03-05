using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Person
{
    public class PersonsModel : RootModel
    {
        [JsonProperty("data")]
        public List<PersonModel> Data {get; set;}
    }
}