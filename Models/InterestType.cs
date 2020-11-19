using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class InterestType
    {
        public int Id { get; set; }
        public String Interesse { get; set; }
        public String CreationDateView { get; set; }
        public String Url { get; set; }
        public String SessionID { get; set; }
	    public String LatLng { get; set; }
        public bool ShowMaps { get; set; }
        public bool Complained { get; set; }

    }
}
