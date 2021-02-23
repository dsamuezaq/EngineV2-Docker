using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.MardiscoreViewModel.Route;
using Chariot.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisCore
{
    public class RouteDao : ADao
    {
        public RouteDao(ChariotContext context)
      : base(context)
        {

        }


        /// <summary>
        /// Obtiene el registro de locales
        /// </summary>
        /// <param name="code"></param>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public Branch GetBranchbyCode(string code, int idAccount)
        {

            try
            {

                var consulta1 = from b in Context.Branches
                                join p in Context.Persons on b.IdPersonOwner equals p.Id
                                where b.Code == code && b.IdAccount == idAccount
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
                                    PersonOwner = p



                                };



                var _model = consulta1.ToList();


                if (_model.Count() > 0)
                {
                    return _model.First();

                }
                else
                {
                    return new Branch();

                }


            }
            catch (Exception e)
            {

                throw new Exception("En locales");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provice"></param>
        /// <param name="district"></param>
        /// <param name="parish"></param>
        /// <returns></returns>
        public ListParishModel GetIdUbicationByName(string provice, string district, string parish)
        {
            try
            {
                var itemReturn = from p in Context.Parishes
                                 join d in Context.Districts on p.IdDistrict equals d.Id
                                 join pv in Context.Provinces on d.IdProvince equals pv.Id
                                 where p.Name.Equals(parish) && d.Name.Equals(district) && pv.Name.Equals(provice)
                                 select new ListParishModel
                                 {
                                     Iddistrict = d.Id,
                                     Idparish = p.Id,
                                     idprovince = pv.Id
                                 };

                if (itemReturn.Count() > 0)
                {

                    return itemReturn.FirstOrDefault();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar ubicacion" + e.ToString());
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="IdAcccout"></param>
        /// <param name="idditrict"></param>
        /// <returns></returns>
        public bool UniqueCodebranch(string code, int IdAcccout, Guid idditrict)
        {
            try
            {
                var itemReturn = Context.Branches.Where(x => x.Code == code && x.IdAccount == IdAcccout).Select(t => t.IdDistrict);
                if (itemReturn.Count() < 1)
                {

                    return true;
                }
                if (itemReturn.Count() > 0)
                {
                    if (itemReturn.First().Equals(idditrict))
                    {
                        return true;
                    }

                }
                return false;
            }

            catch (Exception e)

            {
                throw new Exception("Error al consultar Sectores");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="idDistrict"></param>
        /// <returns></returns>
        public Guid GetSectorByName(string sector, Guid idDistrict)
        {
            try
            {
                var itemReturn = Context.Sectors.Where(x => x.Name == sector && x.IdDistrict == idDistrict);
                var idSectors = itemReturn.Count() > 0 ? itemReturn.First().Id : Guid.Parse("00000000-0000-0000-0000-000000000000");
                return idSectors;
            }

            catch (Exception e)

            {
                throw new Exception("Error al consultar Sectores" + e.Message);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchPerson"></param>
        /// <param name="idAccount"></param>
        /// <param name="iduser"></param>
        /// <param name="option"></param>
        /// <param name="campaign"></param>
        /// <param name="statusStask"></param>
        /// <param name="datestart"></param>
        /// <returns></returns>
        public string SaveBranchMigrate(Branch branchPerson, int idAccount, string iduser, int option, int campaign, int statusStask, DateTime datestart)
        {
            bool status = false;
            Branch branch = null;
            Person person = null;
#pragma warning disable CS0219 // La variable 'task' está asignada pero su valor nunca se usa
            TaskCampaign task = null;
#pragma warning restore CS0219 // La variable 'task' está asignada pero su valor nunca se usa
            try
            {


                //var rutas = branchPerson.Select(x => x.RUTAAGGREGATE).Distinct();




                if (branchPerson.Id != 0)
                {
                    Context.Branches.Update(branchPerson);
                    Context.SaveChanges();


                }
                //var _datainsert = branchPerson.Where(x => x.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
                if (branchPerson.Id == 0)
                {
                    Context.Branches.Add(branchPerson);
                    Context.SaveChanges();


                }

                if (option.Equals(2))
                {

                    TaskCampaign _modelTask = new TaskCampaign();

                    var IMEID = branchPerson.IMEI_ID.Split('-');
                    if (IMEID.Length > 0)
                    {
                        if (IMEID[0] != "0")
                        {
                            try
                            {
                                var idpollster = Context.Pollsters.Where(x => x.IMEI == IMEID[0]
                                                                     && (datestart >= x.Fecha_Inicio && datestart <= x.Fecha_Fin)
                                                                     && x.idaccount.ToString().ToUpper() == idAccount.ToString().ToUpper()
                                                                     && x.Status == CStatusRegister.Active
                                                                     );
                                _modelTask.idPollster = idpollster.FirstOrDefault().Id;
                            }
                            catch (Exception e)
                            {
                                return "No se encuentra registrado el encuestador";
                                throw;
                            }


                        }

                    }

                    _modelTask.IdAccount = idAccount;
                    _modelTask.IdBranch = branchPerson.Id;
                    _modelTask.IdCampaign = campaign;
                    _modelTask.IdMerchant = Guid.Parse(iduser);
                    _modelTask.IdStatusTask = statusStask;
                    _modelTask.Route = branchPerson.RUTAAGGREGATE;
                    _modelTask.DateCreation = DateTime.Now;
                    _modelTask.StartDate = datestart;
                    _modelTask.DateModification = DateTime.Now;
                    _modelTask.Code = branchPerson.Code;
                    _modelTask.ExternalCode = branchPerson.Code;
                    _modelTask.Description = "Tarea creada desde carga de rutas";
                    _modelTask.StatusMigrate = "M";
                    Context.TaskCampaigns.Add(_modelTask);
                    Context.SaveChanges();


                }

                return "";


            }
            catch (Exception e)
            {
                return "No puedo actualizar la información en la base de datos. Revise la Información";
                throw;
            }
            finally
            {
                status = true;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchPerson"></param>
        /// <param name="idAccount"></param>
        /// <param name="iduser"></param>
        /// <param name="option"></param>
        /// <param name="campaign"></param>
        /// <param name="statusStask"></param>
        /// <param name="datestart"></param>
        /// <returns></returns>
        public Branch GuardarlocalesCreadoAPPPedido(Branch branchPerson, int idAccount, string iduser, int option, int campaign, int statusStask, DateTime datestart)
        {
            bool status = false;
            Branch branch = null;
            Person person = null;
#pragma warning disable CS0219 // La variable 'task' está asignada pero su valor nunca se usa
            TaskCampaign task = null;
#pragma warning restore CS0219 // La variable 'task' está asignada pero su valor nunca se usa
            try
            {


                //var rutas = branchPerson.Select(x => x.RUTAAGGREGATE).Distinct();




                if (branchPerson.Id != 0)
                {
                    Context.Branches.Update(branchPerson);
                    Context.SaveChanges();


                }
                //var _datainsert = branchPerson.Where(x => x.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
                if (branchPerson.Id == 0)
                {
                    Context.Branches.Add(branchPerson);
                    Context.SaveChanges();


                }

                if (option.Equals(2))
                {

                    TaskCampaign _modelTask = new TaskCampaign();

                    var IMEID = branchPerson.IMEI_ID.Split('-');
                    if (IMEID.Length > 0)
                    {
                        if (IMEID[0] != "0")
                        {
                            try
                            {
                                var idpollster = Context.Pollsters.Where(x => x.IMEI == IMEID[0]
                                                                     && (datestart >= x.Fecha_Inicio && datestart <= x.Fecha_Fin)
                                                                     && x.idaccount.ToString().ToUpper() == idAccount.ToString().ToUpper()
                                                                     && x.Status == CStatusRegister.Active
                                                                     );
                                _modelTask.idPollster = idpollster.FirstOrDefault().Id;
                            }
                            catch (Exception e)
                            {
                                return null;
                                throw;
                            }


                        }

                    }

                    _modelTask.IdAccount = idAccount;
                    _modelTask.IdBranch = branchPerson.Id;
                    _modelTask.IdCampaign = campaign;
                    _modelTask.IdMerchant = Guid.Parse(iduser);
                    _modelTask.IdStatusTask = statusStask;
                    _modelTask.Route = branchPerson.RUTAAGGREGATE;
                    _modelTask.DateCreation = DateTime.Now;
                    _modelTask.StartDate = datestart;
                    _modelTask.DateModification = DateTime.Now;
                    _modelTask.Code = branchPerson.Code;
                    _modelTask.ExternalCode = branchPerson.Code;
                    _modelTask.Description = "Tarea creada desde carga de rutas";
                    _modelTask.StatusMigrate = "M";
                    Context.TaskCampaigns.Add(_modelTask);
                    Context.SaveChanges();


                }

                return branchPerson;


            }
            catch (Exception e)
            {
                return null;
                throw;
            }
            finally
            {
                status = true;
            }

        }
        public bool AddRouteImei(string document, string rout, int idAccount)
        {

            try
            {
                var routes = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE.Trim() == rout.Trim() && x.IMEI_ID != null).Select(x => x.IMEI_ID).Distinct().ToList();

                if (routes.Count() > 0)
                {
                    var route = routes.First();
                    var actuallyRoute = (route.Length > 5 || route != null) ? route + '-' + document : document;
                    var updatebranches = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE == rout).ToList();
                    updatebranches.ForEach(a => a.IMEI_ID = actuallyRoute);
                    Context.Branches.UpdateRange(updatebranches);
                    Context.SaveChanges();


                }
                else
                {
                    var actuallyRoute = document;
                    var updatebranches = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE == rout).ToList();
                    updatebranches.ForEach(a => a.IMEI_ID = actuallyRoute);
                    Context.Branches.UpdateRange(updatebranches);
                    Context.SaveChanges();

                }


                return true;
            }
            catch (Exception e)
            {

                return false;
            }




        }

        public bool UpdateRouteImei(string document, string routes, int idAccount)
        {

            try
            {
                var route = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE.Trim() == routes && x.IMEI_ID.Contains(document)).Select(x => x.IMEI_ID).Distinct();
                if (route.Count() > 0)
                {

                    var actuallyRoute = route.First().Replace("-" + document, "");
                    actuallyRoute = actuallyRoute.Replace(document + "-", "");
                    actuallyRoute = actuallyRoute.Replace(document, "");
                    var updatebranches = Context.Branches.Where(x => x.IdAccount == idAccount && x.RUTAAGGREGATE.Trim() == routes && x.IMEI_ID.Contains(document)).ToList();
                    updatebranches.ForEach(a => a.IMEI_ID = actuallyRoute);
                    Context.Branches.UpdateRange(updatebranches);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }




        }


    }
}
