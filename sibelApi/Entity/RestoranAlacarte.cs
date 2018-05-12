using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sibelApi.Entity
{
    public class RestoranAlacarte
    {
        public string code;
        public string description;
        public string hotelId;
        [JsonProperty("params")]
        public List<AlacarteParams> paramz;
        public int unitId;
        public string unitKey;
    }
    public class AlacarteParams
    {
        public int capacity;
        public bool childSta;
        public string desc;
    }
}
