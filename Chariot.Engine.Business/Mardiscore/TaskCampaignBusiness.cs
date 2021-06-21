using AutoMapper;
using Chariot.Engine.Business.ClientRest;
using Chariot.Engine.DataAccess.MardisCore;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Framework.CampaingViewModels;
using Chariot.Framework.Complement;
using Chariot.Framework.MardisBranchViewModel;
using Chariot.Framework.MardisClientRestModel;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardiscoreViewModel.Branch;
using Chariot.Framework.MardiscoreViewModel.Route;
using Chariot.Framework.Resources;
using Chariot.Framework.SurtiApp.CargaStock;
using Chariot.Framework.SystemViewModel;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chariot.Engine.Business.Mardiscore
{
    public class TaskCampaignBusiness : ABusiness
    {
        protected HelpersHttpClientBussiness _helpersHttpClientBussiness = new HelpersHttpClientBussiness();
        protected TaskCampaignDao _taskCampaignDao;
        protected RouteDao _routeDao;
        protected BranchMigrateDao _branchMigrateDao;
        public TaskCampaignBusiness(ChariotContext _chariotContext,
                                     RedisCache distributedCache,
                                     IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {
            _branchMigrateDao = new BranchMigrateDao(_chariotContext);
            _taskCampaignDao = new TaskCampaignDao(_chariotContext);
            _routeDao = new RouteDao(_chariotContext);
        }

        public object GetCampanigAccount(int idaccount)
        {
            return _taskCampaignDao.GetCampaing(idaccount); 
        }

        public object GetAllMovilWarenhouse()
        {
            return _taskCampaignDao.GetAllMovilWarenhouseDao();
        }

        public List<ParameterRoute> ObtenerParametrosRutas(string idusuario, string ruta, int cuenta)
        {
            return _taskCampaignDao.ObtenerParametrosRutas(idusuario, ruta, cuenta);
        }
        public List<BranchRutaTaskViewModel> GetBranches(int idaccount, string iddevice) {



            try
            {

                var clienteCupo = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaClienteCupo>("CoberturaClienteCupo/obtener");
                });

                var _data = _taskCampaignDao.GetBranchList(idaccount, iddevice);
                clienteCupo.Wait();
                var result = clienteCupo.Result.Result;
                var _model = _data.Select(x => new BranchRutaTaskViewModel
                {
                    Id = x.Id,
                    IdAccount = x.IdAccount,
                    ExternalCode = x.ExternalCode,
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
                  //  comment = CupoCliente(x.Code),
                   comment= CupoCliente(result, x.Code),
                    // actividad = x.Branch_Activities.Select(t => t.idproject).ToList(),
                    Province = x.Province.Name,
                    District = x.District.Name,
                    //  FechaVisita = x.FechaVisita,
                    //Link = x.Ext_image,
                    isclient = x.isclient,
                    Propietarioape = x.PersonOwner.Name  ,
                    ZonaPeliograsa=x.Zone,
                    ActualizoGeo=x.geoupdate,
                    correo = x.PersonOwner.mail


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

        private string CupoCliente(List<GetCoberturaClienteCupo> resultado, string cliente)
        {
            var data = resultado.Where(x => x.cU_ID.ToString() == cliente);
            if (data.Count() > 0) {
                return data.FirstOrDefault().cU_CUPO + "-" + data.FirstOrDefault().cU_DIAS_CREDITO;
            }

            return "No registra Informacion";
        }


        public string ActualizaGeo(string lat, string lon, int idbranch) {





            return _taskCampaignDao.GuardarGeos(idbranch, lat, lon);
        }
        private string CupoClienteActual(string cliente)
        {
            cliente = "28257";
            var clienteCupo = Task.Factory.StartNew(() => {
                return _helpersHttpClientBussiness.GetApi<GetCoberturaClienteCupo>("CoberturaClienteCupo/obtenerxcodigo?codigo=" + cliente);
            });


            var cupo = clienteCupo.Result.Result;

            if (cupo.Count() > 0)
            {
                return cupo.FirstOrDefault().cU_CUPO + "-" + cupo.FirstOrDefault().cU_DIAS_CREDITO;


            }

            return "No registra Informacion";
        }

        public ReplyViewModel UpdatePollsterSupervisor(UpdatePollsterSupervisor _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                Pollster itemPollster = _taskCampaignDao.GetPollsterById(_data.pollster.Idpollster);
                itemPollster.StatusRoute = _data.pollster.statusRoute;
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                _taskCampaignDao.InsertUpdateOrDelete(itemPollster, _data.transaction);
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }


        }
        public List<MapaLocalModel> ObtenerLocalesConGeo(int cuenta)
        {
            try
            {
                var locales = _taskCampaignDao.ConsulataLocales(cuenta).Select(x => new MapaLocalModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    Region = "da",
                    type = x.TypeBusiness
       ,
                    SpeedLimit = -1
       ,
                    lat = double.Parse(x.LatitudeBranch),
                    Lng = double.Parse(x.LenghtBranch),

                }).ToList();

                return locales;
            }
            catch (Exception e)
            {

                throw;

                return null;
            }
      


        }
        public ReplyViewModel SavePollster(TransactionPollsterViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                _taskCampaignDao.InsertUpdateOrDelete(_data.pollster, _data.transaction);
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }
         
         
        }

        public IList<FactNutriViewModel> GetListFactLoad(string fileBrachMassive)
        {
            try
            {
                IList<FactNutriViewModel> arreglo = new List<FactNutriViewModel>();
                ////autorizacion-->estado
                ///autorizacion-->fecha
                ///autorizacion-->comprobante
                //////autorizacion-->comprobante-->infotrubutaria
                ///mas o menos asies
                XmlDocument doc = new XmlDocument();
                XmlDocument doc1 = new XmlDocument();

                //Creacion texto en memoria
                StringBuilder constructor = new StringBuilder();
                constructor.AppendLine(fileBrachMassive);

                //Persistencia
                string pathTxt = "C://xmls//xml_ejemplo.xml";
                File.WriteAllText(pathTxt, constructor.ToString());

                doc.Load("C://xmls//xml_ejemplo.xml");//Leer el XML
                var cultureInfo = new CultureInfo("en-US");
                XmlNode node = doc.DocumentElement.SelectSingleNode(@"/autorizacion/comprobante");
                XmlNode childNode = node.ChildNodes[0];

                if (childNode is XmlCDataSection)
                {

                    XmlCDataSection cdataSection = childNode as XmlCDataSection;
                    doc1.LoadXml(cdataSection.Data);

                    XmlNode xcabecera = doc1.DocumentElement.SelectSingleNode(@"/factura/infoFactura");
                    var date = xcabecera.ChildNodes[0].InnerText;

                    var datfact = DateTime.UtcNow;

                    XmlNode xcabeceraFact = doc1.DocumentElement.SelectSingleNode(@"/factura/infoTributaria");
                    var numfact = xcabeceraFact.ChildNodes[9].InnerText;
                    var razSocial = xcabecera.ChildNodes[5].InnerText;
                    var ruc = xcabecera.ChildNodes[6].InnerText;

                    XmlNodeList xPersonas1 = doc1.GetElementsByTagName("detalle");


                    foreach (XmlNode item in xPersonas1)
                    {
                        FactNutriViewModel fact = new FactNutriViewModel();
                        fact.ruc = ruc;
                        fact.razonsocial = razSocial;
                        fact.numFact = numfact;
                        fact.dateFact = datfact;
                        fact.bardcode = item.ChildNodes[1].InnerText;


                        fact.code = item.ChildNodes[0].InnerText;
                        if (item.ChildNodes[2].Name == "cantidad")
                        {

                            fact.cant = float.Parse(item.ChildNodes[2].InnerText.Replace(".000000", ""));
                        }

                        if (item.ChildNodes[3].Name == "cantidad")
                        {
                            fact.cant = float.Parse(item.ChildNodes[3].InnerText.Replace(".000000", ""));

                        }

                        arreglo.Add(fact);
                    }
                }
                return arreglo;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public object GetPollster(int Idaccount)
        {
            ReplyViewModelPollsters reply = new ReplyViewModelPollsters();
            try
            {

                reply.messege = "Listado de mercaderistas por cuenta";
                reply.status = "Ok";
                reply.data= _taskCampaignDao.GetPollsterList(Idaccount);
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos de encuestador en la cuenta";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }


        }


        public object GetPollsterActive(int Idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var _reply= _taskCampaignDao.GetPollsterList(Idaccount)
                            .Where(x => x.Status == CStatusRegister.Active)
                            .Select(x => new {
                                x.Id,
                                x.IMEI,
                                x.Phone
                            })
                            .ToList();
                reply.messege = "Listado de mercaderistas por cuenta";
                reply.status = "Ok";
                reply.data = _reply;
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos de encuestador en la cuenta";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }


        }

        public object GetPollsterActiveRoute(int idAccount, string route)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var imei = _taskCampaignDao.GetIMEIRoute(route, idAccount);
                IList<string> encuestadores = new List<string>();
                string[] separadas;

                foreach (var person in imei)
                {
                    if (person != null)
                    {
                        string UniqueImei = person;
                        separadas = UniqueImei.Split('-');
                        for (int xi = 0; xi < separadas.Length; xi++)
                        {

                            encuestadores.Add(separadas[xi]);
                        }
                    }
                }

                var _reply = _taskCampaignDao.GetPollsterListRoute(idAccount,imei)
                            .Where(x => x.Status == CStatusRegister.Active)
                            .Select(x => new {
                                x.Id,
                                x.IMEI,
                                x.Phone
                            })
                            .ToList();
                reply.messege = "Listado de mercaderistas por cuenta";
                reply.status = "Ok";
                reply.data = _reply;
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos de encuestador en la cuenta";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }


        }
        public object SaveRouteTask(GetListbranchViewModel _respose)
        {

            ReplyViewModel reply = new ReplyViewModel();


            var item = _respose._route;
            Branch BranchModel = new Branch();
            BranchModel = _routeDao.GetBranchbyCode(item.Codigo_Encuesta, _respose.account);
            var _data = _routeDao.GetIdUbicationByName(item.Provincia, item.Canton, item.Parroquia);
            if (_data == null)
            {
                reply.status = "error";
                reply.messege = "La informacion de Provicia o Cuidad o Parrioquia no existe en la Base de datos. Consulte los Catologos";
                _respose._route.Errores = "La informacion de Provicia o Cuidad o Parrioquia no existe en la Base de datos. Consulte los Catologos";
                reply.data = _respose._route;
                return reply;
            }
            BranchModel.IdProvince = _data.idprovince;
            BranchModel.IdDistrict = _data.Iddistrict;
            BranchModel.IdParish = _data.Idparish;
            var valcodigo = ValidoCodigo(item.Codigo_Encuesta, _respose.account, _data.Iddistrict, 1);
            if (valcodigo != "")
            {
                reply.status = "error";
                reply.messege = valcodigo;
                _respose._route.Errores = valcodigo;
                reply.data = _respose._route;

                return reply;
            }

            BranchModel.routeDate = DateTime.Now;
            if (BranchModel.Id == 0)
            {
                BranchModel.Code = item.Codigo_Encuesta;
                BranchModel.ExternalCode = item.PT_indice;
                BranchModel.PersonOwner.Code = item.Codigo_Encuesta;
                BranchModel.IdAccount = _respose.account;
                BranchModel.PersonOwner.IdAccount = _respose.account;
                BranchModel.IdCountry = Guid.Parse("BE7CF5FF-296B-464D-82FA-EF0B4F48721B");// Pais ecuador
                BranchModel.IsAdministratorOwner = "SI";
                BranchModel.Zone = "-";
                BranchModel.Neighborhood = "-";

                BranchModel.NumberBranch = "-";
                BranchModel.SecundaryStreet = "-";
                BranchModel.StatusRegister = "A";
                BranchModel.PersonOwner.SurName = "-";
                BranchModel.PersonOwner.TypeDocument = "CI";
                BranchModel.PersonOwner.StatusRegister = "A";
                BranchModel.IdSector = _routeDao.GetSectorByName("CENTRO", BranchModel.IdDistrict);

                //if (_branchMigrateDao.ValTypeBussiness(item.Tipo.ToUpper().TrimEnd().TrimStart(), Guid.Parse(campaign)))
                //{
                //    BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
                //}
                //else
                //{
                //    error = "Tipo de negocio no permitido";
                //    break;
                //}


            }
            BranchModel.PersonOwner.StatusRegister = "A";
            BranchModel.StatusRegister = "A";
            BranchModel.ESTADOAGGREGATE = "S";
            BranchModel.state_period = "S";

            BranchModel.ExternalCode = item.PT_indice;
            BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
            BranchModel.Name = item.local;
            BranchModel.MainStreet = item.Dirección;
            BranchModel.Reference = item.Referencia;
            BranchModel.PersonOwner.Name = item.Nombres;
            BranchModel.PersonOwner.SurName = item.Apellidos;
            BranchModel.PersonOwner.mail = item.Mail;
            BranchModel.PersonOwner.Document = item.Cédula;
            BranchModel.PersonOwner.Mobile = item.Celular;
            BranchModel.PersonOwner.Phone = item.Telefono;
            BranchModel.Label = item.local;
            var latitud = item.Latitud;
            var lat = latitud.Contains("E") ? ExponecialToString(latitud) : latitud;
            BranchModel.LatitudeBranch = lat.Length <= 10 ? lat : lat.Substring(0, 11);
            var longitud = item.Longitud;
            string len = longitud.Contains("E") ? ExponecialToString(longitud) : longitud;
            BranchModel.LenghtBranch = len.Length <= 10 ? len : len.Substring(0, 11);
            BranchModel.Cluster = item.CLUSTER;
            BranchModel.RUTAAGGREGATE = item.RUTA;
            BranchModel.IMEI_ID = item.IMEI;

            BranchModel.CommentBranch = item.Estado;
            BranchModel.Zone = item.Zona_Peligrosa;
            var date = DateTime.Now;
            if (_respose.option == 2)
            {
                try
                {
                    date =  DateTime.ParseExact(item.Fecha, "dd-MM-yyyy", null);
                }
                catch (Exception e)
                {

                    reply.status = "error";
                    reply.messege = "El campo fecha no tienen un format";
                    _respose._route.Errores = "El campo fecha no tienen un format (dd-MM-yy)";
                    reply.data = _respose._route;


                    return reply;
                    throw new Exception("Error al consultar Sectores");
                }
            }
            string resp = _routeDao.SaveBranchMigrate(BranchModel, _respose.account, _respose.iduser, _respose.option, _respose.campaign, _respose.status, date);
            if (resp == "")
            {

                reply.status = "Ok";
                reply.messege = "Local Guardado";
                return reply;
            };
            reply.status = "error";
            reply.messege = resp;
            _respose._route.Errores = resp;
            reply.data = _respose._route;
            return reply;

        }
        public object SaveImei(int idaccount, string id, string route)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var _reply = _routeDao.AddRouteImei(id, route, idaccount);
                if (_reply) {

                    reply.messege = "Guardado correctamente";
                    reply.status = "Ok";
                    // reply.data = _reply;
                    return reply;
                }
                
                else {

                    reply.messege = "No se guardo correctamente";
                    reply.status = "Fail";
                    // reply.data = _reply;
                    return reply;
                } 
             
            }
            catch (Exception e)
            {

                reply.messege = "Error en la base datos";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }
      



        }
        public object deleteRoute(int idaccount, string imei, string route)
        {

            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var _reply = _routeDao.UpdateRouteImei(imei, route, idaccount);
                if (_reply)
                {

                    reply.messege = "Guardado correctamente";
                    reply.status = "Ok";
                    // reply.data = _reply;
                    return reply;
                }

                else
                {

                    reply.messege = "No se guardo correctamente";
                    reply.status = "Fail";
                    // reply.data = _reply;
                    return reply;
                }

            }
            catch (Exception e)
            {

                reply.messege = "Error en la base datos";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }
        }

        public ReplyViewModel DataFactXmlFactura(List<FactNutriViewModel> query1, int idAccount, string iduser, string option, string campaign, string status)
        {
            ReplyViewModel reply = new ReplyViewModel();
            var error = "";
            try
            {
                Invoice invoice_item = new Invoice();
                string ruc = query1.Select(x => x.ruc).FirstOrDefault();

                var users = _branchMigrateDao.UsersRuc(ruc);
                bool cabecera = true;

                foreach (var item in query1)
                {
                    if (cabecera) {
                        invoice_item.NUMBER = item.numFact;
                        invoice_item.FECHA = item.dateFact;
                        invoice_item.CLIENTE = item.razonsocial;
                        invoice_item.RUC_CEDULA = ruc;
                        cabecera = false;
                    }

                    _branchMigrateDao.SaveInvoive(invoice_item, idAccount, 0, option, campaign, status);
                    
                    reply.status = "OK";
                    reply.data = 1;
                }

            }
            catch (Exception e)
            {
                var ex = e.Message.ToString();
                int ne = -1;
                throw;
            }
            return reply;
        }

        public ReplyViewModel DataFactXmlDetalle(List<FactNutriViewModel> query1, int idAccount, string iduser, string option, string campaign, string status)
        {
            ReplyViewModel reply = new ReplyViewModel();
            var error = "";
            try
            {
                Invoice_detail invoice_detail_item = null;

                string ruc = query1.Select(x => x.ruc).FirstOrDefault();
                var users = _branchMigrateDao.UsersRuc(ruc);
                
                foreach (var item in query1)
                {
                    invoice_detail_item = new Invoice_detail();
                    int _idproduct = _branchMigrateDao.ExistProductNutri(item.code);

                    if (_idproduct > 0)
                    {
                        invoice_detail_item.IDPRODUCTO = _idproduct;
                        invoice_detail_item.AMOUNT = item.cant;
                        invoice_detail_item.DESCRIPTION = "";
                        invoice_detail_item.UNIT_PRICE = 0;
                        invoice_detail_item.IVA = 0;
                    }
                    else
                    {
                        ErroresCargaFactura errorF = new ErroresCargaFactura();
                        errorF.Code = item.code;
                        errorF.Error = "El producto no se encuentra registrado en el maestro";
                        reply.data = errorF;
                        reply.messege = "Error";
                        return reply;
                    }

                    _branchMigrateDao.SaveDetalle(invoice_detail_item, item.numFact, idAccount, 0, option, campaign, status);

                    reply.status = "OK";
                    reply.data = 1;
                }

            }
            catch (Exception e)
            {
                var ex = e.Message.ToString();

                int ne = -1;
                throw;
            }
            return reply;
        }

        public ReplyViewModel DataFactXml(List<FactNutriViewModel> query1, int idAccount, string iduser, string option, string campaign, string status, bool guardar)
        {
            ReplyViewModel reply = new ReplyViewModel();
            var error = "";
            try
            {
                Invoice invoice_item = new Invoice();
                Invoice_detail invoice_detail_item = null;

                string ruc = query1.Select(x => x.ruc).FirstOrDefault();

                var users = _branchMigrateDao.UsersRuc(ruc);
                bool cabecera = true;
                foreach (var item in query1)
                {
                    invoice_detail_item = new Invoice_detail();

                    if (cabecera)
                    {
                        invoice_item.NUMBER = item.numFact;
                        invoice_item.FECHA = item.dateFact;
                        invoice_item.CLIENTE = item.razonsocial;
                        invoice_item.RUC_CEDULA = ruc;
                        cabecera = false;
                    }

                    bool Duplicado = _branchMigrateDao.facturaDuplicada(invoice_item, idAccount);

                    if (Duplicado) {
                        ErroresCargaFactura errorF = new ErroresCargaFactura();
                        errorF.Code = item.code;
                        errorF.Error = "Factura Duplicada";
                        reply.data = errorF;
                        reply.messege = "Duplicado";
                        return reply;
                    }

                    int _idproduct = _branchMigrateDao.ExistProductNutri(item.code);

                    if (_idproduct > 0)
                    {
                        invoice_detail_item.IDPRODUCTO = _idproduct;
                        invoice_detail_item.AMOUNT = item.cant;
                        invoice_detail_item.DESCRIPTION = "";
                        invoice_detail_item.UNIT_PRICE = 0;
                        invoice_detail_item.IVA = 0;
                        invoice_item.Invoice_details = new List<Invoice_detail>();
                        invoice_item.Invoice_details.Add(invoice_detail_item);
                    }
                    else
                    {
                        ErroresCargaFactura errorF = new ErroresCargaFactura();
                        errorF.Code = item.code;
                        errorF.Error = "El producto no se encuentra registrado en el maestro";
                        reply.data = errorF;
                        reply.messege = "Error";
                        return reply;
                    }

                    if (guardar)
                    {
                        _branchMigrateDao.SaveProductNutriMigrate(invoice_item, idAccount, 0, option, campaign, status);
                    }

                    reply.status = "OK";
                    reply.data = 1;
                }

            }
            catch (Exception e)
            {
                var ex = e.Message.ToString();

                int ne = -1;
                throw;
            }
            return reply;
        }


        public object GuardarlocalesNuevoAbaseLocalYexternaant(GetListbranchViewModel _respose)
        {

            ReplyViewModel reply = new ReplyViewModel();


            var item = _respose._route;
            Branch BranchModel = new Branch();
            BranchModel = _routeDao.GetBranchbyCode(item.Codigo_Encuesta, _respose.account);
            var _data = _routeDao.GetIdUbicationByName(item.Provincia, item.Canton, item.Parroquia);
            if (_data == null)
            {
                reply.status = "error";
                reply.messege = "La informacion de Provicia o Cuidad o Parrioquia no existe en la Base de datos. Consulte los Catologos";
                _respose._route.Errores = "La informacion de Provicia o Cuidad o Parrioquia no existe en la Base de datos. Consulte los Catologos";
                reply.data = _respose._route;
                return reply;
            }
            BranchModel.IdProvince = _data.idprovince;
            BranchModel.IdDistrict = _data.Iddistrict;
            BranchModel.IdParish = _data.Idparish;
            var valcodigo = ValidoCodigo(item.Codigo_Encuesta, _respose.account, _data.Iddistrict, 1);
            if (valcodigo != "")
            {
                reply.status = "error";
                reply.messege = valcodigo;
                _respose._route.Errores = valcodigo;
                reply.data = _respose._route;

                return reply;
            }

            BranchModel.routeDate = DateTime.Now;
            if (BranchModel.Id == 0)
            {
                BranchModel.Code = item.Codigo_Encuesta;
                BranchModel.ExternalCode = item.PT_indice;
                BranchModel.PersonOwner.Code = item.Codigo_Encuesta;
                BranchModel.IdAccount = _respose.account;
                BranchModel.PersonOwner.IdAccount = _respose.account;
                BranchModel.IdCountry = Guid.Parse("BE7CF5FF-296B-464D-82FA-EF0B4F48721B");// Pais ecuador
                BranchModel.IsAdministratorOwner = "SI";
                BranchModel.Zone = "-";
                BranchModel.Neighborhood = "-";

                BranchModel.NumberBranch = "-";
                BranchModel.SecundaryStreet = "-";
                BranchModel.StatusRegister = "A";
                BranchModel.PersonOwner.SurName = "-";
                BranchModel.PersonOwner.TypeDocument = "CI";
                BranchModel.PersonOwner.StatusRegister = "A";
                BranchModel.IdSector = _routeDao.GetSectorByName("CENTRO", BranchModel.IdDistrict);

                //if (_branchMigrateDao.ValTypeBussiness(item.Tipo.ToUpper().TrimEnd().TrimStart(), Guid.Parse(campaign)))
                //{
                //    BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
                //}
                //else
                //{
                //    error = "Tipo de negocio no permitido";
                //    break;
                //}


            }
            BranchModel.PersonOwner.StatusRegister = "A";
            BranchModel.StatusRegister = "D";
            BranchModel.ESTADOAGGREGATE = "S";
            BranchModel.state_period = "S";

            BranchModel.ExternalCode = item.PT_indice;
            BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
            BranchModel.Name = item.local;
            BranchModel.MainStreet = item.Dirección;
            BranchModel.Reference = item.Referencia;
            BranchModel.PersonOwner.Name = item.Nombres + " " + item.Apellidos;
            BranchModel.PersonOwner.SurName = item.Apellidos;
            BranchModel.PersonOwner.mail = item.Mail;
            BranchModel.PersonOwner.Document = item.Cédula;
            BranchModel.PersonOwner.Mobile = item.Celular;
            BranchModel.PersonOwner.Phone = item.Telefono;
            BranchModel.Label = item.local;
            var latitud = item.Latitud;
            var lat = latitud.Contains("E") ? ExponecialToString(latitud) : latitud;
            BranchModel.LatitudeBranch = lat.Length <= 10 ? lat : lat.Substring(0, 11);
            var longitud = item.Longitud;
            string len = longitud.Contains("E") ? ExponecialToString(longitud) : longitud;
            BranchModel.LenghtBranch = len.Length <= 10 ? len : len.Substring(0, 11);
            BranchModel.Cluster = item.CLUSTER;
            BranchModel.RUTAAGGREGATE = item.RUTA;
            BranchModel.IMEI_ID = item.IMEI;

            BranchModel.CommentBranch = "nuevo";

            var date = DateTime.Now;
            if (_respose.option == 2)
            {

                try
                {
                    date = DateTime.ParseExact(item.Fecha, "dd-MM-yyyy", null);
                }
                catch (Exception e)
                {

                    reply.status = "error";
                    reply.messege = "El campo fecha no tienen un format";
                    _respose._route.Errores = "El campo fecha no tienen un format (dd-MM-yy)";
                    reply.data = _respose._route;


                    return reply;
                    throw new Exception("Error al consultar Sectores");
                }
            }
            Branch resp = _routeDao.GuardarlocalesCreadoAPPPedido(BranchModel, _respose.account, _respose.iduser, _respose.option, _respose.campaign, _respose.status, date);
            if (resp != null)
            {
                _routeDao.ActivarLocalesGuardadoIndustrial(resp.Id);
                if (_respose.account == 15)
                {
                    try
                    {
                        List<PostCoberturaClienteGuardarViewModel> Post = new List<PostCoberturaClienteGuardarViewModel>();
                        Post.Add(new PostCoberturaClienteGuardarViewModel
                        {
                            nU_ID = resp.Id,
                            nU_CEDULA_RUC = Int64.Parse(resp.PersonOwner.Document),
                            nU_NOMBRE = resp.PersonOwner.Name + " " + resp.PersonOwner.SurName,
                            nU_DIRECCION = resp.MainStreet,
                            nU_TIPO_NEG = resp.TypeBusiness,
                            nU_TELEFONO = Int64.Parse(resp.PersonOwner.Phone),
                            nU_CODIGO_VEND = Int64.Parse(resp.Cluster)
                        });
                        var json = JsonConvert.SerializeObject(Post);
                        var EstadoRespuestaCrearClienteIM = Task.Factory.StartNew(() =>
                        {
                            return _helpersHttpClientBussiness.PostApi("CoberturaClienteNuevo/agregarlista", json);
                        });

                        var respuesta = EstadoRespuestaCrearClienteIM.Result.Result;

                        if (respuesta)
                        {

                            reply.status = "Ok";
                            reply.messege = "Local Guardado";
                            reply.data = resp.Id;
                            return reply;
                        }


                    }
                    catch (Exception e)
                    {
                        reply.status = "error";
                        reply.messege = e.Message;
                        _respose._route.Errores = "Error base Externo";
                        reply.data = 999999999;
                        throw;
                    }
                }
                else
                {
                    reply.status = "Ok";
                    reply.messege = "Local Guardado";
                    reply.data = resp.Id;
                    return reply;

                }

            };
            reply.status = "error";
            reply.messege = "Error base mardis";
            _respose._route.Errores = "Error base mardis";
            reply.data = _respose._route.Codigo_Encuesta;

            return reply;

        }
        public object GuardarlocalesNuevoAbaseLocalYexternamew(GetListbranchViewModel _respose)
        {

            ReplyViewModel reply = new ReplyViewModel();


            var item = _respose._route;
            Branch BranchModel = new Branch();
            BranchModel = _routeDao.GetBranchbyCode(item.Codigo_Encuesta, _respose.account);
            var _data = _routeDao.GetIdUbicationByName(item.Provincia, item.Canton, item.Parroquia);
            if (_data == null)
            {

                return null;
            }
            BranchModel.IdProvince = _data.idprovince;
            BranchModel.IdDistrict = _data.Iddistrict;
            BranchModel.IdParish = _data.Idparish;
            var valcodigo = ValidoCodigo(item.Codigo_Encuesta, _respose.account, _data.Iddistrict, 1);
            if (valcodigo != "")
            {
                reply.status = "error";
                reply.messege = valcodigo;
                _respose._route.Errores = valcodigo;
                reply.data = _respose._route;

                return null;
            }

            BranchModel.routeDate = DateTime.Now;
            if (BranchModel.Id == 0)
            {
                BranchModel.Code = item.Codigo_Encuesta;
                BranchModel.ExternalCode = item.PT_indice;
                BranchModel.PersonOwner.Code = item.Codigo_Encuesta;
                BranchModel.IdAccount = _respose.account;
                BranchModel.PersonOwner.IdAccount = _respose.account;
                BranchModel.IdCountry = Guid.Parse("BE7CF5FF-296B-464D-82FA-EF0B4F48721B");// Pais ecuador
                BranchModel.IsAdministratorOwner = "SI";
                BranchModel.Zone = "-";
                BranchModel.Neighborhood = "-";

                BranchModel.NumberBranch = "-";
                BranchModel.SecundaryStreet = "-";
                BranchModel.StatusRegister = "A";
                BranchModel.PersonOwner.SurName = "-";
                BranchModel.PersonOwner.TypeDocument = "CI";
                BranchModel.PersonOwner.StatusRegister = "A";
                BranchModel.IdSector = _routeDao.GetSectorByName("CENTRO", BranchModel.IdDistrict);

                //if (_branchMigrateDao.ValTypeBussiness(item.Tipo.ToUpper().TrimEnd().TrimStart(), Guid.Parse(campaign)))
                //{
                //    BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
                //}
                //else
                //{
                //    error = "Tipo de negocio no permitido";
                //    break;
                //}


            }
            BranchModel.PersonOwner.StatusRegister = "A";
            BranchModel.StatusRegister = "D";
            BranchModel.ESTADOAGGREGATE = "S";
            BranchModel.state_period = "S";

            BranchModel.ExternalCode = item.PT_indice;
            BranchModel.TypeBusiness = item.Tipo.ToUpper().TrimEnd().TrimStart();
            BranchModel.Name = item.local;
            BranchModel.MainStreet = item.Dirección;
            BranchModel.Reference = item.Referencia;
            BranchModel.PersonOwner.Name = item.Nombres + " "+ item.Apellidos;
            BranchModel.PersonOwner.SurName = item.Apellidos;
            BranchModel.PersonOwner.mail = item.Mail;
            BranchModel.PersonOwner.Document = item.Cédula;
            BranchModel.PersonOwner.Mobile = item.Celular;
            BranchModel.PersonOwner.Phone = item.Telefono;
            BranchModel.Label = item.local;
            var latitud = item.Latitud;
            var lat = latitud.Contains("E") ? ExponecialToString(latitud) : latitud;
            BranchModel.LatitudeBranch = lat.Length <= 10 ? lat : lat.Substring(0, 11);
            var longitud = item.Longitud;
            string len = longitud.Contains("E") ? ExponecialToString(longitud) : longitud;
            BranchModel.LenghtBranch = len.Length <= 10 ? len : len.Substring(0, 11);
            BranchModel.Cluster = item.CLUSTER;
            BranchModel.RUTAAGGREGATE = item.RUTA;
            BranchModel.IMEI_ID = item.IMEI;

            BranchModel.CommentBranch = "nuevo";

            var date = DateTime.Now;
            if (_respose.option == 2)
            {

                try
                {
                    date = DateTime.ParseExact(item.Fecha, "dd-MM-yyyy", null);
                }
                catch (Exception e)
                {

                    reply.status = "error";
                    reply.messege = "El campo fecha no tienen un format";
                    _respose._route.Errores = "El campo fecha no tienen un format (dd-MM-yy)";
                    reply.data = _respose._route;


                    return null;
                    throw new Exception("Error al consultar Sectores");
                }
            }
            Branch resp = _routeDao.GuardarlocalesCreadoAPPPedido(BranchModel, _respose.account, _respose.iduser, _respose.option, _respose.campaign, _respose.status, date);
            if (resp != null)
            {

                _respose.iduser = resp.Id.ToString();
                try
                {

                    if (resp.IdAccount == 15) { 
                    _routeDao.ActivarLocalesGuardadoIndustrial(resp.Id);
                    List<PostCoberturaClienteGuardarViewModel> Post = new List<PostCoberturaClienteGuardarViewModel>();
                       
                        string Direccompleta = resp.MainStreet;
                        int max = 90;
                        string direccion = Direccompleta.Substring(0, max < Direccompleta.Length ? max : Direccompleta.Length);
                        string Tipo = resp.TypeBusiness;
                        int maxt = 30;
                        string Tipoapi = Tipo.Substring(0, maxt < Tipo.Length ? max : Tipo.Length);
                        Post.Add(new PostCoberturaClienteGuardarViewModel
                    {
                        nU_ID = resp.Id,
                        nU_CEDULA_RUC = Int64.Parse(resp.PersonOwner.Document),
                        nU_NOMBRE = resp.PersonOwner.Name + " " + resp.PersonOwner.SurName,
                        nU_DIRECCION = direccion,
                        nU_TIPO_NEG = Tipoapi,
                        nU_TELEFONO = Int64.Parse(resp.PersonOwner.Phone),
                        nU_CODIGO_VEND = Int64.Parse(resp.Cluster)
                    });
                    var json = JsonConvert.SerializeObject(Post);
                    var EstadoRespuestaCrearClienteIM =  Task.Factory.StartNew(() =>
                    {
                        return _helpersHttpClientBussiness.PostApi("CoberturaClienteNuevo/agregarlista", json);
                    });

                    var respuesta = EstadoRespuestaCrearClienteIM.Result.Result;

                    if (respuesta)
                    {


                     return _respose;
                    }
                    }
                    return _respose;
                }
                catch (Exception e)
                {
                    return -2.0;
                }
                return 1.0;
            };
            return  _respose;

        }

        #region llamadas API ENGINE
        public async Task<string> GeneratePdf(string campana, string uri) {
            HelpersHttpClientENGINEBussiness httpEngine = new HelpersHttpClientENGINEBussiness();
            var clienteCupo = await httpEngine.PostApiString("/TaskExterno/PdfMasive?ids="+ uri);
            
            return clienteCupo;
        }
        #endregion
        #region funciones

        string ValidoCodigo(string code, int Idaccount, Guid Iddistrict, int fil)
        {

            if (!_routeDao.UniqueCodebranch(code, Idaccount, Iddistrict)) return "Este registro ya se encuentra creado en otra ciudad. Por favor asigne un codigo Unico";

            return "";
        }
        string ExponecialToString(String lat)
        {

            var tam = int.Parse(lat.Substring(lat.Length - 1));
            var cero = "0.";
            for (int i = 1; i < tam; i++)
            {
                cero = cero + "0";

            }


            char[] MyChar = { '.', ',', ' ', '-' };
            lat = lat.Substring(0, lat.Length - 3);
            lat = lat.Replace(".", "");

            string cadenaTexto = lat;
            string negativo = "";
            int resultado;
            resultado = cadenaTexto.IndexOf("-");
            negativo = resultado == 0 ? "-" : "";
            lat = lat.Replace("-", "");
            string NewString = lat.TrimEnd(MyChar);
            lat = negativo + cero + NewString;
            return lat;


        }

        public ReplyViewModel PrintErrorTask(List<ListBranchExcelModel> model, FileInfo file)
        {


            // string sWebRootFolder = _Env.WebRootPath;



            //string sFileName = @"Listado.xlsx";
            //    string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);

         ;
            try
            {

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ErroresCarga");

                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");

                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 50;
                    worksheet.Column(5).Width = 32;
                    worksheet.Column(7).Width = 40;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(11).Width = 20;
                    worksheet.Column(12).Width = 20;
                    worksheet.Column(13).Width = 20;
                    worksheet.Column(14).Width = 20;
                    worksheet.Column(15).Width = 20;
                    worksheet.Column(16).Width = 20;
                    worksheet.Column(17).Width = 20;
                    worksheet.Column(18).Width = 20;
                    worksheet.Column(19).Width = 20;
                    worksheet.Column(20).Width = 30;
                    worksheet.Cells[1, 1].Value = "Codigo_Encuesta";
                    worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Font.Size = 12;
                    worksheet.Cells[1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(colFromHex);

                    worksheet.Cells[1, 2].Value = "PT_indice";
                    worksheet.Cells[1, 2].Style.Font.Size = 12;
                    worksheet.Cells[1, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 2].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 2].Style.Font.Bold = true;

                    worksheet.Cells[1, 3].Value = "Tipo";
                    worksheet.Cells[1, 3].Style.Font.Size = 12;
                    worksheet.Cells[1, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 3].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 3].Style.Font.Bold = true;


                    worksheet.Cells[1, 4].Value = "local";
                    worksheet.Cells[1, 4].Style.Font.Size = 12;
                    worksheet.Cells[1, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 4].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 4].Style.Font.Bold = true;

                    worksheet.Cells[1, 5].Value = "Dirección";
                    worksheet.Cells[1, 5].Style.Font.Size = 12;
                    worksheet.Cells[1, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 5].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 5].Style.Font.Bold = true;

                    worksheet.Cells[1, 6].Value = "Referencia";
                    worksheet.Cells[1, 6].Style.Font.Size = 12;
                    worksheet.Cells[1, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 6].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 6].Style.Font.Bold = true;


                    worksheet.Cells[1, 7].Value = "Nombres";
                    worksheet.Cells[1, 7].Style.Font.Size = 12;
                    worksheet.Cells[1, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 7].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 7].Style.Font.Bold = true;


                    worksheet.Cells[1, 8].Style.Font.Size = 12;
                    worksheet.Cells[1, 8].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 8].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 8].Style.Font.Bold = true;

                    worksheet.Cells[1, 9].Value = "Mail";
                    worksheet.Cells[1, 9].Style.Font.Size = 12;
                    worksheet.Cells[1, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 9].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 9].Style.Font.Bold = true;

                    worksheet.Cells[1, 10].Value = "Cédula";
                    worksheet.Cells[1, 10].Style.Font.Size = 12;
                    worksheet.Cells[1, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 10].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 10].Style.Font.Bold = true;

                    worksheet.Cells[1, 11].Value = "Celular";
                    worksheet.Cells[1, 11].Style.Font.Size = 12;
                    worksheet.Cells[1, 11].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 11].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 11].Style.Font.Bold = true;

                    worksheet.Cells[1, 12].Value = "Telefono";
                    worksheet.Cells[1, 12].Style.Font.Size = 12;
                    worksheet.Cells[1, 12].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 12].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 12].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 12].Style.Font.Bold = true;

                    worksheet.Cells[1, 13].Value = "Latitud";
                    worksheet.Cells[1, 13].Style.Font.Size = 11;
                    worksheet.Cells[1, 13].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 13].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 13].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 13].Style.Font.Bold = true;

                    worksheet.Cells[1, 14].Value = "Longitud";
                    worksheet.Cells[1, 14].Style.Font.Size = 12;
                    worksheet.Cells[1, 14].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 14].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 14].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 14].Style.Font.Bold = true;


                    worksheet.Cells[1, 15].Value = "Provincia";
                    worksheet.Cells[1, 15].Style.Font.Size = 12;
                    worksheet.Cells[1, 15].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 15].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 15].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 15].Style.Font.Bold = true;

                    worksheet.Cells[1, 16].Value = "Canton";
                    worksheet.Cells[1, 16].Style.Font.Size = 12;
                    worksheet.Cells[1, 16].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 16].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 16].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 16].Style.Font.Bold = true;

                    worksheet.Cells[1, 17].Value = "Parroquia";
                    worksheet.Cells[1, 17].Style.Font.Size = 12;
                    worksheet.Cells[1, 17].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 17].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 17].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 17].Style.Font.Bold = true;

                    worksheet.Cells[1, 18].Value = "CLUSTER";
                    worksheet.Cells[1, 18].Style.Font.Size = 12;
                    worksheet.Cells[1, 18].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 18].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 18].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 18].Style.Font.Bold = true;

                    worksheet.Cells[1, 19].Value = "Estado";
                    worksheet.Cells[1, 19].Style.Font.Size = 12;
                    worksheet.Cells[1, 19].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 19].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 19].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 19].Style.Font.Bold = true;

                    worksheet.Cells[1, 20].Value = "RUTA";
                    worksheet.Cells[1, 20].Style.Font.Size = 12;
                    worksheet.Cells[1, 20].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 20].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 20].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 20].Style.Font.Bold = true;


                    worksheet.Cells[1, 21].Value = "IMEI";
                    worksheet.Cells[1, 21].Style.Font.Size = 12;
                    worksheet.Cells[1, 21].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 21].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 21].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 21].Style.Font.Bold = true;



                    worksheet.Cells[1, 22].Value = "Fecha";
                    worksheet.Cells[1, 22].Style.Font.Size = 12;
                    worksheet.Cells[1, 22].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 22].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 22].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 22].Style.Font.Bold = true;

                    worksheet.Cells[1, 23].Value = "Información de Error";
                    worksheet.Cells[1, 23].Style.Font.Size = 12;
                    worksheet.Cells[1, 23].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 23].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 23].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 23].Style.Font.Bold = true;

                    int rows = 2;
                    int rowsobs = 2;
                    foreach (var t in model)
                    {
                        worksheet.Cells[rows, 1].Value = t.Codigo_Encuesta;
                        worksheet.Cells[rows, 2].Value = t.PT_indice;
                        worksheet.Cells[rows, 3].Value = t.Tipo;
                        worksheet.Cells[rows, 4].Value = t.local;
                        worksheet.Cells[rows, 5].Value = t.Dirección;
                        worksheet.Cells[rows, 6].Value = t.Referencia;
                        worksheet.Cells[rows, 7].Value = t.Nombres;
                        worksheet.Cells[rows, 8].Value = t.Apellidos;
                        worksheet.Cells[rows, 9].Value = t.Mail;
                        worksheet.Cells[rows, 10].Value = t.Cédula;
                        worksheet.Cells[rows, 11].Value = t.Celular;
                        worksheet.Cells[rows, 12].Value = t.Telefono;
                        worksheet.Cells[rows, 13].Value = t.Latitud;
                        worksheet.Cells[rows, 14].Value = t.Longitud;
                        worksheet.Cells[rows, 15].Value = t.Provincia;
                        worksheet.Cells[rows, 16].Value = t.Canton;
                        worksheet.Cells[rows, 17].Value = t.Parroquia;
                        worksheet.Cells[rows, 18].Value = t.CLUSTER;
                        worksheet.Cells[rows, 19].Value = t.Estado;
                        worksheet.Cells[rows, 20].Value = t.RUTA;
                        worksheet.Cells[rows, 21].Value = t.IMEI;
                        worksheet.Cells[rows, 22].Value = t.Fecha;
                        worksheet.Cells[rows, 23].Value = t.Errores;

                        rows++;

                    }






                    package.Save();
                    //var streams = new MemoryStream(package.GetAsByteArray());

                    var log = DateTime.Now;
                    string LogFile = log.ToString("yyyyMMddHHmmss");


                    ReplyViewModel reply = new ReplyViewModel();
                    reply.status = "Ok";
                    reply.messege = "Impresión exitosa";
                    //reply.data = GetUrlAzureContainerbyStrem(streams, LogFile, ".xlsx");
                    return reply;
                }

            }
            catch (Exception e)
            {

                ReplyViewModel reply = new ReplyViewModel();
                reply.status = "Error";
                reply.messege = e.Message;

                return reply;
            }





        }


        public ReplyViewModel PrintErrorTaskProducto(List<CargaStockItemModelWeb> model, FileInfo file)
        {


            // string sWebRootFolder = _Env.WebRootPath;



            //string sFileName = @"Listado.xlsx";
            //    string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);

            ;
            try
            {

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Formato");

                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");

                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 50;
                    worksheet.Column(5).Width = 32;
                    worksheet.Column(6).Width = 80;
                    worksheet.Cells[1, 1].Value = "Id_Producto";
                    worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Font.Size = 12;
                    worksheet.Cells[1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(colFromHex);

                    worksheet.Cells[1, 2].Value = "Nombre_Producto";
                    worksheet.Cells[1, 2].Style.Font.Size = 12;
                    worksheet.Cells[1, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 2].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 2].Style.Font.Bold = true;

                    worksheet.Cells[1, 3].Value = "Vendedor";
                    worksheet.Cells[1, 3].Style.Font.Size = 12;
                    worksheet.Cells[1, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 3].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 3].Style.Font.Bold = true;


                    worksheet.Cells[1, 4].Value = "Cedula";
                    worksheet.Cells[1, 4].Style.Font.Size = 12;
                    worksheet.Cells[1, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 4].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 4].Style.Font.Bold = true;

                    worksheet.Cells[1, 5].Value = "Cantidad";
                    worksheet.Cells[1, 5].Style.Font.Size = 12;
                    worksheet.Cells[1, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 5].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 5].Style.Font.Bold = true;


                    worksheet.Cells[1, 6].Value = "Errores";
                    worksheet.Cells[1, 6].Style.Font.Size = 12;
                    worksheet.Cells[1, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 6].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 6].Style.Font.Bold = true;
                    int rows = 2;
                    int rowsobs = 2;
                    foreach (var t in model)
                    {
                        worksheet.Cells[rows, 1].Value = t.Id_Producto;
                        worksheet.Cells[rows, 2].Value = t.Nombre_Producto;
                        worksheet.Cells[rows, 3].Value = t.Vendedor;
                        worksheet.Cells[rows, 4].Value = t.Cedula;
                        worksheet.Cells[rows, 5].Value = t.Cantidad;
                        worksheet.Cells[rows, 6].Value = t.Errores;

                        rows++;

                    }






                    package.Save();
                    //var streams = new MemoryStream(package.GetAsByteArray());

                    var log = DateTime.Now;
                    string LogFile = log.ToString("yyyyMMddHHmmss");


                    ReplyViewModel reply = new ReplyViewModel();
                    reply.status = "Ok";
                    reply.messege = "Impresión exitosa";
                    //reply.data = GetUrlAzureContainerbyStrem(streams, LogFile, ".xlsx");
                    return reply;
                }

            }
            catch (Exception e)
            {

                ReplyViewModel reply = new ReplyViewModel();
                reply.status = "Error";
                reply.messege = e.Message;

                return reply;
            }





        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <returns>20200922</returns>
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
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <returns>20200922</returns>
        public ReplyViewModel GetStatusTask(GetCampaignViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                reply.messege = "success";
          
                reply.status = "Ok";
                var _Reply = _taskCampaignDao.GetStatusTask(_data.IdAccount).Select(x => new CampaignModelReply
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(); ;
                reply.data = _Reply;
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }

        public ReplyViewModel GetActiveRoute(GetCampaignViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                
             var    model = _taskCampaignDao.GetActRoute(_data.IdAccount);
                reply.messege = "success";
                reply.data = model;
                reply.status = "Ok";
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }
        public ReplyViewModel GetRoute(GetEncuestadorViewModel _data)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var imei = _taskCampaignDao.GetIMEIRoute(_data.RouteCode, _data.IdAccount);
                IList<string> encuestadores = new List<string>();
                string[] separadas;

                foreach (var person in imei)
                {
                    if (person != null)
                    {
                        string UniqueImei = person;
                        separadas = UniqueImei.Split('-');
                        for (int xi = 0; xi < separadas.Length; xi++)
                        {

                            encuestadores.Add(separadas[xi]);
                        }
                    }
                }
                var data = _taskCampaignDao.GetIdPersonByDocumentAndTypeDocumentAndAccount(encuestadores, "IMEI", _data.IdAccount);

                IList<PollsterViewModelReply> _model = new List<PollsterViewModelReply>();

                foreach (var item in data)
                {
                    _model.Add(new PollsterViewModelReply { Code = item.IMEI, Name = item.Name, Phone = item.Phone, Oficina = item.Oficina });
                }
                reply.messege = "success";
                reply.data = _model;
                reply.status = "Ok";
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "datos delos escuestadores";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;
            }
        }
        public ReplyViewModel ChangeStatusRoute(int idAccount, string route)
        {
            ReplyViewModel reply = new ReplyViewModel();
            bool model =  _taskCampaignDao.UpdateStatusRoute(idAccount, route);
            if (model) {
                reply.messege = "success";
           ;
                reply.status = "Ok";
            }
            else {
                reply.messege = "Existio un error al actualizar los datos";
               
                reply.status = "Fallo la consulta";
            }


            return reply;
        }
        #endregion
    }
}
