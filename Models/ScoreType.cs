using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ScoreType
    {
        public int UserAgentId { get; set; }
        public int Store { get; set; }
        public int Product { get; set; }
        public int ProductPrice { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }        
    }
}
