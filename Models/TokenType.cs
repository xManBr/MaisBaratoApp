using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class TokenType
    {
        public Boolean Authenticated { get; set; }
        public String Created { get; set; }
        public String Expiration { get; set; }
        public String AccessToken { get; set; }
        public String Message { get; set; }
        public String ActivatedUser { get; set; }
    }
}
