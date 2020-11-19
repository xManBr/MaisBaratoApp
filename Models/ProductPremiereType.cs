using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ProductPremiereType
    {
        public String Sku { get; set; }
        public String Name { get; set; }
        public String SupplyChainCode { get; set; }
        public String SupplyChainName { get; set; }
    }
}
