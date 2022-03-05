using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PcoAPI.Interfaces;

namespace PcoAPI.Models
{
    public interface IDataObject<T>
    {
        string PCOType { get; set; }
        string ID {get; set;}
        T Attributes { get; set; }
    }
}