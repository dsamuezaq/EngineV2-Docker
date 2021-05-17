using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.Business.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardiscoreViewModel.Route;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Engine_V2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : AController<BranchController>
    {

        #region Variable
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;
        public BranchController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper, IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);
            _Env = envrnmt;
        }
        #endregion
        #region APIs
        [HttpGet]
        [Route("RouteBranch")]
        public async Task<IActionResult> Get(int idaccount, string iddevice)
        {

            return Ok(_taskCampaignBusiness.GetBranches(idaccount, iddevice));
        }

        [HttpPost]        
        [Route("ObtenerParametroRuta")]
        public async Task<IActionResult> ObtenerParametroRuta(string idusuario, string ruta, int cuenta)
        {

            return Ok(_taskCampaignBusiness.ObtenerParametrosRutas(idusuario, ruta, cuenta));
        }

        [HttpPost]
        [Route("ActualizarGeoLocal")] 
        public async Task<IActionResult> Get(int idbranch, string lat,string lon)
        {

            return Ok(_taskCampaignBusiness.ActualizaGeo(lat, lon, idbranch));
        }

        [HttpGet]
        [Route("Campaign")]
        public async Task<IActionResult> Campaign(int idaccount = 0)
        {
           return Ok(_taskCampaignBusiness.GetCampanigAccount(idaccount));
        }
        [HttpPost]
        [Route("transactionPollster")]
        [Authorize]
        public async Task<IActionResult> transactionPollster(TransactionPollsterViewModel _data)
        {
            return Ok(_taskCampaignBusiness.SavePollster(_data));
        }

        [HttpPut]
        [Route("updatePollster")]
        [Authorize]
        public async Task<IActionResult> updatePollster(UpdatePollsterSupervisor _data)
        {
            return Ok(_taskCampaignBusiness.UpdatePollsterSupervisor(_data));
        }

        [HttpPost]
        [Route("ListPollster")]
        [Authorize]
        public async Task<IActionResult> ListPollster(int account)
        {

            return Ok(_taskCampaignBusiness.GetPollster(account));
        }


        [HttpPost]
        [Route("LoadTask")]
        [Authorize]
        public async Task<IActionResult> LoadTask(GetListbranchViewModel response)
        {

            return Ok(_taskCampaignBusiness.SaveRouteTask(response));
        }
        [HttpPost]
        [Route("POSTGuardarLocalesNuevoAPPPedido")]
      //  [Authorize]
        public async Task<IActionResult> LoadTPOSTGuardarLocalesNuevoAPPPedidoask(GetListbranchViewModel response)
        {

       
            return Ok(_taskCampaignBusiness.GuardarlocalesNuevoAbaseLocalYexternaant(response));
        }
        [HttpPost]
        [Route("POSTGuardarLocalesNuevoAPPPedidoI")]
        //  [Authorize]
        public object LoadTPOSTGuardarLocalesNuevoAPPPedidoaskI(GetListbranchViewModel response)
        {


            return Ok(_taskCampaignBusiness.GuardarlocalesNuevoAbaseLocalYexternamew(response));
        }

        [HttpPost]
        [Route("PrintErrorLoadTask")]
        [Authorize]
        public async Task<IActionResult> PrintErrorLoadTask(List<ListBranchExcelModel> response)
        {

            string sWebRootFolder = _Env.ContentRootPath;
            var log = DateTime.Now;
            string LogFile = log.ToString("yyyyMMddHHmmss");
            string sFileName = @"Ruta4.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);

            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
   
            }
            else {
         
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                
            }
             var  reply= _taskCampaignBusiness.PrintErrorTask(response, file);
       
            var streams = new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine(sWebRootFolder, sFileName)));
            reply.data = _taskCampaignBusiness.GetUrlAzureContainerbyStrem(streams, LogFile, ".xlsx");
 
            return Ok(reply);
        }
        [HttpPost]
        [Route("RouteCampaign")]
        [Authorize]
        public async Task<IActionResult> CampaignbyAccount(GetCampaignViewModel _data)
        {

            return Ok(_taskCampaignBusiness.GetCampaign(_data));
        }

        [HttpPost]
        [Route("RouteStatus")]
        [Authorize]
        public async Task<IActionResult> RouteStatus(GetCampaignViewModel _data)
        {

            return Ok(_taskCampaignBusiness.GetStatusTask(_data));
        }
        [Route("RouteActive")]
        [Authorize]
        public async Task<IActionResult> RouteActive(GetCampaignViewModel _data)
        {

            return Ok(_taskCampaignBusiness.GetActiveRoute(_data));
        }
        [Route("GetEncuestadorbyRoute")]
        [Authorize]
        public async Task<IActionResult> GetEncuestador(GetEncuestadorViewModel _data)
        {

            return Ok(_taskCampaignBusiness.GetRoute(_data));
        }
        [Route("ChangeStatusRoute")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(GetEncuestadorViewModel _data)
        {

            return Ok(_taskCampaignBusiness.ChangeStatusRoute(_data.IdAccount,_data.RouteCode));
        }

        [Route("GetlistPollsterActivebyAccount")]
        [Authorize]
        public async Task<IActionResult> GetlistPollsterActivebyAccount(int account)
        {

            return Ok(_taskCampaignBusiness.GetPollsterActive(account));
        }

        [Route("GetlistPollsterActivebyAccountRoute")]
        [Authorize]
        public async Task<IActionResult> GetlistPollsterActivebyAccountRoute(GetEncuestadorViewModel _data)
        {

            return Ok(_taskCampaignBusiness.GetPollsterActiveRoute(_data.IdAccount, _data.RouteCode));
        }
        

        [Route("SaveRoutePollsterbyImei")]
        [Authorize]
        public async Task<IActionResult> SaveEncuestador(GetEncuestadorSaveViewModel _model)
        {


            var model = _taskCampaignBusiness.SaveImei(_model.IdAccount, _model.Iddevice, _model.RouteCode);

            return Ok(model);
        }

        [Route("DeleteRoutePollsterbyImei")]
        [Authorize]
        public async Task<IActionResult> deleteEcuestador(GetEncuestadorSaveViewModel _model)
        {


            var model = _taskCampaignBusiness.deleteRoute(_model.IdAccount, _model.Iddevice, _model.RouteCode);

            return Ok(model);
        }
        #endregion
        #region AdministracionRutas
        [HttpPost]
        [Route("ObtenerLocalesConGeo")]
        public async Task<IActionResult> ObtenerLocalesConGeo(int cuenta)
        {

            return Ok(_taskCampaignBusiness.ObtenerLocalesConGeo(cuenta));
        }
        #endregion
    }
}
