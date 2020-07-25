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
        public TrackingBusiness(ChariotContext _chariotContext,
                                     RedisCache distributedCache,
                                     IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {

            _trackingDao = new TrackingDao(_chariotContext);
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

            foreach (var  item in _data) {

                item.Idcampaign =_trackingDao.GetCampaignIdByDescripcion(item.campaign);
                item.idpollster = _trackingDao.GetPollsterIdByIdDevice(item.IdDevice);
                item.datetime_tracking = d;
                mapperTrackingBranch.Add(new TrackingBranch
                {
                    GeoLength = item.GeoLength,
                    Geolatitude = item.Geolatitude,
                    datetime_tracking = item.datetime_tracking,
                    CodeBranch = item.CodeBranch,
                    NameBranch = item.NameBranch,
                    StreetBranch = item.StreetBranch,
                    StatusBranch = item.StatusBranch,
                    RouteBranch = item.RouteBranch,
                    IdPollster = item.idpollster,
                    Idcampaign = item.Idcampaign
                        
                });
            }
            
            var Status=_trackingDao.SaveTrackingBranch(mapperTrackingBranch);

            return Status;
        }

        public bool SaveTrackingBranchStatus(StatusBranchTrackingViewModel _data)
        {

            DateTime d = DateTime.Now;
            TrackingBranch mapperTrackingBranch = new TrackingBranch();
            int idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.campaign);
            TrackingBranch _table = _trackingDao.GetBranchByCode(_data.Code, idcampaign, d);


            _table.AggregateUri = _data.AggregateUri;
            _table.StatusBranch =_data.Status;
            _table.timeTask =_data.TimeTask;

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

                 }).ToList();

                reply.messege = "success";
                reply.data = _Reply;
                reply.status = "Ok";
            }
         
            return reply;
        }
        public ReplyViewModel GetTracking(GetTrackingViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
               var _dataTable = _trackingDao.GetTrackingbyIdCampaign(_data.Idcampaign, _data.DateTracking);

            List<TrackingModelReply> _Reply = 
                _dataTable.Select(x => new TrackingModelReply {
                                                                latitud=x.Geolatitude,
                                                                longitud=x.GeoLength,
                                                                first_name=_trackingDao.GetPollsterNameById(x.IdPollster),
                                                                last_name="",
                                                                estado=_trackingDao.GetPercentageDone(x.IdPollster,x.Idcampaign, _data.DateTracking),
                                                                Idpollster=x.IdPollster,
                                                                bateria=x.battery_level==null?0: x.battery_level,
                                                              }).ToList();

            reply.messege = "success";
            reply.data = _Reply;
            reply.status = "Ok";
            return reply;
        }

        public ReplyViewModel GetDashboard(GetTrackingViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            TrackingDashboardModelReply _Reply = new TrackingDashboardModelReply();
            var _statusPollter=_trackingDao.GetPollsterStatus(_data.Idcampaign, _data.DateTracking);
            _Reply.Total_business = _trackingDao.GetTotal_business(_data.Idcampaign, _data.DateTracking);
            _Reply.Full = _trackingDao.GetFull_business(_data.Idcampaign, _data.DateTracking);
            _Reply.Incompletes = _trackingDao.GetIncomplete_business(_data.Idcampaign, _data.DateTracking);
            _Reply.Total_pollsters = _trackingDao.GetTotal_pollsters(_data.Idcampaign, _data.DateTracking);
            _Reply.Active_pollsters = _trackingDao.GetActive_pollsters(_data.Idcampaign, _data.DateTracking);
            _Reply.Delay = _statusPollter.Delay;
            _Reply.Medium = _statusPollter.Medium;
            _Reply.Regular = _statusPollter.Regular;
            reply.messege = "success";
            reply.data = _Reply;
            reply.status = "Ok";
            return reply;
        }
        #endregion
        #region Method Private
        #endregion


    }
}
