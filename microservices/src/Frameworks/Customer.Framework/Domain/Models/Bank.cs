using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Framework.Domain.Models
{
    public class ResultList
    {
        [JsonProperty("result")]
        public List<Result> result { get; set; }
        public string errorMessage { get; set; }
        public string errorMessages { get; set; }
        public bool hasError { get; set; }
        public string timeGenerated { get; set; }
   
    }

   
    public class Result
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }
}
