using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Batch
{
    public class BatchModel
    {
        [JsonProperty("type")]
        public string PCOType { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("attributes")]
        public NewBatchAttributesModel Attributes {get; set;}
    }
}