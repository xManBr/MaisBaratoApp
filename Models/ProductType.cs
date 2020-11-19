using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ProductType
    {
        public int UserAgentId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public Byte[] Picture { get; set; }

        public String PictureBase64 { get; set; }
        
        public int ProductId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public String Source { get; set; }

        public String ObjectTypeId { get; set; }

        public String StatusId { get; set; }

        public Boolean Complained { get; set; }

        public String VirtualPath { get; set; }

    }
}