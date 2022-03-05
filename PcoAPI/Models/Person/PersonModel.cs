using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PcoAPI.Interfaces;

namespace PcoAPI.Models.Person
{
    public class PersonModel : IRelationship, IDataObject<PersonAttributesModel>
    {
        [JsonProperty("type")]
        public string PCOType { get; set; }
        [JsonProperty("id")]
        public string ID {get; set;}
        [JsonProperty("attributes")]
        public PersonAttributesModel Attributes {get; set;}
    }
}