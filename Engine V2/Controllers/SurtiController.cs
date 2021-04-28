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
using Chariot.Framework.SurtiApp.SP;
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
        #region APIs SURTI SP
        [HttpGet]
        [Route("shopping_cart_items/{idvendedor}")]
        //  [Authorize]
        public async Task<IActionResult> shopping_cart_items(int idvendedor)
        {
            ItemCarritoModelSurtiApp _itemCarritoModelSurtiApp = new ItemCarritoModelSurtiApp();
            _itemCarritoModelSurtiApp.delivery_date = "Mañana";
            ItemCarritoDetalleModelSurtiApp _itemDetalle = new ItemCarritoDetalleModelSurtiApp();
            _itemDetalle.created_on_utc = DateTime.UtcNow;
            _itemDetalle.updated_on_utc = DateTime.UtcNow;
            _itemDetalle.shopping_cart_type = "ShoppingCart";
            _itemDetalle.product_id = 117;
            _itemDetalle.weight = 0.0000;
            ConsolidadoProductoBodega _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Brazo de Cerdo";
            _productoBodega.short_description = null;
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 17.0100;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
            ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "https://surti-test-nopc.azurewebsites.net/images/thumbs/0000173_brazo-de-cerdo.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.unit_type = "kgs";
            _productoBodega.price_by_unit = 2.3500;
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 117;
            _productoBodega.images.Add(_imagePoductoBodega);
            _itemDetalle.product = _productoBodega;
            _itemDetalle.customer_id = 2542;
            _itemDetalle.id= 3847;
            _itemCarritoModelSurtiApp.shopping_carts.Add(_itemDetalle);


            ////////////
            ///PRODUCTO 2
            ///////////
            _itemCarritoModelSurtiApp.delivery_date = "Mañana";
             _itemDetalle = new ItemCarritoDetalleModelSurtiApp();
            _itemDetalle.created_on_utc = DateTime.UtcNow;
            _itemDetalle.updated_on_utc = DateTime.UtcNow;
            _itemDetalle.shopping_cart_type = "ShoppingCart";
            _itemDetalle.product_id = 49;
            _itemDetalle.weight = 0.0000;
            _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Ajo en Pepa (1 Unidad)";
            _productoBodega.short_description = "Ajo en Pepa (1 Unidad)";
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 0.1000;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
             _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "https://surti-test-nopc.azurewebsites.net/images/thumbs/0000173_brazo-de-cerdo.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.unit_type = "kgs";
            _productoBodega.price_by_unit = 0.1000;
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 49;
            _productoBodega.images.Add(_imagePoductoBodega);
            _itemDetalle.product = _productoBodega;
            _itemDetalle.customer_id = 2542;
            _itemDetalle.id = 3855;
            _itemCarritoModelSurtiApp.shopping_carts.Add(_itemDetalle);

            return Ok(_itemCarritoModelSurtiApp);

        }
            [HttpGet]
            [Route("products-for-tiendas")]
            //  [Authorize]
            public async Task<IActionResult> productsfortiendas(int warehouse_id)
            {

            ProductoTiendaModeloApp _productoTiendaModeloApp = new ProductoTiendaModeloApp();

        
            ProductoCatalogoTienda _itemDetalle = new ProductoCatalogoTienda();
            _itemDetalle.Id = 49;
            _itemDetalle.catalog_name = "TOP Ventas";
 
            ConsolidadoProductoBodega _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Brazo de Cerdo";
            _productoBodega.short_description = null;
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 17.0100;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
            ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "https://surti-test-nopc.azurewebsites.net/images/thumbs/0000173_brazo-de-cerdo.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.unit_type = "kgs";
            _productoBodega.price_by_unit = 2.3500;
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 117;
            _productoBodega.images.Add(_imagePoductoBodega);




            _itemDetalle.products.Add(_productoBodega);


             _productoBodega = new ConsolidadoProductoBodega();
            _productoBodega.name = "Cebolla Colorada (1 Gaveta)";
            _productoBodega.short_description = "14 libras de Cebolla Colorada";
            _productoBodega.full_description = null;
            _productoBodega.sku = null;
            _productoBodega.approved_rating_sum = 0;
            _productoBodega.price = 5.9000;
            _productoBodega.disable_buy_button = false;
            _productoBodega.stock_quantity = 10000;
            _productoBodega.has_tier_prices = false;
            _productoBodega.category_ids.Add(29);
             _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "https://surti-test-nopc.azurewebsites.net/images/thumbs/0000155_cebolla-colorada-1-gaveta.jpeg";
            _imagePoductoBodega.attachment = null;
            _productoBodega.unit_type = "kgs";
            _productoBodega.price_by_unit = 2.3500;
            _productoBodega.unit_type = "kgs";
            _productoBodega.id = 58;
            _productoBodega.images.Add(_imagePoductoBodega);




            _itemDetalle.products.Add(_productoBodega);




            _productoTiendaModeloApp.catalogs.Add(_itemDetalle);

            return Ok(_productoTiendaModeloApp);

        }
            #endregion
            #endregion
        }
}
