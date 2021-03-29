using AutoMapper;
using Chariot.Engine.DataAccess.MardisCore;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.Complement;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.SystemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.Mardiscore
{
    public class TrackingBusiness : ABusiness
    {
        protected TrackingDao _trackingDao;
        protected TaskCampaignDao _taskCampaignDao;
        public TrackingBusiness(ChariotContext _chariotContext,
                                     RedisCache distributedCache,
                                     IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {

            _trackingDao = new TrackingDao(_chariotContext);
            _taskCampaignDao= new TaskCampaignDao(_chariotContext);
        }

        #region Method Public
        public bool SaveTracking(TrackingViewModel _data) {

            DateTime d = DateTime.Now;
            PersonalTraker mapperTracking = _mapper.Map<PersonalTraker>(_data);
            mapperTracking.IdPollster = _trackingDao.GetPollsterIdByIdDevice(_data.IdDevice);
            mapperTracking.Idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.Namecampaign);
            mapperTracking.LastDate = d;
            
            return _trackingDao.SaveTrackingbyIdDevice(mapperTracking); 
        }
        public bool SaveTrackingBranch(List<TrackingBranchViewModel> _data)
        {

            DateTime d = DateTime.Now;
            List<TrackingBranch> mapperTrackingBranch= new List<TrackingBranch> ();
            int Idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.First().campaign);
            int idpollster = _trackingDao.GetPollsterIdByIdDevice(_data.First().IdDevice);
          
            foreach (var  item in _data) {
                DateTime? _StartDate = null;
                DateTime? _EndDate = null;
                if (item.dateexec != null) {
                    _StartDate =  DateTime.Parse(item.dateexecini);
                }
                if (item.dateexecini != null)
                {
                    _EndDate = DateTime.Parse(item.dateexec);
                }
                mapperTrackingBranch.Add(new TrackingBranch
                {
                    GeoLength = item.GeoLength,
                    Geolatitude = item.Geolatitude,
                    datetime_tracking = d,
                    CodeBranch = item.CodeBranch,
                    NameBranch = item.NameBranch,
                    StreetBranch = item.StreetBranch,
                    StatusBranch = item.StatusBranch,
                    RouteBranch = item.RouteBranch,
                    IdPollster = idpollster,
                    Idcampaign = Idcampaign,
                    timeTask=item.timetaks
                   ,ModificationDate= d
                   ,Start= _StartDate,
                    End= _EndDate


                });;;
            }
            
            var Status=_trackingDao.SaveTrackingBranch(mapperTrackingBranch);

            return Status;
        }
        public bool SaveTrackingNewBranch(TrackingNewBranchViewModel _data)
        {

            DateTime d = DateTime.Now;
            TrackingBranch mapperTrackingBranch = new TrackingBranch();
            int Idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.campaign);
            int idpollster = _trackingDao.GetPollsterIdByIdDevice(_data.IdDevice);

         
                DateTime? _StartDate = null;
                DateTime? _EndDate = null;
                if (_data.Start != null)
                {
                    _StartDate = DateTime.ParseExact(_data.Start, "dd/MM/yy HH:mm", null);
            }
                if (_data.End != null)
                {
                    _EndDate = DateTime.ParseExact(_data.End, "dd/MM/yy HH:mm", null); 
                }

                     mapperTrackingBranch.GeoLength = _data.GeoLength;
                     mapperTrackingBranch.Geolatitude = _data.Geolatitude;
                     mapperTrackingBranch.datetime_tracking = d;
                     mapperTrackingBranch.CodeBranch = _data.CodeBranch;
                     mapperTrackingBranch.NameBranch = _data.NameBranch;
                     mapperTrackingBranch.StreetBranch = _data.StreetBranch;
                     mapperTrackingBranch.StatusBranch = _data.StatusBranch;
                     mapperTrackingBranch.RouteBranch = _data.RouteBranch;
                     mapperTrackingBranch.IdPollster = idpollster;
                     mapperTrackingBranch.Idcampaign = Idcampaign;
                     mapperTrackingBranch.timeTask = _data.timetaks;
                     mapperTrackingBranch.ModificationDate = d;
                     mapperTrackingBranch.Start = _StartDate;
                     mapperTrackingBranch.End = _EndDate;
                      mapperTrackingBranch.AggregateUri = _data.AggregateUri;


            var Status = _trackingDao.SaveTrackingNewBranch(mapperTrackingBranch);

            return Status;
        }
        public bool SaveTrackingBranchStatus(StatusBranchTrackingViewModel _data)
        {

            DateTime d = DateTime.Now;
            TrackingBranch mapperTrackingBranch = new TrackingBranch();
            int idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.campaign);
            TrackingBranch _table = _trackingDao.GetBranchByCode(_data.Code, idcampaign, d,_data.IdDevice);

            DateTime? _StartDate = null;
            DateTime? _EndDate = null;
            if (_data.Start != null)
            {
                _StartDate = DateTime.ParseExact(_data.Start, "dd/MM/yy HH:mm",null);
            }
            if (_data.End != null)
            {
                _EndDate = DateTime.ParseExact(_data.End, "dd/MM/yy HH:mm", null);
            }
            _table.AggregateUri = _data.AggregateUri;
            _table.StatusBranch =_data.Status;
            _table.timeTask =_data.TimeTask;
            _table.Start = _StartDate;
            _table.End = _EndDate;


            _table.ModificationDate = d;
            var Status = _trackingDao.UpdateTrackingBranch(_table);

            return Status;
        }

        public ReplyViewModel GetBranches(GetBranchViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            reply.messege = "Don't demand data";
            reply.status = "Fail";
            var _dataTable = _trackingDao.GetBranchesbyIdPollster(_data.Idcampaign, _data.DateTracking,_data.Idpollster);
            if (_dataTable.Count() > 0) {
                List<BranchesModelReply> _Reply =
                 _dataTable.Select(x => new BranchesModelReply
                 {
                     GeoLength = x.GeoLength,
                     Geolatitude = x.Geolatitude,
                     TimeTask = x.timeTask,
                     CodeBranch = x.CodeBranch,
                     NameBranch = x.NameBranch,
                     StreetBranch = x.StreetBranch,
                     StatusBranch = x.StatusBranch,
                     RouteBranch = x.RouteBranch,
                     Status = x.AggregateUri == null ? "Pendiente" : "Finalizado"
                     ,Start=x.Start
                     ,End=x.End
                     ,uri=x.AggregateUri
                     
                 }).ToList();
                if (_data.Status == "" || _data.Status == null)
                {
                    reply.messege = "success";
                    reply.data = _Reply.OrderByDescending(c => c.Status).ThenByDescending(x=>x.End);
                    reply.status = "Ok";
                }
                else {
                    reply.messege = "success";
                    reply.data = _Reply.Where(x=>x.Status== _data.Status).OrderBy(t => t.TimeTask); ;
                    reply.status = "Ok";
                }
              
            }
         
            return reply;
        }

        public ReplyViewModel GetTracking(GetTrackingViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            //   var _dataTable = _trackingDao.GetTrackingbyIdCampaign(_data.Idcampaign, _data.DateTracking);
            var _Reply = _trackingDao.GetSPTrackingByPollster(_data.Idcampaign, _data.DateTracking,_data.Iduser);
            //List<TrackingModelReply> _Reply = 
            //    _dataTable.Select(x => new TrackingModelReply {
            //                                                    latitud=x.Geolatitude,
            //                                                    longitud=x.GeoLength,
            //                                                    first_name=_trackingDao.GetPollsterNameById(x.IdPollster),
            //                                                    last_name="",
            //                                                    estado=_trackingDao.GetPercentageDone(x.IdPollster,x.Idcampaign, _data.DateTracking),
            //                                                    Idpollster=x.IdPollster,
            //                                                    bateria=x.battery_level==null?0: x.battery_level,
            //                                                    Ultima_conexion=x.LastDate.AddHours(-5),
            //                                                    Inicio=_trackingDao.GetStartDate(x.IdPollster, x.Idcampaign, _data.DateTracking),
            //                                                    Fin = _trackingDao.GetEndDate(x.IdPollster, x.Idcampaign, _data.DateTracking),
            //                                                    Telefono=_trackingDao.GetPollsterPhoneById(x.IdPollster)

            //    }).ToList();


            if (_data.Status == "" || _data.Status==null)
            {

                reply.messege = "success";
                reply.data = _Reply.OrderBy(u =>u.estado);
                reply.status = "Ok";
                return reply;
            }
            else {
                reply.messege = "success";
                reply.data = _Reply.Where(x=>x.estado==_data.Status);
                reply.status = "Ok";
                return reply;

            }


          
        }

        public ReplyViewModel GetDashboard(GetTrackingViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            TrackingDashboardModelReply _Reply = new TrackingDashboardModelReply();
            var _statusPollter=_trackingDao.GetPollsterStatus(_data.Idcampaign, _data.DateTracking,_data.Iduser);
            _Reply.Total_business = _trackingDao.GetTotal_business(_data.Idcampaign, _data.DateTracking, _data.Iduser);
            _Reply.Full = _trackingDao.GetFull_business(_data.Idcampaign, _data.DateTracking, _data.Iduser);
            _Reply.Incompletes = _trackingDao.GetIncomplete_business(_data.Idcampaign, _data.DateTracking, _data.Iduser);
            _Reply.Total_pollsters = _trackingDao.GetTotal_pollsters(_data.Idcampaign, _data.DateTracking, _data.Iduser);
            _Reply.Active_pollsters = _trackingDao.GetActive_pollsters(_data.Idcampaign, _data.DateTracking, _data.Iduser);
            _Reply.Delay = _statusPollter.Delay;
            _Reply.Medium = _statusPollter.Medium;
            _Reply.Regular = _statusPollter.Regular;
            reply.messege = "success";
            reply.data = _Reply;
            reply.status = "Ok";
            return reply;
        }

        public ReplyViewModel GetCampaign(GetCampaignViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var _Reply = _taskCampaignDao.GetCampaing(_data.Iduser).Select(x => new CampaignModelReply
                {
                 Id = x.Id,
                 Name = x.Name
                }).ToList(); ;
                reply.messege = "success";
                reply.data = _Reply;
                reply.status = "Ok";
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos con esa Campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;
               
            }   
        }

        public ReplyViewModel ObtenerEstadoDeActivacionDeDescargaDeRutas(String IdDevice, string Nombrecuenta)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var _Reply = _taskCampaignDao.ConsultarEstadoDeRutaDeEncuestadorXIDDevice(IdDevice , Nombrecuenta);
               
                reply.messege = "success";
                reply.data = _Reply;
                reply.status = "Ok";
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos con esa Campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }
        #endregion
        #region Method Private
        #endregion


    }
}
