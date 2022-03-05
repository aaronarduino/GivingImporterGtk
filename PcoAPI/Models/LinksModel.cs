using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models
{
    public class LinksModel
    {
        [JsonProperty("self")]
        public string Self {get; set;}
    }
}