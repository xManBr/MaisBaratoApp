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
    public class LoginController : Controller
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*
         * 1-
         * Fazer uma rotina de gravação de usuário com update de senha - que deve ser igual ao token de acesso enviado pelo feccebook
         * o memso took deve ser gravado no browser por cook - valido enquanto o usuário estiver logado no site....
         * 
         * 2-Crar uma tabela de associação da cozinha (Kithen) com todos os usuário associados a ela pelo usuário master.... (administrador)
         * e se o usuário ainda não existir na base de dados do aplicativo - deve ser cadastrado com os dados do facebbok - nome e email....
         * os demais dados podem ser complementados depois - isso vai permitir que o usuário tenha acesso a mais funcionalidades do site
         * - enquanto o cadastro estiver parcial  vai ter acesso a telas basicas - sem poder de cadastro - soh visualização....
         * - dados complementares possíveis: telefone, endereço, cidade, estado.
         * */

        [AllowAnonymous]
        [HttpPost]
        public TokenType Post(
            [FromBody]UserAgentType usuario,
            [FromServices]UsersDAO usersDAO,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            UserAgentType usuarioBase = new UserAgentType();
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Code))
            {
                usuarioBase = usersDAO.Find(usuario.Code);
                using (MD5 md5Hash = MD5.Create())
                {
                    string accessKey = Md5HashUtility.GetMd5Hash(md5Hash, usuario.Password);
                    credenciaisValidas = (usuarioBase != null &&
                        (usuario.Code == usuarioBase.Code || usuario.Code == usuarioBase.Email) &&
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
                    Message = "OK",
                    ActivatedUser = usuarioBase.StatusId == "ACT" ? "1" : "0"
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