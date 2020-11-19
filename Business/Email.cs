using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;

using Microsoft.Extensions.Configuration;


namespace Mercoplano.Maisbarato.Server.RESTful.Business
{
    public class Email
    {
        private IConfiguration _configuration;

        #region Insert

        public static void Insert(IConfiguration _configuration,EmailTO emailTO)
        {
            EmailTO[] emailTOs = new EmailTO[] { emailTO };
            String emailXml = PreperEmailXml(emailTOs);
            EmailDAO.Insert(_configuration, emailXml);
        }

        public static void Insert(IConfiguration _configuration, EmailTO[] emailTOs)
        {
            String emailXml = PreperEmailXml(emailTOs);
            EmailDAO.Insert(_configuration, emailXml);
        }

        private static String PreperEmailXml(EmailTO[] emailTOs)
        {
            String xmlRootName = "Email";
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendFormat("<{0}>", xmlRootName);
            for (int i = 0; i < emailTOs.Length; i++)
            {
                xmlString.AppendFormat("<{0}>", "Item");
                EmailTO list = emailTOs[i];

                xmlString.AppendFormat("<ToEmail>{0}</ToEmail>", list.ToEmail);
                xmlString.AppendFormat("<FromEmail>{0}</FromEmail>", list.FromEmail);
                xmlString.AppendFormat("<Subject>{0}</Subject>", list.Subject);
                xmlString.AppendFormat("<Body>{0}</Body>", list.Body.Replace("&i", "##"));
                xmlString.AppendFormat("<ObjectTypeId>{0}</ObjectTypeId>", list.ObjectTypeId);
                xmlString.AppendFormat("<StatusId>{0}</StatusId>", list.StatusId);
                xmlString.AppendFormat("<ReferenceCode>{0}</ReferenceCode>", list.ReferenceCode);

                xmlString.AppendFormat("</{0}>", "Item");
            }
            xmlString.AppendFormat("</{0}>", xmlRootName);

            return xmlString.ToString();
        }

        /*
         * 
		,MI.value('ReferenceCode[1]','NVARCHAR(256)') as ReferenceCode
         * */
        #endregion
        public static EmailTemplateTO GetEmailMsg(IConfiguration _configuration, Int32 emailTemplateId, Int16 languageId)
        {
            return EmailTemplateDAO.List(_configuration, emailTemplateId, languageId);
        }


        #region Send
        public static bool Send(List<EmailTO> emailTOs)
        {
            try
            {
                foreach (var email in emailTOs)
                {
                    Send(email);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Send(EmailTO emailTO)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage message = new MailMessage(emailTO.FromEmail, emailTO.ToEmail);
            message.Subject = emailTO.Subject;
            message.Body = emailTO.Body;
            message.BodyEncoding = Encoding.Default;//[TODO] Será que esse é o certo.
            message.IsBodyHtml = true;
            System.Net.NetworkCredential authenticationInfo = new System.Net.NetworkCredential();
            authenticationInfo.UserName = "donotreply@maisbarato.app"; //"donotreply@maisbarato.com";
            authenticationInfo.Password = Config.EMAIL_AUTHENTICATIONINFO;//Password

            try
            {
                smtp.Host = "mail.maisbarato.app";//?????????? isso vai funcionar??
                //smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = authenticationInfo;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;

                smtp.Send(message);

                message = null;
                return true;
            }
            catch (Exception e)
            {
                String msg = e.Message;
                return false;
            }
        }
        #endregion

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}