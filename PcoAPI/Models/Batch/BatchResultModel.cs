using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.Batch
{
    public class BatchResultModel
    {
        [JsonProperty("data")]
        public BatchModel Data { get; set; }
    }
}
