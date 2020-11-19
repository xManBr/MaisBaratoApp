using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Mercoplano.Maisbarato.Server.RESTful.Security;

using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Security.Cryptography;

using Mercoplano.Maisbarato.Server.RESTful.Util;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/[controller]")]
    public class LoginVolleyController : Controller
    {
        private IConfiguration _configuration;

        public LoginVolleyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public TokenType Post(
            String code,
            String password,
            [FromServices]UsersDAO usersDAO,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            UserAgentType usuarioBase = new UserAgentType();
            if (code != null && !String.IsNullOrWhiteSpace(code))
            {
                usuarioBase = usersDAO.Find(code);
                using (MD5 md5Hash = MD5.Create())
                {
                    string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, password);
                    credenciaisValidas = (usuarioBase != null &&
                        code == usuarioBase.Code &&
                        accessKey == usuarioBase.Password);
                }
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioBase.UserAgentId.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.UserAgentId.ToString())
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new TokenType
                {
                    Authenticated = true,
                    Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    AccessToken = token,
                    Message = "OK"
                };
            }
            else
            {
                return null;
                /*
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
                */
            }
        }
    }
}
