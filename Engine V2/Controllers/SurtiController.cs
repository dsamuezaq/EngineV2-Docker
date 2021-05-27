using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.Business.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisOrders;

using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardiscoreViewModel.Route;
using Chariot.Framework.MardisOrdersViewModel;
using Chariot.Framework.SurtiApp;
using Chariot.Framework.SurtiApp.CargaStock;
using Chariot.Framework.SurtiApp.SP;
using Chariot.Framework.SystemViewModel;
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
    public class SurtiController : AController<OrderController>
    {
        #region Variable
        private readonly OrdersBusiness _ordersBusiness;
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;
        public SurtiController(ILogger<SurtiController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper, IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _ordersBusiness = new OrdersBusiness(_chariotContext, distributedCache, mapper);
            _taskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);
            _Env = envrnmt;
        }
        #endregion
        #region APIs

       
        
        [HttpGet]
        [Route("ObtenieneDescuentoXcanal/{canal}")]
        //  [Authorize]
        public async Task<IActionResult> ObtenieneDescuentoXcanal(int canal)
        {

            List<DescuentoModeloApp> _descuentosUPModeloApp = new List<DescuentoModeloApp>();

            DescuentoModeloApp _descuentoUPModeloApp = new DescuentoModeloApp();
            List<DescuentoDetailModeloApp> _descuentosModeloApp = new List<DescuentoDetailModeloApp>();

            DescuentoDetailModeloApp _descuentoModeloApp = new DescuentoDetailModeloApp();
            _descuentoModeloApp.canal_id = canal;
            _descuentoModeloApp.id = 9;
            _descuentoModeloApp.name = "Descuanto Canales (2021-02-06)";
            _descuentoModeloApp.discount_amount = 0.00;
            _descuentoModeloApp.discount_percentage = 2.00;
            _descuentoModeloApp.is_percentage = true;
            _descuentoModeloApp.category_ids.Add(38);
            _descuentoModeloApp.category_ids.Add(39);


      
            _descuentoUPModeloApp.discounts.Add(_descuentoModeloApp);
             _descuentoModeloApp = new DescuentoDetailModeloApp();
            _descuentoModeloApp.canal_id = canal;
            _descuentoModeloApp.id = 9;
            _descuentoModeloApp.name = "Preventa Ajos Porcentaje Descontados";
            _descuentoModeloApp.discount_amount = 0.00;
            _descuentoModeloApp.discount_percentage = 5.00;
            _descuentoModeloApp.is_percentage = true;
            _descuentoModeloApp.category_ids.Add(39);
            _descuentoUPModeloApp.discounts.Add(_descuentoModeloApp);



            return Ok(_descuentoUPModeloApp);

        }
        [HttpGet]
        [Route("entregadoresXcuenta/{Cuenta}")]
        //  [Authorize]
        public async Task<IActionResult> Entregadores(int Cuenta)
        {


            var Respuesta=_ordersBusiness.ObtenerVendedorXdistribuidor(Cuenta);

            return Ok(Respuesta.data);

        }
        [HttpGet]
        [Route("inventarioBodegaXIdbodegaCentral/{IdbodegaCentral}")]
        //  [Authorize]
        public async Task<IActionResult> inventarioBodegaXIdbodegaCentral(int IdbodegaCentral)
        {

            //Inventario_bodegaModelApp _inventario_bodegaModelApp = new Inventario_bodegaModelApp();

            //InventarioDetalebodega _inventariosDetalebodega = new InventarioDetalebodega();
          

            //    _inventariosDetalebodega.subwarehouse = 1;
            //ProductoBodega _productoBodega = new ProductoBodega();
            //    _productoBodega.name = "Aji (0.5 Libras)2";
            //    _productoBodega.short_description = null;
            //    _productoBodega.full_description = null;
            //    _productoBodega.sku = null;
            //    _productoBodega.approved_rating_sum = 0;
            //    _productoBodega.price= 0.7500;
            //    _productoBodega.disable_buy_button = false;
            //    _productoBodega.stock_quantity = 10000;
            //    _productoBodega.has_tier_prices = false;
            //    _productoBodega.category_ids.Add(29);
            //ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            //_imagePoductoBodega.id = 58;
            //_imagePoductoBodega.picture_id = 81;
            //_imagePoductoBodega.position = 0;
            //_imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            //_imagePoductoBodega.attachment = null;
            //_productoBodega.unit_type = "kgs";
            //_productoBodega.id = 115;
            //_productoBodega.images.Add(_imagePoductoBodega);
            //_inventariosDetalebodega.product = _productoBodega;
            //_inventariosDetalebodega.id = 48;
       


            //_inventario_bodegaModelApp.warehouse_inventory.Add(_inventariosDetalebodega);



            //_inventariosDetalebodega = new InventarioDetalebodega();


            //_inventariosDetalebodega.subwarehouse = 2;
            // _productoBodega = new ProductoBodega();
            //_productoBodega.name = "Ajo en Pepa (1 Unidad)2";
            //_productoBodega.short_description = "Ajo en Pepa (1 Unidad)2";
            //_productoBodega.full_description = null;
            //_productoBodega.sku = null;
            //_productoBodega.approved_rating_sum = 0;
            //_productoBodega.price = 0.7500;
            //_productoBodega.disable_buy_button = false;
            //_productoBodega.stock_quantity = 10000;
            //_productoBodega.has_tier_prices = false;
            //_productoBodega.category_ids.Add(29);
            //_productoBodega.unit_type = "kgs";
            //_productoBodega.id = 125;
            //_imagePoductoBodega = new ImagePoductoBodega();
            //_imagePoductoBodega.id = 58;
            //_imagePoductoBodega.picture_id = 81;
            //_imagePoductoBodega.position = 0;
            //_imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            //_imagePoductoBodega.attachment = null;
            //_productoBodega.images.Add(_imagePoductoBodega);
            //_inventariosDetalebodega.product = _productoBodega;
            //_inventariosDetalebodega.id = 121;
         


            //_inventario_bodegaModelApp.warehouse_inventory.Add(_inventariosDetalebodega);

            var InventarioBodega = _ordersBusiness.ObtenerProductoEnBodegaCentralDistribuidor(IdbodegaCentral);
            return Ok(InventarioBodega.data);

        }
        [HttpGet]
        [Route("inventarioBodegaXIdbodegaCentralTODOS/{idvendedor}")]
        //  [Authorize]
        public async Task<IActionResult> inventarioBodegaXIdbodegaCentralTODOS(int idvendedor)
        {

            var InventarioBodega = _ordersBusiness.ObtenerProductoEnBodegaCentralCamion(idvendedor);
            return Ok(InventarioBodega.data);

        }
        [HttpPost]
        [Route("load-inventario-camion")]
        //  [Authorize]
        public async Task<IActionResult> loadinventariocamion(int warehouseid, int productid, int quantity, int entregadorid , int userid, string comment)
        {

            var InventarioBodega = _ordersBusiness.CrearInventarioMovil( warehouseid,  productid,  quantity,  entregadorid,  userid,  comment);
            return Ok(InventarioBodega.data);

        }
        [HttpPost]
        [Route("LoadStock")]
        //  [Authorize]
        public async Task<IActionResult> LoadStock(CargaStockModeloWeb _datoCarga)
        {
          
            return Ok(_ordersBusiness.CrearInventarioExcel(_datoCarga));

        }
        [HttpPost]
        [Route("LoadStockapp")]
        //  [Authorize]
        public async Task<IActionResult> LoadStockapp(int cuenta, int cantidad, int idvendedorapp, string usuario, int opcion, string codigoproducto)
        {

            return Ok(_ordersBusiness.CrearInventarioAPP(cuenta, cantidad, idvendedorapp, usuario, opcion, codigoproducto));

        }
        [HttpPost]
        [Route("ObtnerVenddoresActivos")]
        //  [Authorize]
        public async Task<IActionResult> ObtnerVenddoresActivos(GetCampaignViewModel _data)
        {
            var idvendedor = _ordersBusiness.Distribuidor(_data.Iduser.ToString());
            var Respuesta = _ordersBusiness.ObtenerVendedorXdistribuidorEngine(idvendedor);

            var resulta = Respuesta.entregadores.Select(x => new { id=x.id, route = x.username, status = x.status == "empty" ? false : true }).ToList();
            try
            {


                reply.messege = "success";
                reply.data = resulta;
                reply.status = "Ok";
       
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
           

            }
            return Ok(reply);

        }
        [HttpPost]
        [Route("ObtnerInventarioVenddoresActivos")]
        //  [Authorize]
        public async Task<IActionResult> ObtnerInventarioVenddoresActivos(GetEncuestadorViewModel _data)
        {
            var InventarioBodega = _ordersBusiness.ObtenerProductoEnBodegaCentralCamionEngine(int.Parse(_data.RouteCode));
    
            return Ok(InventarioBodega);

        }

        [HttpPost]
        [Route("PrintErrorLoadTask")]
   
        public async Task<IActionResult> PrintErrorLoadTask(List<CargaStockItemModelWeb> response)
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
            else
            {

                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

            }
            var reply = _taskCampaignBusiness.PrintErrorTaskProducto(response, file);

            var streams = new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine(sWebRootFolder, sFileName)));
            reply.data = _taskCampaignBusiness.GetUrlAzureContainerbyStrem(streams, LogFile, ".xlsx");

            return Ok(reply);
        }
        [HttpPost]
        [Route("BodegaCentralList")]
        [Authorize]
        public async Task<IActionResult> BodegaCentralList(int account, string idusuario)
        {
            var iddistribuidor = _ordersBusiness.Distribuidor(idusuario.ToString());
            var InventarioBodega = _ordersBusiness.ObtenerBodegaCentralXDistribuidor(iddistribuidor);

            return Ok(InventarioBodega);
        }
        [HttpPost]
        [Route("BodegaMovilList")]
        [Authorize]
        public async Task<IActionResult> BodegaMovilList( int idvendedor)
        {
            var InventarioBodega = _ordersBusiness.ObtenerProductoEnBodegaCentralCamionEngine(idvendedor);

            return Ok(InventarioBodega);
        }
        #region APIs SURTI SP

 


            #endregion
            #endregion
        }
}
