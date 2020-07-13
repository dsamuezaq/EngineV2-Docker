using Chariot.Engine.Business.MardisSecurity;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardisSecurityViewModel;
using Chariot.Framework.SystemViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Engine_V2.Libraries
{
    public class AController<T> : ControllerBase
    {
        #region variable Class
        protected string TableName;
        protected string ControllerName;
        #endregion
        ChariotContext Context { get; }
        protected  RedisCache _distributedCache;
        protected UserBusiness _userBusiness;
        protected ReplyViewModel reply = new ReplyViewModel();

        private readonly AppSettings _appSettings;
        private readonly IConfiguration configuration;
        public AController(ChariotContext _chariotContext, RedisCache distributedCache, IOptions<AppSettings> appSettings)
        {
            Context = _chariotContext;
            _distributedCache = distributedCache;
            _userBusiness = new UserBusiness(_chariotContext, distributedCache);
            _appSettings = appSettings.Value;
     
        }

        /// <summary>
        /// Get token by user with Rediscache
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        //protected object GetUserToken(string user, string pass)
        //{
        //  return  
        //}}

        public UserTokenModel auth(string user , string pass )
        {
            
            var _reply = _userBusiness.FindUserBycredentials("sertecomcell@prospeccionclaro.com.ec", "00");
            return _reply;
        }


        protected bool Isvalidauthtoken(string token)
        {
            return _userBusiness.VerifyTokenUser(token);
        }
        /// <summary>
        /// generate token that is valid for 7 days
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token user</returns>
        protected string generateJwtToken(UserTokenModel InfoUser)
        {
            // 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, InfoUser._user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
