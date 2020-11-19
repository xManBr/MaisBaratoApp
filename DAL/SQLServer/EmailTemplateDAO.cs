using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Mercoplano.Maisbarato.Server.RESTful.Models;

namespace Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer
{
    public class EmailTemplateDAO
    {
        public static EmailTemplateTO List(IConfiguration _configuration, Int32 emailTemplateId, Int16 languageId)
        {
            List<EmailTemplateTO> emailTemplateTOs = new List<EmailTemplateTO>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "common.p_EmailTemplate_s"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_EmailTemplateId", emailTemplateId);
                p.Add("@IN_LanguageId", languageId);

                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    emailTemplateTOs = multi.Read<EmailTemplateTO>().ToList();
                    /*
                    emailTemplateTO.CreationDate = drr["CreationDate"].ToString();
                    emailTemplateTO.EmailMessage = drr["EmailMessage"].ToString();
                    emailTemplateTO.EmailSubejct = drr["EmailSubejct"].ToString();
                    emailTemplateTO.EmailTemplateId = Convert.ToInt32(drr["EmailTemplateId"]);
                    emailTemplateTO.LanguageId = Convert.ToInt16(drr["LanguageId"]);
                    emailTemplateTO.LastModifiedDate = drr["LastModifiedDate"].ToString();
                    emailTemplateTO.ObjectTypeId = drr["ObjectTypeId"].ToString();
                    emailTemplateTO.StatusId = drr["StatusId"].ToString();
                    emailTemplateTO.LanguageTO.Id = emailTemplateTO.LanguageId;
                    emailTemplateTO.LanguageTO.Code = drr["LanguageCode"] != DBNull.Value ? drr["LanguageCode"].ToString() : String.Empty;
                    emailTemplateTO.LanguageTO.Name = drr["LanguageName"] != DBNull.Value ? drr["LanguageName"].ToString() : String.Empty;
                     * */
                }

                if (emailTemplateTOs.Count > 0)
                { return emailTemplateTOs[0]; }
                else
                {
                    return null;
                }
            }
        }
    }
}
