using Chariot.Engine.Business.MardisSecurity;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
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
        protected string GetUserToken(string user, string pass)
        {
          return  _userBusiness.FindUserBycredentials(user, pass);
        }
    }
}
