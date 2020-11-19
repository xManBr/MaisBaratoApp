﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mercoplano.Maisbarato.Server.RESTful.Models
{
    [Serializable()]
    public class LanguageModel
    {
        public LanguageModel()
        {
            this.StatusModel = new StatusModel();
        }
        public Int16 Id { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }

        public StatusModel StatusModel { get; set; }
        /*
        public Int16 LanguageId { get; set; }
        public String LanguageCode { get; set; }
        public String LanguageName { get; set; }
         * */
    }

}
