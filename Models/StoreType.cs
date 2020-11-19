using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class StoreType
    {
        public int StoreId { get; set; }
        public String Name { get; set; }
        public String FullName { get; set; }
        public string LatLng { get; set; }
        public String DDI { get; set; }
        public String DDD { get; set; }
        public String Phone { get; set; }
        public String WebSite { get; set; }
        public String Address1 { get; set; }
        public String Number { get; set; }
        public String Address2 { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public String StateName { get; set; }
        public String Country { get; set; }
        public String ActivityKey { get; set; }
        public int UserAgentId { get; set; }
        public String Source { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public Boolean Complained { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}