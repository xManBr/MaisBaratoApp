using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class ProductFileType
    {
        public int ProductFileId { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ObjectTypeId { get; set; }
        public string StatusId { get; set; }
        public string VirtualPath { get; set; }
        public Byte[] Picture { get; set; }
    }
}
