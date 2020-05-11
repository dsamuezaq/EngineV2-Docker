using Chariot.Engine.DataAccess.MardisSecurity;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.MardisSecurityViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.MardisSecurity
{
    public class UserBusiness : ABusiness
    {
        protected UserDao _userDao;
        public UserBusiness(ChariotContext _chariotContext,
                            RedisCache distributedCache) : base(_chariotContext, distributedCache)
        {

            _userDao = new UserDao(_chariotContext);
        }
        /// <summary>
        /// Method for get Token API
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public UserTokenModel FindUserBycredentials(string user, string pass) {

            string _data = _userDao.GetUserbycredentials(user, pass);
            UserTokenModel _resultData = new UserTokenModel();
            if (!_data.Equals("no found")) 
            {
                  return SetUserToken(_data);

            };
            _resultData.message = "no found";
            return _resultData;

        }
        /// <summary>
        /// Method for Set token in Redis Cache
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        private UserTokenModel SetUserToken(string idUser) {
            string token = Guid.NewGuid().ToString();

            UserTokenModel _model = new UserTokenModel();
            _model.Id = idUser.ToString();
            _model.token = token;
            _model.DateToken = DateTime.Now.AddDays(1);
            _model.message = "A-OK";
            _RedisCache.Set("UserToken:" + token, _model);
            return _model;


        }
        /// <summary>
        ///Method consume api for token 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool VerifyTokenUser(string Token)
        {
            UserTokenModel _data= _RedisCache.Get<UserTokenModel>("UserToken:" + Token);
            if (_data != null) { 
                if (_data.DateToken > DateTime.Now)
                    return true;
            }
            return false;
        }
    }
}