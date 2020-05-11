using Chariot.Engine.Business.MardisSecurity;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.SystemViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AController(ChariotContext _chariotContext, RedisCache distributedCache)
        {
            Context = _chariotContext;
            _distributedCache = distributedCache;
            _userBusiness = new UserBusiness(_chariotContext, distributedCache);
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
        [Route("Auth")]
        [HttpPost]
        public object auth(string user , string pass )
        {
            
            var _reply = _userBusiness.FindUserBycredentials("sertecomcell@prospeccionclaro.com.ec", "00");
            return _reply;
        }


        protected bool Isvalidauthtoken(string token)
        {
            return _userBusiness.VerifyTokenUser(token);
        }
    }
}
