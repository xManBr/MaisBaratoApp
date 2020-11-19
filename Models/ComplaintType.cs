using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ComplaintType
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public String LatLng { get; set; }
        public int ProductId { get; set; }
        public int ProductPriceId { get; set; }
        public int StoreId { get; set; }
        public String Note { get; set; }
        public String SourceIP { get; set; }
        public int InterestId { get; set; }

    }
}
