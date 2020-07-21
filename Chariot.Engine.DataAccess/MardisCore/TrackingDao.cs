﻿using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.MardiscoreViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace Chariot.Engine.DataAccess.MardisCore
{
    public class TrackingDao : ADao
    {
        public TrackingDao(ChariotContext context)
      : base(context)
        {

        }

       /// <summary>
       /// Save data mobil from tracking
       /// </summary>
       /// <param name="_table">Data table Tracking</param>
       /// <returns></returns>
        public bool SaveTrackingbyIdDevice(PersonalTraker _table)
        {
            try
            {
               var Table=  Context.PersonalTrakers;
                Table.Add(_table);
                //Context.BulkInsert(Table);
                
                //Context.Entry(_table).State = StateInsert;
                Context.SaveChanges();

                 return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        /// <summary>
        /// Save data mobil from tracking
        /// </summary>
        /// <param name="_table">Data table Tracking</param>
        /// <returns></returns>
        public bool SaveTrackingBranch(List<TrackingBranch> _table)
        {
            try
            {
                //var Table = Context.TrackingBranches;
                //Table.AddRange(_table);
                //Context.BulkInsert(Table);
                using (var transaction = Context.Database.BeginTransaction())
                {
                    EntityFrameworkManager.ContextFactory = context => new CurrentContext();
                    Context.BulkInsert(_table);

                    transaction.Commit();
                }
                //Context.Entry(_table).State = StateInsert;
                //   Context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        /// <summary>
        /// Save data mobil from tracking
        /// </summary>
        /// <param name="IdDevice">Data table Tracking</param>
        /// <returns>Id pollster</returns>
        public int GetPollsterIdByIdDevice(string IdDevice)
        {
            try
            {

              
               var _dataTable=   Context.Pollsters.Where(x => x.IMEI.Equals(IdDevice));
               return _dataTable.Count()>0?_dataTable.First().Id:0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        public int GetCampaignIdByDescripcion(string NameCampaign)
        {
            try
            {
                var _dataTable = Context.Campaigns.Where(x => x.Name== NameCampaign);
                return _dataTable.Count() > 0 ? _dataTable.First().Id : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        /// <summary>
        /// Get data tracking by campaign of  supervisor
        /// </summary>
        /// <param name="idcampaign"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<PersonalTraker> GetTrackingbyIdCampaign(int idcampaign , DateTime date)
        {
            try
            {
                var DataTable = Context.PersonalTrakers.Where(x=>x.Idcampaign==idcampaign && x.LastDate.Date == date.Date);
                return DataTable.Count() > 0 ? DataTable.ToList():null;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public string GetPollsterNameById(int idpollster)
        {
            try
            {
                var _dataTable = Context.Pollsters.Where(x => x.Id.Equals(idpollster));
                return _dataTable.Count() > 0 ? _dataTable.First().Name : "Sin Identificar";
            }
            catch (Exception e)
            {

                return "Sin Identificar";
            }
        }

    }
}