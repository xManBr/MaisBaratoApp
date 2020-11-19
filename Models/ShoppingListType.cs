using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ShoppingListType
    {
        public int ListId { get; set; }
        public int UserAgentId { get; set; }
        public String TargetName { get; set; }
        public String Sku { get; set; }
        public String ProductName { get; set; }
        public int Amount { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public String CreationDateView { get; set; }
        public String LastModifiedDateView { get; set; }
    }
}