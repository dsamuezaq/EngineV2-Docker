﻿using AutoMapper;
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
        public ReplyViewModel SaveTracking(TrackingViewModel _data) {

            DateTime d = DateTime.Now;
            PersonalTraker mapperTracking = _mapper.Map<PersonalTraker>(_data);
            mapperTracking.IdPollster = _trackingDao.GetPollsterIdByIdDevice(_data.IdDevice);
            mapperTracking.Idcampaign = _trackingDao.GetCampaignIdByDescripcion(_data.Namecampaign);
            mapperTracking.LastDate = d;
            _trackingDao.SaveTrackingbyIdDevice(mapperTracking);
            return null;
        }
        public ReplyViewModel SaveTrackingBranch(List<TrackingBranchViewModel> _data)
        {

            DateTime d = DateTime.Now;
            List<TrackingBranch> mapperTrackingBranch= new List<TrackingBranch> ();
            //_data.AsParallel()
            //      .ForAll(s =>
            //      {
            //          s.Idcampaign = _trackingDao.GetPollsterIdByIdDevice(s.campaign);
            //          s.idpollster = _trackingDao.GetPollsterIdByIdDevice(s.IdDevice);
            //          s.datetime_tracking = d;

            //      });

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
            
             _trackingDao.SaveTrackingBranch(mapperTrackingBranch);
            return null;
        }
        public ReplyViewModel GetTracking(GetTrackingViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
               var _dataTable = _trackingDao.GetTrackingbyIdCampaign(_data.Idcampaing, _data.DateTracking);

            List<TrackingModelReply> _Reply = 
                _dataTable.Select(x => new TrackingModelReply {
                                                                latitud=x.Geolatitude,
                                                                longitud=x.GeoLength,
                                                                first_name=_trackingDao.GetPollsterNameById(x.IdPollster),
                                                                last_name="",
                                                                bateria=50,
                                                                Idpollster=x.IdPollster
                                                              }).ToList();

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
