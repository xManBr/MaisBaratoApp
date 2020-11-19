using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class EmailTO
    {
        public EmailTO()
        {
            this.StatusTO = new StatusModel();
        }
        public int EmailId { get; set; }
        public String ToEmail { get; set; }
        public String FromEmail { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String CreationDate { get; set; }
        public String ObjectTypeId { get; set; }
        public String StatusId { get; set; }
        public StatusModel StatusTO { get; set; }
        public String ReferenceCode { get; set; }
        public String LastModifiedDate { get; set; }
    }
}

