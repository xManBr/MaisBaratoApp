using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class UserAgentType
    {
        public int UserAgentId { get; set; }
        public String Code { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String PhoneDDI { get; set; }
        public String PhoneDDDNumber { get; set; }
        public String Email { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public String ActivityKey { get; set; }
        public String UserLanguageCode { get; set; }
    }
}
