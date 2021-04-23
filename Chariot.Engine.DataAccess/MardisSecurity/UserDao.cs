using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Engine.DataObject.MardisSecurity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisSecurity
{
   public class UserDao:  ADao
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
        public string GetRoleName(Guid Idrole)
        {
            try
            {
                var _data = Context.Profiles.Where(x => x.Id == Idrole).ToList();
                return _data.Count() > 0 ? _data.FirstOrDefault().Name : "Sin Identificar";
            }
            catch (Exception e)
            {

                return null;
            }


        }
        /// <summary>
        /// Exist user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public List<Menu> GetMenubyProfile(string idprofile)
        {
            try
            {
                var _data = from a in Context.AuthorizationProfiles
                            join m in Context.Menus on a.IdMenu equals m.Id
                            where a.IdProfile.ToString().Equals(idprofile)
                            select m;
                return _data.Count() > 0 ? _data.ToList() : null;
            }
            catch (Exception e)
            {

                return null;
            }


        }
        #region SurtiAPP

        /// <summary>
        /// Exist user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public Distributor GetUserbycredentialsSurti(string user, string pass)
        {
            try
            {

                var usuarios = from u in Context.Users
                              join s in Context.Distributors on u.Id equals s.Iduser
                              where u.Email == user && u.Password == pass
                              select  new Distributor { 
                              IDDISTRIBUTOR=s.IDDISTRIBUTOR,
                              NAME=s.NAME,
                              user=u
                              
                              }
                             ;
                var usuario = usuarios.ToList();
   
                return usuario.Count() > 0 ? usuario.FirstOrDefault() : null;
            }
            catch (Exception e)
            {

                return null;
            }


        }
        public int ConsulatarIdDistribuidorDeVendedorXIdVendedor(int idvendedor)
        {
            try
            {
                var Distribuidor = Context.SALESMAN_DISTRIBUTORS.Where(x => x.IDSALESMAN.Equals(idvendedor));

                return Distribuidor.Count() > 0 ? Distribuidor.First().IDDISTRIBUTOR : 0;
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return 0;
            }
       

        }
        #endregion
    }
}
