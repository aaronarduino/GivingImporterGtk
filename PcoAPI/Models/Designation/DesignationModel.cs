using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.Designation
{
    public class DesignationModel
    {
        [JsonProperty("type")]
        public string PCOType { get; set; }
        [JsonProperty("attributes")]
        public DesignationAttributesModel Attributes { get; set; }
        [JsonProperty("relationships")]
        public DesignationRelationshipModel Relationships { get; set; }
    }
}
