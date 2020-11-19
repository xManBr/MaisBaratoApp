using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class StatusType
    {
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public String StatusName { get; set; }
        public Int32 TranslationId { get; set; }
        public String LastModifiedDate { get; set; }
        public String CreationDate { get; set; }
    }
}
