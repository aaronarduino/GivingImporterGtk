using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Batch
{
    public class NewBatchAttributesModel {
        [JsonProperty("description")]
        public string Description {get; set;}
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}