using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisSecurity
{
   public class UserDao: ADao
    {
       public UserDao(ChariotContext context)
     : base(context)
        {
         
        }
        /// <summary>
        /// Exist user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public User GetUserbycredentials(string user, string pass)
        {
            try
            {
                var _data = Context.Users.Where(x => x.Email == user && x.Password == pass).ToList();
                return _data.Count() > 0 ? _data.FirstOrDefault() : null;
            }
            catch (Exception e)
            {

                return null;
            }
     
        
        }
    }
}
