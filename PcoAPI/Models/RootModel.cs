using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models
{
    public class RootModel
    {
        [JsonProperty("links")]
        public LinksModel Links {get; set;}
    }
}