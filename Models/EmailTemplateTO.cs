using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    public class EmailTemplateTO
    {
        public EmailTemplateTO()
        {
            this.LanguageTO = new LanguageModel();
        }
        public Int32 EmailTemplateId {get;set;}
        public Int16 LanguageId {get;set;}
        public LanguageModel LanguageTO { get; set; }
        public String EmailSubejct {get;set;}
        public String EmailMessage {get;set;}
        public String CreationDate {get;set;}
        public String ObjectTypeId {get;set;}
        public String StatusId {get;set;}
        public String LastModifiedDate {get;set;}      
 
    }
}
