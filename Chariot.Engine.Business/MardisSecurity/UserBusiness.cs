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
        public string FindUserBycredentials(string user, string pass) {
#if DEBUG
            string _data = _userDao.GetUserbycredentials(user, pass);
            if (!_data.Equals("no found")) 
            {
#endif
                var UserToken = _distributedCache.Get<UserTokenModel>("UserToken:"+ _data);
                if (UserToken != null)
                {
                    if (UserToken.DateToken > DateTime.Now) 
                        return UserToken.token;
                    else return SetUserToken(_data);

                }
                else {
          
                    return SetUserToken(_data);
                }
              
            
            
            };

            return "not found";

        }
        private string SetUserToken(string idUser) {
            string token = Guid.NewGuid().ToString();
            UserTokenModel _model = new UserTokenModel();
            _model.token = token;
            _model.DateToken = DateTime.Now.AddDays(1);
            _distributedCache.Set("UserToken:" + idUser, _model);
            return token;


        }

    }
}