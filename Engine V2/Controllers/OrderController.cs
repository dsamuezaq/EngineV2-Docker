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
using Chariot.Framework.MardisOrdersViewModel;
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
    public class OrderController : AController<OrderController>
    {

        #region Variable
        private readonly OrdersBusiness _ordersBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;
        public OrderController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper,IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _ordersBusiness = new OrdersBusiness(_chariotContext, distributedCache, mapper);
            _Env = envrnmt;
        }
        #endregion
        #region APIs
        #region Obtener data Pedidos
        [HttpGet]
        [Route("getVendedores")]
        public async Task<IActionResult> getVendedores(int Idaccount = 15)
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetVentas(Idaccount));
        }

        [HttpGet]
        [Route("getRubros")]
        public async Task<IActionResult> getRubros( int Idaccount=15 )
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetRubros(Idaccount));
        }

        [HttpGet]
        [Route("getClientes")]
        public async Task<IActionResult> getClientes()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetClientes());
        }

        [HttpPost]
        [Route("SaveClientes")]
        public async Task<IActionResult> SaveClientes(List<ClientViewModel> _response)
        {


            return Ok(_ordersBusiness.SaveClientes(_response));
        }
        [HttpGet]
        [Route("getArticulos")]
        public async Task<IActionResult> getArticulos(int Idaccount = 15)
        {
           
            return Ok(_ordersBusiness.GetArticulos(Idaccount));
        }
        [HttpGet]
        [Route("GETObtenerProductoXCodigo")]
        public async Task<IActionResult> GETObtenerProductoXCodigo(string CodigoProducto)
        {

            return Ok(_ordersBusiness.BSSObtenerProductoXCodigo(CodigoProducto));
        }

        [HttpGet]
        [Route("getDepositos")]
        public async Task<IActionResult> getDepositos(int Idaccount=15)
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetDepositos(090637));
        }

        [HttpGet]
        [Route("GETListadoVisitas")]
        public async Task<IActionResult> GETListadoVisitas()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.ListadoVisitasBSS());
        }

        [HttpPost]
        [Route("GETGuardarVisitas")]
        public async Task<IActionResult> GETGuardarVisitas(List<VisitasViewModel> DatosVisitas)
        {
         

            return Ok(_ordersBusiness.GuardarVisitaBSS(DatosVisitas));
        }
        [HttpGet]
        [Route("getRegistros")]
        public async Task<IActionResult> getRegistros(int Idaccount=15)
        {
            List<RegistroViewModel> datos = new List<RegistroViewModel>();
            RegistroViewModel dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileVendedores";
            dato.cantidadRegistros = _ordersBusiness.GetVentas(Idaccount).Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileRubros";
            dato.cantidadRegistros = _ordersBusiness.GetRubros(Idaccount).Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileClientes";
            dato.cantidadRegistros = 88;
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileArticulos";
            dato.cantidadRegistros = _ordersBusiness.GetArticulos(Idaccount).Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileDepositos";
            dato.cantidadRegistros = _ordersBusiness.GetDepositos(Idaccount).Count();
            dato.paginas = 1;
            datos.Add(dato);

            ///*TABLAS DESUENTOS*/
            dato = new RegistroViewModel();
            dato.tabla = "DESCUENTO_AVENA";
            dato.cantidadRegistros = 1;//baseDt.DescuentoAvena.Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "DESCUENTO_FORMAPAGO";
            dato.cantidadRegistros = 1;// baseDt.DescuentoFormapago.Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "DESCUENTO_VOLUMEN";
            dato.cantidadRegistros = 1;// baseDt.DescuentoVolumen.Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "PRECIO_ESCALA";
            dato.cantidadRegistros = 1;// baseDt.PrecioEscala.Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "CARTERA";
            dato.cantidadRegistros = 1;// baseDt.CarteraDis.Where(x => x.Codvendedor != null).Count();
            dato.paginas = 1;
            datos.Add(dato);




            return Ok(datos);
        }
        #endregion

        #region Guardar data Pedidos
        [HttpPost]
        [Route("PEDIDOS")]
        public async Task<IActionResult> PostPEDIDOS(List<OrdersViewModel> PEDIDOS)
        {


            return Ok(_ordersBusiness.SaveDataOrders(PEDIDOS));
        }

        [HttpPost]
        [Route("Devolucion")]
        public async Task<IActionResult> PostDevolucion(List<Devolucion> Devolucion)
        {

            return Ok(_ordersBusiness.GuardarDevoluciones(Devolucion));
        }
        [HttpPost]
        [Route("ProductList")]
        [Authorize]
        public async Task<IActionResult> ProductList(int account)
        {


            return Ok(_ordersBusiness.GetProduct(account));
        }
        [HttpPost]
        [Route("SaveExcelProduct")]
        [Authorize]
        public async Task<IActionResult> SaveExcelProduct(ExcelProductViewModelReply _response)
        {


            return Ok(_ordersBusiness.SaveProductexcel(_response));
        }
        [HttpPost]
        [Route("transactionProduct")]
        [Authorize]
        public async Task<IActionResult> transactionProduct(ProductViewModelReplyOnly _response)
        {


            return Ok(_ordersBusiness.SaveProduct(_response));
        }

        [HttpPost]
        [Route("DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int idproduct)
        {


            return Ok(_ordersBusiness.UpdataProduct(idproduct));
        }
        [HttpPost]
        [Route("PrintErrorProduct")]
        [Authorize]
        public async Task<IActionResult> PrintErrorProduct(List<ExcelProductViewModelReply> response)
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
            var reply = _ordersBusiness.PrintErrorTask(response, file);

            var streams = new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine(sWebRootFolder, sFileName)));
            reply.data = _ordersBusiness.GetUrlAzureContainerbyStrem(streams, LogFile, ".xlsx");

            return Ok(reply);
        }
        [HttpPost]
        [Route("SequeceOrder")]
        public async Task<IActionResult> SequeceOrder(int idvendedor,string iddevice)
        {


            return Ok(_ordersBusiness.Getsequence(idvendedor, iddevice));
        }

        #endregion

        #region Guardar data Inventario
        [HttpPost]
        [Route("inventario")]
        public async Task<IActionResult> Postinventario(List<InventaryViewModel> inventaries)
        {
 

            return Ok(_ordersBusiness.SaveDataInventary(inventaries));
        }

        #endregion
        #region App Entregas
        [HttpPost]
        [Route("CoberturaCartera")]
        //[Authorize]
        public async Task<IActionResult> CoberturaCartera()
        {


            return Ok(_ordersBusiness.GetCartera());
        }


        [HttpPost]
        [Route("deliveryRoute")]
      //  [Authorize]
        public async Task<IActionResult> deliveryRoute(string idcamion, int idaccount)
        {


            return Ok(await _ordersBusiness.GetRoute(idcamion, idaccount));
        }


        [HttpPost]
        [Route("ObtenerRutaClienteXCamion")]
        //  [Authorize]
        public async Task<IActionResult> ObtenerRutaClienteXCamion(string idcamion, int idaccount)
        {


            return Ok(await _ordersBusiness.ObtenerRutas(idcamion, idaccount));
        }
        [HttpPost]
        [Route("ObtenerCarteraXCliente")]
        //  [Authorize]
        public async Task<IActionResult> ObtenerCarteraXCliente(int idclient, int idaccount)
        {


            return Ok(await _ordersBusiness.ObtenerDatosDeCarteraXCliente(idclient, idaccount));
        }
        [HttpPost]
        [Route("ObtenerCarteraXClienteTotal")]
        //  [Authorize]
        public async Task<IActionResult> ObtenerCarteraXClienteTotal(int camion, int idaccount)
        {


            return Ok(await _ordersBusiness.ObtenerDatosDeCarteraXClienteTotal(camion, idaccount));
        }

        

        [Route("Invoicecustomer")]
       // [Authorize]
        public async Task<IActionResult> Invoicecustomer(int idclient, int idaccount)
        {


            return Ok(await _ordersBusiness.GetInvoice(idclient, idaccount));
        }

        [Route("InvoicecustomerXFact")]
        // [Authorize]
        public async Task<IActionResult> InvoicecustomerXFact(int Fact, int idaccount)
        {


            return Ok(await _ordersBusiness.GetInvoicXFact(Fact, idaccount));
        }
        [Route("InvoicecustomerXFactTotal")]
        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> InvoicecustomerXFactTotal(int camion, int idaccount)
        {


            return Ok(await _ordersBusiness.GetInvoicXFactTOTAL(camion, idaccount));
        }
        
        [HttpPost]
        [Route("Invoicetruck")]
      ///  [Authorize]
        public async Task<IActionResult> Invoicetruck(string Imei, int idaccount)
        {


            return Ok(await _ordersBusiness.GetTruck(Imei, idaccount));
        }

        [HttpPost]
        [Route("POSTActualizarEstadoEntregaFacturaXFumero")]
      //  [Authorize]
        public async Task<IActionResult> POSTActualizarEstadoEntregaFacturaXFumero( int NumeroFactura, String CodigoLocal ,string cO_observacion , string cO_estado, double lat , double lon)
        {

            return Ok(await _ordersBusiness.BSSActualizarEstadoEntregaFacturaXFumero(NumeroFactura, CodigoLocal, cO_observacion, cO_estado,  lat,  lon));
        }
        [HttpPost]
        [Route("POSTActualizarEstadoEntregaFacturaXFumerotest")]
        //  [Authorize]
        public async Task<IActionResult> POSTActualizarEstadoEntregaFacturaXFumerotest(int NumeroFactura, String CodigoLocal, string cO_observacion, string cO_estado, double lat, double lon)
        {

            return Ok(await _ordersBusiness.BSSActualizarEstadoEntregaFacturaXFumeroTest(NumeroFactura, CodigoLocal, cO_observacion, cO_estado, lat, lon));
        }

        

        [HttpPost]
        [Route("POSTCrearPagoCarteraXFactura")]
        //  [Authorize]
        public async Task<IActionResult> POSTCrearPagoCarteraXFactura(CarteraPagoViewModel _datoCarteraPago)
        {

            return Ok(await _ordersBusiness.BSSActualizarPagosCarteraXFumero(_datoCarteraPago));
        }

        [HttpPost]
        [Route("CrearDevoluciones")]
        //  [Authorize]
        public async Task<IActionResult> CrearDevoluciones(List<DevolucionFactura> DevolucionFacturas)
        {

            return Ok( _ordersBusiness.GuardarDevolucionFactura(DevolucionFacturas));
        }

        [HttpPost]
        [Route("ObtenerInformacionHistoricaFactura")]
        //  [Authorize]
        public async Task<IActionResult> ObtenerInformacionHistoricaFactura(int Fact, int idaccount)
        {

                 return Ok(await _ordersBusiness.ObtenerInformacionHistoricaFactura(Fact, idaccount));
        }

        [HttpPost]
        [Route("ObtenerInformacionHistoricaFacturaTotal")]
        //  [Authorize]
        public async Task<IActionResult> ObtenerInformacionHistoricaFacturaTotal(List<int> Fact, int idaccount)
        {

            return Ok(await _ordersBusiness.ObtenerInformacionHistoricaFacturaTotal(Fact, idaccount));
        }
        #endregion
        #endregion
    }
}