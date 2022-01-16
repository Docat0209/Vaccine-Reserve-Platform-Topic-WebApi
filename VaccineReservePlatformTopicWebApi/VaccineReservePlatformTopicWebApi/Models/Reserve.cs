using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineReservePlatformTopicWebApi.Models
{
    public class Reserve
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ROCId { get; set; }
        public string VaccineType { get; set; }
        public string City { get; set; }
        public string Dist { get; set; }
        public string HospName { get; set; }

    }
}