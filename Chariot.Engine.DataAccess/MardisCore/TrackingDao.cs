
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.Procedure;
using Chariot.Framework.MardiscoreViewModel;
using Microsoft.EntityFrameworkCore;
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
                var Table = Context.PersonalTrakers;
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
            Context.TrackingBranches.Where(x => x.IdPollster == _table.FirstOrDefault().IdPollster
                                                && x.datetime_tracking.Date == _table.First().datetime_tracking.Date).DeleteFromQuery();
            try
            {
                foreach (var item in _table) {
                    item.AggregateUri = item.StatusBranch.ToUpper() == "S" || item.StatusBranch.ToUpper() == "PENTIENTE" ? item.AggregateUri : "Finalizado Ruta";
                    Context.TrackingBranches.Add(item);
                    Context.SaveChanges();
                }
                
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        /// <summary>
        /// Save data  mobil from new brach
        /// </summary>
        /// <param name="_table">Data table Tracking</param>
        /// <returns></returns>
        public bool SaveTrackingNewBranch(TrackingBranch _table)
        {
        
            try
            {

                     
                    Context.TrackingBranches.Add(_table);
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
        public bool UpdateTrackingBranch(TrackingBranch _table)
        {
            try
            {

                Context.TrackingBranches.Update(_table);
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
        /// <param name="IdDevice">Data table Tracking</param>
        /// <returns>Id pollster</returns>
        public int GetPollsterIdByIdDevice(string IdDevice)
        {
            try
            {


                var _dataTable = Context.Pollsters.Where(x => x.IMEI.Equals(IdDevice));
                return _dataTable.Count() > 0 ? _dataTable.First().Id : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        /// <summary>
        /// Save data mobil from tracking
        /// </summary>
        /// <param name="IdDevice">Data table Tracking</param>
        /// <returns>Id pollster</returns>
        public TrackingBranch GetBranchByCode(string Code, int idcampaign, DateTime date)
        {
            try
            {

                var _dataTable = Context.TrackingBranches.Where(x => x.CodeBranch.Equals(Code) && x.Idcampaign == idcampaign && x.datetime_tracking.Date == date.Date);
                return _dataTable.Count() > 0 ? _dataTable.First() : null;
            }
            catch (Exception e)
            {

                return null;
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
                var _dataTable = Context.Campaigns.Where(x => x.Name == NameCampaign);
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
        public List<TrackingBranch> GetBranchesbyIdPollster(int idcampaign, DateTime date, int idpollster)
        {
            try
            {
                var DataTable = Context.TrackingBranches.Where(x => x.Idcampaign == idcampaign && x.IdPollster == idpollster && x.datetime_tracking.Date == date.Date);
                return DataTable.Count() > 0 ? DataTable.ToList() : null;
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

                List<PersonalTraker> _result = new List<PersonalTraker>();
                var Pollters = Context.PersonalTrakers.Where(x => x.Idcampaign == idcampaign && x.LastDate.Date == date.Date).Select(s => s.IdPollster).Distinct();


                foreach (var item in Pollters) {
                    var max = Context.PersonalTrakers.Where(x => x.Idcampaign == idcampaign && x.LastDate.Date == date.Date && x.IdPollster == item).Max(x => x.LastDate);
                    var DataTable = Context.PersonalTrakers.Where(x => x.Idcampaign == idcampaign && x.LastDate == max && x.IdPollster == item).FirstOrDefault();

                    _result.Add(DataTable);
                }


                return _result.Count() > 0 ? _result.ToList() : null;
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
        public DateTime? GetStartDate(int idpollster , int Idcampaign, DateTime DateTrack)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();

                DateTime? StartN = Context.TrackingBranches.Where(x => x.Idcampaign == Idcampaign && x.IdPollster == idpollster && x.datetime_tracking.Date == DateTrack.Date).Select(x=>x.datetime_tracking).Min();
                DateTime Start = Convert.ToDateTime(StartN);
                return Start.AddHours(-5);
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
        public DateTime? GetEndDate(int idpollster, int Idcampaign, DateTime DateTrack)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();

                var EndN = Context.TrackingBranches.Where(x => x.Idcampaign == Idcampaign && x.IdPollster == idpollster && x.datetime_tracking.Date == DateTrack.Date).Select(x => x.ModificationDate).Max();
                DateTime End = Convert.ToDateTime(EndN);
                return End.AddHours(-5); 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public string GetPollsterPhoneById(int idpollster)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();

                var _dataTable = Context.Pollsters.Where(x => x.Id.Equals(idpollster));
                return _dataTable.Count() > 0 ? _dataTable.First().Phone : "Sin Identificar";
            }
            catch (Exception e)
            {

                return "Sin Identificar";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public List<SP_dato_tracking_encuestadores> GetSPTrackingByPollster(int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();

                var _dataTable = Context.Query<SP_dato_tracking_encuestadores>($@"EXEC dbo.sp_dato_tracking_encuestadores @fecha = '{date.Date}',  @idca = {campaign}   ");
                return _dataTable.Count() > 0 ? _dataTable.ToList(): null;
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
        public string GetPercentageDone(int idpollster, int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();


                var Done = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.IdPollster == idpollster && x.datetime_tracking.Date == date.Date & x.AggregateUri != null).Count();
                var All = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.IdPollster == idpollster && x.datetime_tracking.Date == date.Date).Count();
                var percentage = (Done * 100) / All;
                string Bateria = "Sin Ruta";
                if (percentage < 20)
                    Bateria = "0-20 %";
                if (percentage > 19 && percentage < (40))
                    Bateria = "21-40 %";
                if (percentage > 39 && percentage < (60))
                    Bateria = "41-60 %";
                if (percentage > 59 && percentage < (80))
                    Bateria = "61-80 %";
                if (percentage > (80))
                    Bateria = "81-100 %";

                return Bateria;
            }
            catch (Exception e)
            {

                return "Sin Ruta";
            }
        }
        #region Dashboard
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public int GetTotal_business(int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();



                var All = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.datetime_tracking.Date == date.Date);


                return All.Count() > 0 ? All.Count() : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public int GetFull_business(int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();



                var All = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.datetime_tracking.Date == date.Date && x.AggregateUri != null);


                return All.Count() > 0 ? All.Count() : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public int GetIncomplete_business(int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();



                var All = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.datetime_tracking.Date == date.Date && x.AggregateUri == null);


                return All.Count() > 0 ? All.Count() : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public int GetTotal_pollsters(int campaign, DateTime date)
        {
            try
            {
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();
                var All = Context.PersonalTrakers.Where(x => x.Idcampaign == campaign && x.LastDate.Date == date.Date);
                return All.Count() > 0 ? All.Select(s => s.IdPollster).Distinct().Count() : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idpollster">Data table Tracking</param>
        /// <returns>Name pollster</returns>
        public int GetActive_pollsters(int campaign, DateTime date)
        {
            try
            {
                var POLLTERS= Context.PersonalTrakers.Where(x => x.Idcampaign == campaign && x.LastDate.Date == date.Date);
                var numPollter = POLLTERS.Select(s => s.IdPollster.ToString()).Distinct().ToArray();
                var All = Context.TrackingBranches.Where(x => x.Idcampaign == campaign && x.datetime_tracking.Date == date.Date && numPollter.Contains(x.IdPollster.ToString()));
                return All.Count() > 0 ? All.Select(s=>s.IdPollster).Distinct().Count() : 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public StatusPollster GetPollsterStatus(int campaign, DateTime date)
        {
            try
            {
                StatusPollster statusPollster = new StatusPollster();

                 var All = from b in Context.TrackingBranches
                              join t in Context.PersonalTrakers on b.IdPollster equals t.IdPollster
                              where t.Idcampaign == campaign && t.LastDate.Date == date.Date
                              select t.IdPollster; 
                /// var entities = Context.TrackingBranches.AsNoTracking().ToList();
                foreach(var item in All.Distinct())
                {
                    string status = GetPercentageDone(item, campaign, date);
                    switch (status)
                    {
                        case "Retraso":
                            statusPollster.Delay = statusPollster.Delay+1;
                        break;
                        case "Medio":
                            statusPollster.Medium = statusPollster.Medium + 1;
                            break;
                        case "Normal":
                            statusPollster.Regular = statusPollster.Regular + 1;
                            break;
                    
                    }

                }             
                
                return statusPollster;
            }
            catch (Exception e)
            {

                return null;
            }
        }

     
        #endregion
    }
}
