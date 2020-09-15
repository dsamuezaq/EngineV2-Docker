using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.Business.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardisOrdersViewModel;
using Engine_V2.Libraries;
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
        // private readonly IMapper _mapper;
        public OrderController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _ordersBusiness = new OrdersBusiness(_chariotContext, distributedCache, mapper);

        }
        #endregion
        #region APIs
        #region Obtener data Pedidos
        [HttpGet]
        [Route("getVendedores")]
        public async Task<IActionResult> getVendedores()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetVentas());
        }

        [HttpGet]
        [Route("getRubros")]
        public async Task<IActionResult> getRubros()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetRubros());
        }

        [HttpGet]
        [Route("getClientes")]
        public async Task<IActionResult> getClientes()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetClientes());
        }
        [HttpGet]
        [Route("getArticulos")]
        public async Task<IActionResult> getArticulos()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetArticulos());
        }

        [HttpGet]
        [Route("getDepositos")]
        public async Task<IActionResult> getDepositos()
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.GetDepositos());
        }
   
        [HttpGet]
        [Route("getRegistros")]
        public async Task<IActionResult> getRegistros()
        {
            List<RegistroViewModel> datos = new List<RegistroViewModel>();
            RegistroViewModel dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileVendedores";
            dato.cantidadRegistros = _ordersBusiness.GetVentas().Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileRubros";
            dato.cantidadRegistros = _ordersBusiness.GetRubros().Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileClientes";
            dato.cantidadRegistros = 88;
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileArticulos";
            dato.cantidadRegistros = _ordersBusiness.GetArticulos().Count();
            dato.paginas = 1;
            datos.Add(dato);

            dato = new RegistroViewModel();
            dato.tabla = "wsSysMobileDepositos";
            dato.cantidadRegistros = _ordersBusiness.GetDepositos().Count(); 
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
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_ordersBusiness.SaveDataOrders(PEDIDOS));
        }

        #endregion
        #endregion
    }
}