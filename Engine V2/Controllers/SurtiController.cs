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
using Chariot.Framework.SurtiApp;
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
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;
        public SurtiController(ILogger<SurtiController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper, IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _ordersBusiness = new OrdersBusiness(_chariotContext, distributedCache, mapper);
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

          EntregadorModeloApp _entregadoresModeloApp = new EntregadorModeloApp();

            EntregadorDetalle _entregadorModeloApp = new EntregadorDetalle();

            _entregadorModeloApp.username = "dsamueza@mardisresearch.com";
            _entregadorModeloApp.email = "dsamueza@mardisresearch.com";
            _entregadorModeloApp.first_name = "David";
            _entregadorModeloApp.last_name = "Samueza";
            _entregadorModeloApp.active = true;
            _entregadorModeloApp.deleted = false;
            _entregadorModeloApp.status = "loaded";
            _entregadorModeloApp.delivery_count = "0";
            _entregadorModeloApp.id = 5577;



            _entregadoresModeloApp.entregadores.Add(_entregadorModeloApp);
            _entregadorModeloApp = new EntregadorDetalle();
            _entregadorModeloApp.username = "dsamuedza@mardisresearch.com";
            _entregadorModeloApp.email = "dsamuezad@mardisresearch.com";
            _entregadorModeloApp.first_name = "Davidss";
            _entregadorModeloApp.last_name = "Samueza112";
            _entregadorModeloApp.active = true;
            _entregadorModeloApp.deleted = false;
            _entregadorModeloApp.status = "loaded";
            _entregadorModeloApp.delivery_count = "6";
            _entregadorModeloApp.id = 5377;



            _entregadoresModeloApp.entregadores.Add(_entregadorModeloApp);
            _entregadorModeloApp = new EntregadorDetalle();

            _entregadorModeloApp.username = "dsamuedza@mardisresearch.com";
            _entregadorModeloApp.email = "dsamuezad@mardisresearch.com";
            _entregadorModeloApp.first_name = "Oscar";
            _entregadorModeloApp.last_name = "larreagui";
            _entregadorModeloApp.active = true;
            _entregadorModeloApp.deleted = false;
            _entregadorModeloApp.status = "empty";
            _entregadorModeloApp.delivery_count = "0";
            _entregadorModeloApp.id = 5370;



            _entregadoresModeloApp.entregadores.Add(_entregadorModeloApp);
            var S = _entregadoresModeloApp;
            return Ok(S);

        }
        [HttpGet]
        [Route("inventarioBodegaXIdbodegaCentral/{Cuenta}")]
        //  [Authorize]
        public async Task<IActionResult> inventarioBodegaXIdbodegaCentral(int IdbodegaCentral)
        {

            Inventario_bodegaModelApp _inventario_bodegaModelApp = new Inventario_bodegaModelApp();

            InventarioDetalebodega _inventariosDetalebodega = new InventarioDetalebodega();
          

                _inventariosDetalebodega.subwarehouse = 1;
            ProductoBodega _productoBodega = new ProductoBodega();
                _productoBodega.name = "Aji (0.5 Libras)";
                _productoBodega.short_description = null;
                _productoBodega.full_description = null;
                _productoBodega.sku = null;
                _productoBodega.approved_rating_sum = 0;
                _productoBodega.price= 0.7500;
                _productoBodega.disable_buy_button = false;
                _productoBodega.stock_quantity = 10000;
                _productoBodega.has_tier_prices = false;
                _productoBodega.category_ids.Add(29);
            ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 115;
            _productoBodega.images.Add(_imagePoductoBodega);
            _inventariosDetalebodega.product = _productoBodega;
            _inventariosDetalebodega.id = 48;
       


            _inventario_bodegaModelApp.warehouse_inventory.Add(_inventariosDetalebodega);



            _inventariosDetalebodega = new InventarioDetalebodega();


            _inventariosDetalebodega.subwarehouse = 2;
             _productoBodega = new ProductoBodega();
            _productoBodega.name = "Ajo en Pepa (1 Unidad)";
            _productoBodega.short_description = "Ajo en Pepa (1 Unidad)";
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 0.7500;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 125;
            _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.images.Add(_imagePoductoBodega);
            _inventariosDetalebodega.product = _productoBodega;
            _inventariosDetalebodega.id = 121;
         


            _inventario_bodegaModelApp.warehouse_inventory.Add(_inventariosDetalebodega);

            return Ok(_inventario_bodegaModelApp);

        }
        [HttpGet]
        [Route("inventarioBodegaXIdbodegaCentralTODOS/{Cuenta}")]
        //  [Authorize]
        public async Task<IActionResult> inventarioBodegaXIdbodegaCentralTODOS(int IdbodegaCentral)
        {

            ConsolidadoItemSurtiApp _inventario_bodegaModelApp = new ConsolidadoItemSurtiApp();

            ConsolidadoInventarioDetalebodega _inventariosDetalebodega = new ConsolidadoInventarioDetalebodega();



            ConsolidadoProductoBodega _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Aji (0.5 Libras)";
            _productoBodega.short_description = null;
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 0.7500;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
            ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.images.Add(_imagePoductoBodega);
            _productoBodega.id = 115;
            _productoBodega.unit_type = "kgs";
            _inventariosDetalebodega.product = _productoBodega;
            _inventariosDetalebodega.id = 1;
      


            _inventario_bodegaModelApp.consolidadoItem.Add(_inventariosDetalebodega);



            _inventariosDetalebodega = new ConsolidadoInventarioDetalebodega();


 
            _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Ajo en Pepa (1 Unidad)";
            _productoBodega.short_description = "Ajo en Pepa (1 Unidad)";
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 0.7500;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
            _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.images.Add(_imagePoductoBodega);
            _productoBodega.id = 125;
            _productoBodega.unit_type = "kgs";
            _inventariosDetalebodega.product = _productoBodega;
            _inventariosDetalebodega.id = 2;


            _inventario_bodegaModelApp.consolidadoItem.Add(_inventariosDetalebodega);

            return Ok(_inventario_bodegaModelApp);

        }

        #endregion
    }
}
