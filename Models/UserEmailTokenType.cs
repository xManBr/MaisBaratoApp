using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class UserEmailTokenType
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }

    }
}
