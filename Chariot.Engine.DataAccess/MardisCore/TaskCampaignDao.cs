using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisCore
{
    public class TaskCampaignDao : ADao
    {
        public TaskCampaignDao(ChariotContext context)
      : base(context)
        {

        }
        public object GetCampaing()
        {
            //   var consulta = Context.Campaigns.Include(t => t.Account).Where(c => c.StatusRegister == CStatusRegister.Active).ToList();

            var consulta = from c in Context.Campaigns
                           join a in Context.Accounts on c.IdAccount equals a.Id
                           where c.StatusRegister == CStatusRegister.Active
                           select new
                           {
                               c.Id,
                               c.Name,
                               c.IdAccount,
                               c.Idform,
                               AccountName = a.Name
                           };
            return consulta.ToList();
        }

        public bool ConsultarEstadoDeRutaDeEncuestadorXIDDevice(string IDdevice, string Nombrecuenta)
        {
            //   var consulta = Context.Campaigns.Include(t => t.Account).Where(c => c.StatusRegister == CStatusRegister.Active).ToList();
            var consultaDatosCuenta = Context.Accounts.Where(x => x.Name == Nombrecuenta);

            if (consultaDatosCuenta.Count() > 0)
            {

                var consulta = Context.Pollsters.Where(x => x.IMEI == IDdevice && x.idaccount == consultaDatosCuenta.First().Id);
                return consulta.Count() > 0 ? (consulta.First().StatusRoute == false ? true : false) : true;
            }
            else {

                return  true;
            }
        }
        /// <summary>
        /// Save data mobil from tracking
        /// </summary>
        /// <param name="_table">Data table Tracking</param>
        /// <returns></returns>
        public List<Branch> GetBranchList(int idAccount, string Imeid)
        {

            List<Branch> consulta = new List<Branch>();
            if (Imeid == "")
            {
                consulta = Context.Branches.Include(b => b.PersonOwner).Where(tb => tb.StatusRegister == CStatusRegister.Active && tb.IdAccount == idAccount && tb.ESTADOAGGREGATE.Equals("") == false && tb.ESTADOAGGREGATE != null).ToList();
            }
            else
            {
                   

                   var  consulta1 = from b in Context.Branches
                            join p in Context.Persons on b.IdPersonOwner equals p.Id
                            join pv in Context.Provinces on b.IdProvince equals pv.Id
                            join ds in Context.Districts on b.IdDistrict equals ds.Id
                            where b.IdAccount == idAccount && b.IMEI_ID.Contains(Imeid) && b.ESTADOAGGREGATE=="S"
                                  && b.StatusRegister==CStatusRegister.Active

                            select new Branch
                            {
                                Code = b.Code,
                                ExternalCode = b.ExternalCode,
                                Name = b.Name,
                                Label = b.Label,
                                CreationDate = b.CreationDate,
                                IdCountry = b.IdCountry,
                                IdProvince = b.IdProvince,
                                IdDistrict = b.IdDistrict,
                                IdParish = b.IdParish,
                                IdSector = b.IdSector,
                                Zone = b.Zone,
                                Neighborhood = b.Neighborhood,
                                MainStreet = b.MainStreet,
                                SecundaryStreet = b.SecundaryStreet,
                                NumberBranch = b.NumberBranch,
                                LatitudeBranch = b.LatitudeBranch,
                                LenghtBranch = b.LenghtBranch,
                                Reference = b.Reference,
                                IsAdministratorOwner = b.IsAdministratorOwner,
                                StatusRegister = b.StatusRegister,
                                TypeBusiness = b.TypeBusiness,
                                ESTADOAGGREGATE = b.ESTADOAGGREGATE,
                                RUTAAGGREGATE = b.RUTAAGGREGATE,
                                IMEI_ID = b.IMEI_ID,
                                routeDate = b.routeDate,
                                Cluster = b.Cluster,
                                state_period = b.state_period,
                                IdAccount = b.IdAccount,
                                Id = b.Id,
                                IdPersonOwner = b.IdPersonOwner,
                                CommentBranch = b.CommentBranch,
                                PersonOwner = p,
                                Province = pv,
                                District = ds

                            
                            };

                    consulta = consulta1.ToList();
                    //
                    //   consulta.Where(x => x.Id == item.Id).FirstOrDefault().Province = Context.Provinces.AsNoTracking().Where(x => x.Id == item.IdProvince).FirstOrDefault();


                    //   consulta.Where(x => x.Id == item.Id).FirstOrDefault().District = Context.Districts.AsNoTracking().Where(x => x.Id == item.IdDistrict).FirstOrDefault();

             




              
                return consulta;

            }
            return null;
        }
        public  List<Pollster> GetPollsterList(int idaccount)
        {
            var consulta = Context.Pollsters.Where(x => x.idaccount == idaccount).ToList();


            return consulta;
        }

        public Pollster GetPollsterById(int IdP)
        {
            var consulta = from c in Context.Pollsters
                           where c.Id == IdP
                           select c;
            return consulta.ToList().First();
        }

        public List<Pollster> GetPollsterListRoute(int idaccount , IList<string> imeis)
        {
            var consulta = Context.Pollsters.Where(x => x.idaccount == idaccount && !imeis.Contains(x.IMEI)).ToList();


            return consulta;
        }
        //public  List<Pollster> GetPollsterList(int idaccount)
        //{
        //    var consulta = Context.Pollsters.Where(x => x.idaccount == idaccount).ToList();


        //    return consulta;
        //}


        /// <summary>
        /// Obtiene las campañas permitidas por usuario
        /// </summary>
        /// <param name="User">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public List<Campaign> GetCampaing(Guid User)
        {
            try
            {
                var _table = from c in Context.Campaigns
                             join u in Context.UserCampaigns on c.Id equals u.idCanpaign
                             where u.idUser.Equals(User)
                             select c;

                return _table.Count() > 0 ? _table.ToList() : null;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// Obtiene las campañas permitidas por usuario
        /// </summary>
        /// <param name="Account">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public List<StatusTask> GetStatusTask(int Account)
        {
            try
            {
                var _table = from c in Context.Accounts
                             join us in Context.StatusTaskAccounts on c.Id equals us.Idaccount
                             join st in Context.StatusTasks on us.Idstatustask equals st.Id
                             where us.Idaccount.Equals(Account)
                             orderby us.ORDER ascending
                             select st;

                return _table.Count() > 0 ? _table.ToList() : null;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public Object GetActRoute(int idAccount)
        {

            try
            {

          
            IList<RouteBranch> _model = new List<RouteBranch>();

            var query = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE != "")
                        .Select(s => new { s.RUTAAGGREGATE, s.ESTADOAGGREGATE }).Distinct().OrderBy(x => x.RUTAAGGREGATE);
            var result = query.ToList();
            foreach (var item in result)
            {
                if (item != null)
                {

                    var List_data = _model.Where(x => x.route == item.RUTAAGGREGATE);
                    if (List_data.Count() > 0)
                    {
                        if (item.ESTADOAGGREGATE == "S")
                        {
                            List_data.First().status = true;
                        }
                        else
                        {
                            List_data.First().status = false;
                        }
                    }
                    else
                    {
                        if (item.ESTADOAGGREGATE == "S")
                        {
                            RouteBranch route = new RouteBranch();
                            _model.Add(new RouteBranch() { route = item.RUTAAGGREGATE, status = true });
                        }
                        else
                        {
                            RouteBranch route = new RouteBranch();
                            _model.Add(new RouteBranch() { route = item.RUTAAGGREGATE, status = false });

                        }
                    }
                }
            }
            var resulta = _model.Select(x=> new { x.route, x.status } ).ToList();
            return resulta;
            }
            catch (Exception e)
            {
                    
                return null;
            }
        }

        public IList<String> GetIMEIRoute(string routes, int idAccount)
        {

            var query = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE.Trim() == routes).Select(x => x.IMEI_ID).Distinct().ToList();
            //     result.upda
          
            return query;
        }
        public IList<Pollster> GetIdPersonByDocumentAndTypeDocumentAndAccount(IList<string> document, string typeDocument, int idAccount)
        {
            return Context.Pollsters
                .Where(p => document.Contains(p.IMEI) &&
                     p.Status == CStatusRegister.Active
                     && p.idaccount.Equals(idAccount)).ToList();

        }

        public bool UpdateStatusRoute(int idAccount, string route)
        {

            try
            {


                using (var transaction = Context.Database.BeginTransaction())
                {
                    var updatebranches = Context.Branches.Where(x => x.RUTAAGGREGATE.Trim() == route.Trim() && x.IdAccount == idAccount).ToList();
                    var Isactive = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE.Trim() == route && x.ESTADOAGGREGATE == "S").Select(x => x.Id).FirstOrDefault();
                    var estados = Isactive != 0? "" : "S";
                    updatebranches.ForEach(a => a.ESTADOAGGREGATE = estados);
                    Context.Branches.UpdateRange(updatebranches);
                    Context.SaveChanges();
                    transaction.Commit();
                }

            }
            catch (Exception e)
            {

                e.Message.ToString();
                return false;
            }


            return true;
        }
        public object GetCampaing_data()
        {
            //   var consulta = Context.Campaigns.Include(t => t.Account).Where(c => c.StatusRegister == CStatusRegister.Active).ToList();

            var consulta = from c in Context.Campaigns
                           join a in Context.Accounts on c.IdAccount equals a.Id
                           where c.StatusRegister == CStatusRegister.Active
                           select new {
                               c.Id,
                               c.Name,
                               c.IdAccount,
                               c.Idform,
                               AccountName= a.Name
                           };
            return consulta.ToList();
        }
    }
}
