using System;
using PcoAPI.Models.Fund;
using PcoAPI.Models;
using Newtonsoft.Json;

namespace PcoAPI.Models.Designation
{
    public class DesignationRelationshipModel
    {
        [JsonProperty("fund")]
        public SingleDataModel<FundModel> Fund { get; set; }
    }
}
