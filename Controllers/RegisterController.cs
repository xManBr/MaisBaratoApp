using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Mercoplano.Maisbarato.Server.RESTful.Security;
using Dapper;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Mercoplano.Maisbarato.Server.RESTful.Util;
using Mercoplano.Maisbarato.Server.RESTful.Security;

using Mercoplano.Maisbarato.Server.RESTful.Business;


using System.Linq;
using System.Data;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Produces("application/json")]
    [Route("api/Register/[action]")]
    public class RegisterController : Controller
    {
        private IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        #region Atorization

        [HttpPost]
        [Authorize("Bearer")]
        [ActionName("UpdateUser")]
        public int UpdateUser([FromBody] Object json)
        {
            int errorCode = 999;
            var userAgentType = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAgentType>(json.ToString());

            try
            {
                string activityKey = Guid.NewGuid().ToString() + "_" + Guid.NewGuid().ToString();

                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "common.UserAgentTempUpdate"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Code", userAgentType.Code);
                    p.Add("@IN_Name", userAgentType.Name);
                    p.Add("@IN_PhoneDDI", userAgentType.PhoneDDI);
                    p.Add("@IN_PhoneDDDNumber", userAgentType.PhoneDDDNumber);
                    p.Add("@IN_Email", userAgentType.Email);
                    p.Add("@IN_ObjectTypeId", "DEF");
                    p.Add("@IN_StatusId", "ACT");
                    p.Add("@IN_ActivityKey", activityKey);
                    p.Add("@IN_UserLanguageCode", userAgentType.UserLanguageCode);
                    p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                    errorCode = p.Get<int>("@IN_ERROR");
                }
                return errorCode;
            }
            catch (Exception e)
            {
                String xx = e.Message;
                return errorCode;
            }
        }

        #endregion

        #region AllowAnonymous

        [AllowAnonymous]
        [HttpGet]
        [ActionName("WToken")]
        public StatusType WToken(String e)
        {//Requisita um numero de 6 algarismos para permitir mudança de senha
            return WTokenInternal(e);
        }

        private StatusType WTokenInternal(String e)
        {   // Requisita um numero de 6 algarismos para permitir mudança de senha
            // [TODO] Sera que com a nova tonica, seria o caso de pedir senha.... nao sei ainda...
            StatusType statusType = new StatusType();
            statusType.ObjectTypeId = "WToken";
            statusType.StatusId = "1";
            string accessToken = String.Empty;
            if (e != String.Empty)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    //[TODO] Registrar o campo email com chave única
                    accessToken = RandomProvider.GetRandomNumber(100000, 999999).ToString();
                    using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                    {
                        var sql = "common.UserEmailTokenInsert";
                        var p = new DynamicParameters();
                        p.Add("@IN_Email", e);
                        p.Add("@IN_Token", accessToken);
                        p.Add("@IN_DeadlineMinute", Config.DEAD_LINE_MINUTE_TOKEN);
                        p.Add("@IN_ERROR", 1, System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);

                        conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                        statusType.StatusId = p.Get<int>("@IN_ERROR").ToString();//retorna 0 para sucesso!
                    }
                }
            }

            return statusType;//Retono 1 - > email não encontrado na base.
        }


        [AllowAnonymous]
        [HttpGet]
        [ActionName("UpdateStatus")]
        public UserAgentType UpdateStatus(String e, String t, String w)
        {   // Ativa status do usuário se todos os demais parametros esiverem coindidentes....
            int errorCode = 0;
            List<UserAgentType> list = new List<UserAgentType>();
            UserAgentType userAgentType = new UserAgentType();
            userAgentType.StatusId = "PEND";
            try
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        //Recuperar o token gerado...
                        string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, w);
                        var sql = "common.UserAgentUpdateStatus";
                        var p = new DynamicParameters();
                        p.Add("@IN_Email", e);
                        p.Add("@IN_StatusId", "ACT");
                        p.Add("@IN_AccessKey", accessKey);
                        p.Add("@IN_Token", t);
                        p.Add("@IN_ERROR", 1, System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);

                        conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                        errorCode = p.Get<int>("@IN_ERROR");

                        if (errorCode == 0)
                        {
                            sql = "common.UserAgentSelect";
                            p = new DynamicParameters();
                            p.Add("@IN_CodeOrEmail", e);
                            p.Add("@IN_Password", accessKey);
                            using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                            {
                                list = multi.Read<UserAgentType>().ToList();
                            }

                            if (list.Count > 0)
                            {
                                userAgentType = list[0];
                            }
                        }
                        else
                        {
                            userAgentType.StatusId = "PEND";
                        }
                    }
                }
                return userAgentType;
            }
            catch (Exception ex)
            {
                String xx = ex.Message;
                userAgentType.StatusId = "ERR";
                return userAgentType;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("ChangeKey")]
        public UserAgentType ChangeKey(String e, String t, String w)
        {// Se a alteração de senha for bem sucedida retorna um objeto com code de usuário, caso contrario, um objeto nulo...
            UserAgentType userAgentType = null;
            if ((e != String.Empty) && (t != String.Empty) && (w != String.Empty))
            {
                List<UserAgentType> list = new List<UserAgentType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    //Recuperar o token gerado...
                    var sql = "common.UserEmailTokenSelect";
                    var p = new DynamicParameters();
                    p.Add("@IN_Email", e);
                    p.Add("@IN_Token", t);
                    userAgentType = conexao.QueryFirstOrDefault<UserAgentType>(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                    if (userAgentType != null)
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            try
                            {
                                string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, w);
                                sql = "common.UserAgentChangePw";
                                p = new DynamicParameters();
                                p.Add("@IN_UserAgentId", userAgentType.UserAgentId);
                                p.Add("@IN_PasswordNew", accessKey);
                                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);//[TODO] Verificar se não deu erro no update


                                // Retorna code para aplicativo ciente providenciar Login
                                return userAgentType;//Sucesso
                            }
                            catch
                            {
                                userAgentType = null; //Fracasso
                            }
                        }
                    }
                }
            }
            return userAgentType;
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("IdentityExternal")]
        public UserAgentType IdentityExternal(String e, String w, String n)
        {   // Entra email e senha(accesskey), se usuário não existe grava e se existe altera accesskey... 
            // Agora grava a senha com token pendente, soh atualiza depois que user informar token enviado ao email
            // Os upload do usuário poderam ou não serem publicados, tudo depende o status adminitrativo - indicando se na epoca pode publicacao de usuario com token pedente
            // Para que o usuário tenha acesso total deve estar com o cadastro, e token, atualizados....
            // ideia - deixar fazer uma primeria publicação, as demais, sujeita a moderação, as demais, somente com token atualizado....
            UserAgentType userAgentType = null;

            if ((e != String.Empty) && (w != String.Empty))
            {
                List<UserAgentType> list = new List<UserAgentType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        try
                        {
                            string activityKey = Guid.NewGuid().ToString() + "_" + Guid.NewGuid().ToString();
                            string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, w);
                            string sql = "common.IdentityExternalInsertOrUpdate";
                            var p = new DynamicParameters();
                            p = new DynamicParameters();
                            p.Add("@IN_Email", e);
                            p.Add("@IN_PasswordNew", accessKey);
                            p.Add("@IN_Code", activityKey.Substring(0, 10));
                            p.Add("@IN_Name", n);
                            p.Add("@IN_ActivityKey", activityKey);
                            p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);//[TODO] Verificar se não deu erro no update
                                                                                                         // Retorna code para aplicativo ciente providenciar Login

                            sql = "common.UserAgentSelect";
                            p = new DynamicParameters();
                            p.Add("@IN_CodeOrEmail", e);
                            p.Add("@IN_Password", accessKey);
                            using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                            {
                                list = multi.Read<UserAgentType>().ToList();
                            }

                            if (list.Count > 0)
                            {
                                userAgentType = list[0];
                            }
                        }
                        catch (Exception ex)
                        {
                            String mm = ex.Message;
                            userAgentType = null; //Fracasso
                        }
                    }
                }
            }
            return userAgentType;
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("Confirmation")]
        public void Confirmation(String accessKey)
        {
            if (accessKey != String.Empty)
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "common.UserAgentActivity";
                    var p = new DynamicParameters();
                    p.Add("@IN_ActivityKey", accessKey);
                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Subscription")]
        public int Subscription([FromBody] Object json)
        {
            int errorCode = 999;
            var userAgentType = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAgentType>(json.ToString());

            try
            {
                string activityKey = Guid.NewGuid().ToString() + "_" + Guid.NewGuid().ToString();

                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "common.UserAgentTempInsert"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Code", userAgentType.Code);
                    p.Add("@IN_Name", userAgentType.Name);
                    p.Add("@IN_PhoneDDI", userAgentType.PhoneDDI);
                    p.Add("@IN_PhoneDDDNumber", userAgentType.PhoneDDDNumber);
                    p.Add("@IN_Email", userAgentType.Email);
                    p.Add("@IN_ObjectTypeId", "DEF");
                    p.Add("@IN_StatusId", "ACT");
                    p.Add("@IN_ActivityKey", activityKey);
                    p.Add("@IN_UserLanguageCode", userAgentType.UserLanguageCode);
                    p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                    errorCode = p.Get<int>("@IN_ERROR");
                }
                return errorCode;
            }
            catch (Exception e)
            {
                String xx = e.Message;
                return errorCode; // Erro Generico
            }
        }



        [AllowAnonymous]
        [HttpGet]
        [ActionName("GetUser")]
        public UserAgentType GetUser(String e, String w)
        {
            return GetUserInterno(e, w);
        }
        /*
        if ((e != String.Empty) && (w != String.Empty))
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, w);

                List<UserAgentType> list = new List<UserAgentType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "common.UserAgentSelect";
                    var p = new DynamicParameters();
                    p.Add("@IN_CodeOrEmail", e);
                    p.Add("@IN_Password", accessKey);
                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<UserAgentType>().ToList();
                    }
                }

                if (list.Count > 0)
                {
                    return list[0];
                }
            }
        }
        return null;
        */
        #endregion

        private UserAgentType GetUserInterno(String e, String w)
        {
            if ((e != String.Empty) && (w != String.Empty))
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, w);

                    List<UserAgentType> list = new List<UserAgentType>();
                    using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                    {
                        var sql = "common.UserAgentSelect";
                        var p = new DynamicParameters();
                        p.Add("@IN_CodeOrEmail", e);
                        p.Add("@IN_Password", accessKey);
                        using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                        {
                            list = multi.Read<UserAgentType>().ToList();
                        }
                    }

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                }
            }
            return null;
        }

    }
}
