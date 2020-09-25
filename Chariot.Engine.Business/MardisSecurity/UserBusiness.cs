using AutoMapper;
using Chariot.Engine.DataAccess.MardisSecurity;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisSecurity;
using Chariot.Framework.Complement;
using Chariot.Framework.MardisSecurityViewModel;
using Chariot.Framework.SystemViewModel;
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
                                     RedisCache distributedCache
                                     , IMapper mapper) : base(_chariotContext, distributedCache, mapper)
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

            var _data = _userDao.GetUserbycredentials(user, pass);
            UserTokenModel _resultData = new UserTokenModel();
            if (_data != null)
            {

                UserViewModel _user = new UserViewModel();
                _user.Email = _data.Email;
                _user.Idtype = _data.IdProfile.ToString();
                _user.IdAccount = _data.IdAccount.ToString();
                _user.name = _data.IdPerson.ToString();
                _user.Id   = _data.Id.ToString();
                _user.RoleName = _userDao.GetRoleName(_data.IdProfile);
                _resultData.DateToken = DateTime.Now;
                _resultData.message = "Ok";
                _resultData._user = _user;
                return _resultData ;

            };
            _resultData.message = "Usuario o contraseña incorrecto";
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
           // _model.Id = idUser.ToString();
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

        /// <summary>
        ///Method consume api for token 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public ReplyViewModel GetUserMenu(string idperfil)
        {
            var _data = _userDao.GetMenubyProfile(idperfil);
            ReplyViewModel reply = new ReplyViewModel();
            reply.messege = "No exiten datos";
            reply.status = "Fail";

            if (_data.Count() > 0)
            {

                List<GetMenuViewModel> result = _data.Select(x => new GetMenuViewModel
                {
                    Icon = x.Icon,
                    Name = x.Name,
                    UrlMenu = x.UrlMenu

                }).ToList();
                reply.messege = "success";
                reply.data = result;
                reply.status = "Ok";

            }


            return reply;
        }
    }
}