using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ProductPriceType
    {


        public Boolean Editable { get; set; }

        public int ProductPriceId { get; set; }

        public int StoreId { get; set; }

        public String Store { get; set; }

        public int UserAgentId { get; set; }

        public String UserCode { get; set; }

        public int ProductId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
        
        public string Measurement { get; set; }

        public string LatLng { get; set; }

        public int CheckedAmount { get; set; }

        public int NotCheckedAmount { get; set; }

        public String ObjectTypeId { get; set; }

        public String StatusId { get; set; }

        public String CreationDateView { get; set; }

        public Boolean Complained { get; set; }

        public String ShelfLifeView { get; set; } // Data limite do produto ao preço ofertado...

        public String DeadlineView { get; set; }// Data limite da oferta

        public String TargetName { get; set; }

        public Int16 IsShoppingList { get; set; }

        public String ActivityKey { get; set; }


    }
}