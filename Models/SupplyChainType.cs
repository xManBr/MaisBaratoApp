using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class SupplyChainType
    {
        public int SupplyChainId { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public String Category { get; set; }
    }
}