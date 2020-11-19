using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Mercoplano.Maisbarato.Server.RESTful.Util
{
    public class XmlManager
    {
        public static string BuildXmlStringValue(string xmlRootName, string[] values)
        {
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendFormat("<{0}>", xmlRootName);
            for (int i = 0; i < values.Length; i++)
            {
                xmlString.AppendFormat("<value>{0}</value>", values[i]);
            }
            xmlString.AppendFormat("</{0}>", xmlRootName);

            return xmlString.ToString();
        }

        public static string BuildXmlStringId(string xmlRootName, string[] values)
        {
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendFormat("<{0}>", xmlRootName);
            for (int i = 0; i < values.Length; i++)
            {
                xmlString.AppendFormat("<id>{0}</id>", values[i]);
            }
            xmlString.AppendFormat("</{0}>", xmlRootName);

            return xmlString.ToString();
        }
    }
}