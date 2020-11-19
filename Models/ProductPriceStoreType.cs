using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ProductPriceStoreType
    {
        public int ProductPriceId { get; set; }
        public String UserCode { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public Boolean Complained { get; set; }
        public int ProductId { get; set; }
        public int UserAgentId { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Measurement { get; set; }
        public string LatLng { get; set; }
        public int CheckedAmount { get; set; }
        public int NotCheckedAmount { get; set; }
        public String CreationDateView { get; set; }
        public Boolean Editable { get; set; }
        public String ShelfLifeView { get; set; } // Data limite do produto ao preço ofertado...
        public String DeadlineView { get; set; }// Data limite da oferta
        public int StoreId { get; set; }
        public String StoreName { get; set; }
        public String FullName { get; set; }
        public String Source { get; set; }
        public String DDI { get; set; }
        public String DDD { get; set; }
        public String Phone { get; set; }
        public String WebSite { get; set; }
        public String Address1 { get; set; }
        public String Number { get; set; }
        public String Address2 { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public String StateName { get; set; }
        public String Country { get; set; }
        public string ProductName { get; set; }

    }
}
