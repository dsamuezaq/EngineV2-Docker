using AutoMapper;
using Chariot.Engine.DataAccess.MardisCore;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.MardiscoreViewModel.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.Mardiscore
{
    public class TaskCampaignBusiness : ABusiness
    {

        protected TaskCampaignDao _taskCampaignDao;
        public TaskCampaignBusiness(ChariotContext _chariotContext,
                                     RedisCache distributedCache,
                                     IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {

            _taskCampaignDao = new TaskCampaignDao(_chariotContext);
        }
        public List<BranchRutaTaskViewModel> GetBranches(int idaccount, string iddevice) {


           
            try
            {
                var _data = _taskCampaignDao.GetBranchList(idaccount, iddevice);
                var _model = _data.Select(x => new BranchRutaTaskViewModel
                {
                    Id = x.Id,
                    IdAccount = x.IdAccount,
                    ExternalCode = x.Code,
                    Code = x.Code,
                    Name = x.Name,
                    MainStreet = x.MainStreet,
                    Neighborhood = x.Neighborhood,
                    Reference = x.Reference,
                    Propietario = x.PersonOwner.Name,
                    IdProvince = x.IdProvince,
                    IdDistrict = x.IdDistrict,
                    IdParish = x.IdParish,
                    RUTAAGGREGATE = x.RUTAAGGREGATE,
                    IMEI_ID = x.IMEI_ID,
                    LatitudeBranch = x.LatitudeBranch,
                    LenghtBranch = x.LenghtBranch,
                    Celular = x.PersonOwner.Phone,
                    TypeBusiness = x.TypeBusiness,
                    Cedula = x.PersonOwner.Document,
                    ESTADOAGGREGATE = x.state_period == null ? "" : x.state_period,
                    comment = x.CommentBranch == null ? "" : x.CommentBranch,
                    // actividad = x.Branch_Activities.Select(t => t.idproject).ToList(),
                    Province = x.Province.Name,
                    District = x.District.Name,
                    //  FechaVisita = x.FechaVisita,
                    //Link = x.Ext_image,
                    //Isclient = x.Isclient,
                    Propietarioape = x.PersonOwner.SurName,
                    //correo = x.PersonOwner.mail,


                    //Contacts = x.Contacts.Select(t => new ViewContact
                    //{
                    //    NameContact = t.NameContact,
                    //    numberContact = t.numberContact,
                    //    positionContact = t.positionContact
                    //}).ToList(),
    


                }

                         );

                return _model.ToList();
            }
            catch (Exception e)
            {

              return null;
            }
           

        }
    }
}
