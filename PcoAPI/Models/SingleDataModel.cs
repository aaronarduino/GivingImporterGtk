using System;
using Newtonsoft.Json;

namespace PcoAPI.Models
{
    public class SingleDataModel<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
