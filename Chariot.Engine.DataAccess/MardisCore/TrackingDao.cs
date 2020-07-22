using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.MardiscoreViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Context.TrackingBranches.Where(x => x.IdPollster==_table.FirstOrDefault().IdPollster
                                                && x.datetime_tracking.Date == _table.First().datetime_tracking.Date).DeleteFromQuery();
                Context.BulkInsert(_table);
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
        public bool UpdateTrackingBranch(List<TrackingBranch> _table)
        {
            try
            {
                Context.TrackingBranches.Where(x => x.IdPollster == _table.FirstOrDefault().IdPollster
                                                && x.datetime_tracking.Date == _table.First().datetime_tracking.Date).DeleteFromQuery();
                Context.BulkInsert(_table);
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
        /// <summary>
        /// Get data Campaign
        /// </summary>
        /// <param name="NameCampaign">Name Campaign</param>
        /// <returns>Id pollster</returns>
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
        public List<TrackingBranch> GetBranchesbyIdPollster(int idcampaign , DateTime date, int idpollster)
        {
            try
            {
                var DataTable = Context.TrackingBranches.Where(x => x.Idcampaign == idcampaign && x.IdPollster == idpollster && x.datetime_tracking.Date == date.Date);
                return DataTable.Count() > 0 ? DataTable.ToList():null;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        /// <summary>
        /// Get data tracking by campaign of  supervisor
        /// </summary>
        /// <param name="idcampaign"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<PersonalTraker> GetTrackingbyIdCampaign(int idcampaign, DateTime date)
        {
            try
            {
          

                var DataTable = Context.PersonalTrakers.Where(x => x.Idcampaign == idcampaign && x.LastDate.Date == date.Date);
                return DataTable.Count() > 0 ? DataTable.ToList() : null;
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
               /// var entities = Context.TrackingBranches.AsNoTracking().ToList();
         


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
