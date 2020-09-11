using Chariot.Engine.DataObject;
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
                            where b.IdAccount == 4 && b.IMEI_ID.Contains("359944074291986")
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
        public void d()
        {
         

        }
    }
}
