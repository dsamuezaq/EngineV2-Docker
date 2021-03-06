﻿using AutoMapper;
using Chariot.Engine.Business.ClientRest;
using Chariot.Engine.DataAccess.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Engine.DataObject.MardisOrders.Vistas;
using Chariot.Engine.DataObject.SurtiApp;
using Chariot.Framework.Complement;
using Chariot.Framework.MardisClientRestModel;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardisOrdersViewModel;
using Chariot.Framework.Resources;
using Chariot.Framework.SurtiApp;
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
using System.Threading;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.MardisOrders
{
    public class OrdersBusiness : ABusiness
    {
        protected OrdersDao _ordersDao;
        protected HelpersHttpClientBussiness _helpersHttpClientBussiness = new HelpersHttpClientBussiness();
        public OrdersBusiness(ChariotContext _chariotContext,
                RedisCache distributedCache,
                IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {
            _ordersDao = new OrdersDao(_chariotContext);

        }

        public List<VendedoresViewModel> GetVentas(int Idaccount)
        {

            if (Idaccount == 0)
            {
                List<VendedoresViewModel> mapperVendedores = _mapper.Map<List<Salesman>, List<VendedoresViewModel>>(_ordersDao.SelectEntity<Salesman>().ToList());
                return mapperVendedores;
            }
            else
            {
                List<VendedoresViewModel> mapperVendedores = _mapper.Map<List<Salesman>, List<VendedoresViewModel>>(_ordersDao.SelectEntity<Salesman>().Where(x => x.idaccount == Idaccount).ToList());
                return mapperVendedores;
            }
        }
        public List<RubrosViewModel> GetRubros(int Idaccount)
        {

            List<RubrosViewModel> mapperRubros = _mapper.Map<List<RubrosViewModel>>(_ordersDao.SelectEntity<Items>());
            return mapperRubros.Where(x => x.Idaccount == Idaccount).ToList();

        }
        public Log_Cierre_Dia ObtenerLogCierre(string idvendedor)
        {
            Log_Cierre_Dia DatosCierreDia;
            try
            {
                DatosCierreDia = _ordersDao.SelectEntity<Log_Cierre_Dia>().Where(x => x.idVendedor == idvendedor && x.fecha_cierre == null).First();
                return DatosCierreDia;

            }
            catch (Exception)
            {

                return null;
            }


        }
        public bool SaveLog_Cierre_Dia(Log_Cierre_Dia _responselist)
        {
            List<ReplyViewModel> _data = new List<ReplyViewModel>();


            var _insert = _ordersDao.InsertUpdateOrDeleteSelectAll(_responselist, "I");

            if (_insert != null)
            {
                return true;

            }
            else
            {
                return false;

            }



            return false;

        }
        public List<ClientViewModel> GetClientes()
        {

            List<ClientViewModel> mapperRubros = _mapper.Map<List<ClientViewModel>>(_ordersDao.SelectEntity<Client>());
            return mapperRubros;

        }
        public List<ReplyViewModel> SaveClientes(List<ClientViewModel> _responselist)
        {
            List<ReplyViewModel> _data = new List<ReplyViewModel>();
            foreach (ClientViewModel _response in _responselist)
            {
                ReplyViewModel reply = new ReplyViewModel();
                Client mapperCliente = _mapper.Map<Client>(_response);
                var _insert = _ordersDao.InsertUpdateOrDeleteSelectAll(mapperCliente, "I");

                if (_insert != null)
                {
                    reply.data = _insert;
                    reply.messege = "";
                    reply.status = "Ok";

                }
                else
                {

                    reply.data = _responselist;
                    reply.messege = "";
                    reply.status = "Error";
                }
                _data.Add(reply);

            }
            return _data;

        }

        public List<ArticulosViewModel> GetArticulos(int Idaccount, string idVendedor)
        {

            if (Idaccount == 15)
            {


                var ProductoCartera = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaProductoViewModel>("CoberturaProducto/obtener");
                });

                List<ArticulosViewModel> mapperRubros = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.SelectEntity<Product>().Where(x => x.StatusRegister == "A" && x.Idaccount == Idaccount));
                ProductoCartera.Wait();

                List<ArticulosViewModel> _reply = (from ext in ProductoCartera.Result.Result
                                                   join ar in mapperRubros on ext.codigoprod.Trim() equals ar.IdArticulo.Trim()
                                                   where ar.Idaccount == Idaccount
                                                   select new ArticulosViewModel
                                                   {
                                                       Id = ar.Id,
                                                       IdArticulo = ar.IdArticulo,
                                                       Descripcion = ar.Descripcion,
                                                       IdRubro = ar.IdRubro,
                                                       Iva = ext.pagaiva=="N"? Decimal.Parse("1"): Decimal.Parse("1.12"),
                                                       ImpuestosInternos = ar.ImpuestosInternos,
                                                       Exento = ar.Exento,
                                                       Precio1 = ext.precioprod,
                                                       Precio2 = ar.Precio2,
                                                       Precio3 = ar.Precio3,
                                                       Precio4 = ar.Precio4,
                                                       Precio5 = ar.Precio5,
                                                       Precio6 = ar.Precio6,
                                                       Precio7 = ar.Precio7,
                                                       Precio8 = ar.Precio8,
                                                       Precio9 = ar.Precio9,
                                                       Precio10 = ext.stock,
                                                       Idaccount = Idaccount
                                                   }).ToList();
                return _reply;


            }
            else if (Idaccount == 13)
            {
                //IDVENDEDOR
                int idV = 0;
                IQueryable<Salesman> vendedores = Enumerable.Empty<Salesman>().AsQueryable();
                vendedores = (from s in Context.Salesmans where s.idVendedor.Equals(idVendedor) select s);

                if (vendedores.Count() > 0)
                {
                    idV = vendedores.First().id;
                }
                //IDVENDEDOR

                List<ArticulosViewModel> mapperRubros = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.SelectEntity<Product>().Where(x => x.StatusRegister == "A" && x.Idaccount == Idaccount));
                List<ArticulosViewModel> _reply = (from ar in mapperRubros
                                                   join mw in Context.Movil_Warenhouse_Resumes on ar.Id equals mw.IDPRODUCTO
                                                   where ar.Idaccount == Idaccount
                                                   where mw.IDVENDEDOR == idV
                                                   select new ArticulosViewModel
                                                   {
                                                       Id = ar.Id,
                                                       IdArticulo = ar.IdArticulo,
                                                       Descripcion = ar.Descripcion,
                                                       IdRubro = ar.IdRubro,
                                                       Iva = ar.Iva,
                                                       ImpuestosInternos = ar.ImpuestosInternos,
                                                       Exento = ar.Exento,
                                                       Precio1 = ar.Precio1,
                                                       Precio2 = ar.Precio2,
                                                       Precio3 = ar.Precio3,
                                                       Precio4 = ar.Precio4,
                                                       Precio5 = ar.Precio5,
                                                       Precio6 = ar.Precio6,
                                                       Precio7 = ar.Precio7,
                                                       Precio8 = ar.Precio8,
                                                       Precio9 = ar.Precio9,
                                                       Precio10 = mw.BALANCE,
                                                       Idaccount = Idaccount
                                                   }).ToList();
                return _reply.OrderByDescending(x => x.Precio10).ToList();


            }
            else
            {

                List<ArticulosViewModel> mapperRubros = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.SelectEntity<Product>().Where(x => x.StatusRegister == "A" && x.Idaccount == Idaccount));
                List<ArticulosViewModel> _reply = (from ar in mapperRubros
                                                   where ar.Idaccount == Idaccount
                                                   select new ArticulosViewModel
                                                   {
                                                       Id = ar.Id,
                                                       IdArticulo = ar.IdArticulo,
                                                       Descripcion = ar.Descripcion,
                                                       IdRubro = ar.IdRubro,
                                                       Iva = ar.Iva,
                                                       ImpuestosInternos = ar.ImpuestosInternos,
                                                       Exento = ar.Exento,
                                                       Precio1 = ar.Precio1,
                                                       Precio2 = ar.Precio2,
                                                       Precio3 = ar.Precio3,
                                                       Precio4 = ar.Precio4,
                                                       Precio5 = ar.Precio5,
                                                       Precio6 = ar.Precio6,
                                                       Precio7 = ar.Precio7,
                                                       Precio8 = ar.Precio8,
                                                       Precio9 = ar.Precio9,
                                                       Precio10 = ar.Precio10,

                                                       Idaccount = Idaccount
                                                   }).ToList();
                return _reply;

            }


        }
        public ArticulosViewModel BSSObtenerProductoXCodigo(string CodigoProducto)
        {
            try
            {
                var ProductoCartera = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaProductoViewModel>("CoberturaProducto/obtener");
                });

                List<ArticulosViewModel> DatosProducto = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.DAOObtenerProductoXCodigo(CodigoProducto));
                ProductoCartera.Wait();

                List<ArticulosViewModel> DatoProductoYCartera = (from _productoCartera in ProductoCartera.Result.Result
                                                                 join _datoProducto in DatosProducto on _productoCartera.codigoprod.Trim() equals _datoProducto.IdArticulo.Trim()
                                                                 select new ArticulosViewModel
                                                                 {
                                                                     Id = _datoProducto.Id,
                                                                     IdArticulo = _datoProducto.IdArticulo,
                                                                     Descripcion = _datoProducto.Descripcion,
                                                                     IdRubro = _datoProducto.IdRubro,
                                                                     Iva = _datoProducto.Iva,
                                                                     ImpuestosInternos = _datoProducto.ImpuestosInternos,
                                                                     Exento = _datoProducto.Exento,
                                                                     Precio1 = _productoCartera.precioprod,
                                                                     Precio2 = _datoProducto.Precio2,
                                                                     Precio3 = _datoProducto.Precio3,
                                                                     Precio4 = _datoProducto.Precio4,
                                                                     Precio5 = _datoProducto.Precio5,
                                                                     Precio6 = _datoProducto.Precio6,
                                                                     Precio7 = _datoProducto.Precio7,
                                                                     Precio8 = _datoProducto.Precio8,
                                                                     Precio9 = _datoProducto.Precio9,
                                                                     Precio10 = _productoCartera.stock,
                                                                 }).ToList();
                return DatoProductoYCartera.FirstOrDefault();
            }
            catch (Exception e)
            {

                return null;
            }

        }
        public List<DepositosViewModel> GetDepositos(int Idaccount)
        {

            List<DepositosViewModel> mapperRubros = _mapper.Map<List<DepositosViewModel>>(_ordersDao.SelectEntity<Deposit>());
            return mapperRubros.Where(x => x.Idaccount == Idaccount).ToList();

        }
        public List<Visitas> ListadoVisitasBSS()
        {

            List<Visitas> DatosVisitas = _ordersDao.SelectEntity<Visitas>();
            return DatosVisitas;

        }
        public bool GuardarVisitaBSS(List<VisitasViewModel> DatosVisitas)
        {
            try
            {
                List<Visitas> DatosVisitaEntidad = _mapper.Map<List<Visitas>>(DatosVisitas);
                return _ordersDao.GuardarRegistroVisita(DatosVisitaEntidad);
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool SaveDataOrders(List<OrdersViewModel> PEDIDOS)
        {
            try
            {
                List<Order> mapperOrders = _mapper.Map<List<Order>>(PEDIDOS);
                return _ordersDao.SaveDataPedido(mapperOrders);
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public double SaveDataOrdersInvidual(List<OrdersViewModel> PEDIDOS)
        {
            try
            {
                List<Order> mapperOrders = _mapper.Map<List<Order>>(PEDIDOS);
                return _ordersDao.SaveDataPedidoI(mapperOrders);
            }
            catch (Exception e)
            {
                return -1.0;
            }

        }
        public bool GuardarDevoluciones(List<Devolucion> devoluciones)
        {
            try
            {

                return _ordersDao.SaveDataDevolucion(devoluciones);
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool SaveDataInventary(List<InventaryViewModel> Inventaries)
        {
            try
            {
                List<Inventory> mapperinventaries = _mapper.Map<List<Inventory>>(Inventaries);
                return _ordersDao.SaveDataInventarios(mapperinventaries);
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public object Getsequence(int idvendedor, string iddevice)
        {
            try
            {
                int lastcode = _ordersDao.GetLastCode();
                List<SequenceOrder> Lnuevo = new List<SequenceOrder>();
                for (int i = 1; i <= 50; i++)
                {
                    SequenceOrder nuevo = new SequenceOrder();
                    nuevo.Idsaleman = idvendedor;
                    nuevo.estado = "R";
                    nuevo.Code = lastcode + i;
                    nuevo.codeunico = (lastcode + i).ToString();
                    nuevo.imei_id = iddevice;

                    var data = _ordersDao.InsertUpdateOrDeleteSelectAll(nuevo, "I");
                    if (data != null)
                        Lnuevo.Add(data);
                }
                return Lnuevo;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public ReplyViewModel GetProduct(int account)
        {
            ReplyViewModel reply = new ReplyViewModel();

            try
            {
                reply.messege = "";
                reply.status = "Ok";
                var _data = _ordersDao.GetProductByIdaccount(account).Select(x => new ProductViewModelReply
                {
                    Id = x.Id,
                    Codigo = x.IdArticulo,
                    Cantidad = x.Precio2,
                    Exento = x.Exento == 1 ? "Si" : "No",
                    Impuesto_interno = x.ImpuestosInternos.ToString() == "1" ? "Si" : "No",
                    IVA = x.Iva.ToString() == "1.12" ? "Si" : "No",
                    Precio = x.Precio1,
                    Sku = x.Descripcion,
                    Estado = x.StatusRegister == CStatusRegister.Active ? "ACTIVO" : "INACTIVO"

                });

                reply.data = _data.ToList();
            }
            catch (Exception e)
            {
                reply.messege = e.Message;
                reply.status = "Error";

            }

            return reply;

        }

        public ReplyViewModel SaveProductexcel(ExcelProductViewModelReply _response)
        {
            ReplyViewModel reply = new ReplyViewModel();

            Product _table = new Product();
            try
            {
                reply.messege = "Producto guardado";
                reply.status = "Ok";

                _table.IdArticulo = _response.Codigo;
                _table.Descripcion = _response.Sku;
                _table.IdRubro = "1";
                _table.Iva = _response.IVA == "si" ? 1 : 0;
                _table.ImpuestosInternos = _response.Impuesto_interno == "si" ? 1 : 0;
                _table.Exento = _response.Exento == "si" ? 1 : 0;
                _table.StatusRegister = CStatusRegister.Active;
                try
                {
                    _table.Precio1 = Decimal.Parse(_response.Precio);
                }
                catch (Exception)
                {
                    reply.status = "error";
                    reply.messege = "El campo Precio no tienen un formato correcto";
                    _response.Error = "El campo Precio no tienen un formato correcto";
                    reply.data = _response;

                    return reply;
                    throw;
                }

                try
                {
                    _table.Precio2 = Decimal.Parse(_response.Cantidad);
                }
                catch (Exception)
                {
                    reply.status = "error";
                    reply.messege = "El campo Cantidad no tienen un formato correcto";
                    _response.Error = "El campo Cantidad no tienen un formato correcto";
                    reply.data = _response;

                    return reply;
                    throw;
                }

                _table.Idaccount = _response.Idaccount;
                _ordersDao.InsertUpdateOrDelete(_table, "I");
            }
            catch (Exception e)
            {
                reply.status = "error";
                reply.messege = e.Message;
                _response.Error = e.Message;
                reply.data = _response;

            }

            return reply;

        }

        public ReplyViewModel SaveProduct(ProductViewModelReplyOnly _response)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                Product _table = new Product();
                _table.Id = _response.product.Id;
                _table.Idaccount = _response.product.Idaccount;
                _table.IdArticulo = _response.product.Codigo;
                _table.Descripcion = _response.product.Sku;
                _table.StatusRegister = CStatusRegister.Active;
                _table.IdRubro = "1";
                _table.Iva = (decimal)(_response.product.IVA == "Si" ? 1.12 : 1.0);
                _table.ImpuestosInternos = _response.product.Impuesto_interno == "si" ? 1 : 0;
                _table.Exento = _response.product.Exento == "Si" ? 1 : 0;

                _table.Precio1 = _response.product.Precio;

                _table.Precio2 = _response.product.Cantidad;
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                _ordersDao.InsertUpdateOrDelete(_table, _response.transaction);
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

        public ReplyViewModel UpdataProduct(int Idproduct)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                if (!_ordersDao.InactiveProductById(Idproduct))
                {
                    reply.messege = "No se pudo guardar la información";
                    reply.status = "Fail";
                    reply.error = "No se pudo guardar la información";
                }
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
        public ReplyViewModel UpdataActiveProduct(int Idproduct)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                if (!_ordersDao.ActiveProductById(Idproduct))
                {
                    reply.messege = "No se pudo guardar la información";
                    reply.status = "Fail";
                    reply.error = "No se pudo guardar la información";
                }
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

        public ReplyViewModel GuardarDevolucionFactura(List<DevolucionFactura> DevolucionFacturas)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                reply.data = true;
                if (!_ordersDao.GuardardevolucionFactura(DevolucionFacturas))
                {
                    reply.messege = "No se pudo guardar la información en base de datos Mardis";
                    reply.status = "Fail";
                    reply.error = "No se pudo guardar la información en base de datos Mardis";
                    reply.data = false;
                }
                else
                {
                    List<PostDevolucionFactura> Post = new List<PostDevolucionFactura>();
                    Post = DevolucionFacturas.Select(x => new PostDevolucionFactura
                    {
                        d_DEVOLUCION = x.Id + 2000,
                        d_ORDEN = x.d_ORDEN,
                        d_FECHA = x.d_FECHA,
                        d_FACTURA = x.d_FACTURA,
                        d_CLIENTE = x.d_CLIENTE,
                        d_PRODUCTO = x.d_PRODUCTO,
                        d_PRECIO = x.d_PRECIO,
                        d_CANTIDAD = x.d_CANTIDAD,
                        d_VENDEDOR = x.d_VENDEDOR,
                        d_ESTADO = x.d_ESTADO,
                        d_PEDIDO_MARDIS = "SE" + (x.Id + 1000).ToString()
                    }

                    ).ToList();
                    var json = JsonConvert.SerializeObject(Post);
                    var EstadoRespuestaCrearDevoluciones = Task.Factory.StartNew(() =>
                    {
                        return _helpersHttpClientBussiness.PostApi("CoberturaDevolucion/AgregarLista", json);
                    });

                    EstadoRespuestaCrearDevoluciones.Wait();

                    if (!EstadoRespuestaCrearDevoluciones.Result.Result)
                    {
                        reply.messege = "No se pudo guardar la información en base de datos IM";
                        reply.status = "Fail";
                        reply.error = "No se pudo guardar la información en base de datos IM";
                        reply.data = false;
                    }

                }
                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = false;
                return reply;
            }

        }
        public ReplyViewModel PrintErrorTask(List<ExcelProductViewModelReply> model, FileInfo file)
        {

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

                    worksheet.Cells[1, 1].Value = "Codigo";
                    worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Font.Size = 12;
                    worksheet.Cells[1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(colFromHex);

                    worksheet.Cells[1, 2].Value = "Sku";
                    worksheet.Cells[1, 2].Style.Font.Size = 12;
                    worksheet.Cells[1, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 2].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 2].Style.Font.Bold = true;

                    worksheet.Cells[1, 3].Value = "IVA";
                    worksheet.Cells[1, 3].Style.Font.Size = 12;
                    worksheet.Cells[1, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 3].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 3].Style.Font.Bold = true;

                    worksheet.Cells[1, 4].Value = "Impuesto_interno";
                    worksheet.Cells[1, 4].Style.Font.Size = 12;
                    worksheet.Cells[1, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 4].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 4].Style.Font.Bold = true;

                    worksheet.Cells[1, 5].Value = "Exento";
                    worksheet.Cells[1, 5].Style.Font.Size = 12;
                    worksheet.Cells[1, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 5].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 5].Style.Font.Bold = true;

                    worksheet.Cells[1, 6].Value = "Cantidad";
                    worksheet.Cells[1, 6].Style.Font.Size = 12;
                    worksheet.Cells[1, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 6].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 6].Style.Font.Bold = true;

                    worksheet.Cells[1, 7].Value = "Precio";
                    worksheet.Cells[1, 7].Style.Font.Size = 12;
                    worksheet.Cells[1, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 7].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 7].Style.Font.Bold = true;

                    worksheet.Cells[1, 8].Value = "Error";
                    worksheet.Cells[1, 8].Style.Font.Size = 12;
                    worksheet.Cells[1, 8].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.Cells[1, 8].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 8].Style.Font.Bold = true;

                    int rows = 2;
                    int rowsobs = 2;
                    foreach (var t in model)
                    {
                        worksheet.Cells[rows, 1].Value = t.Codigo;
                        worksheet.Cells[rows, 2].Value = t.Sku;
                        worksheet.Cells[rows, 3].Value = t.IVA;
                        worksheet.Cells[rows, 4].Value = t.Impuesto_interno;
                        worksheet.Cells[rows, 5].Value = t.Exento;
                        worksheet.Cells[rows, 6].Value = t.Cantidad;
                        worksheet.Cells[rows, 7].Value = t.Precio;
                        worksheet.Cells[rows, 8].Value = t.Error;

                        rows++;

                    }

                    package.Save();

                    var log = DateTime.Now;
                    string LogFile = log.ToString("yyyyMMddHHmmss");

                    ReplyViewModel reply = new ReplyViewModel();
                    reply.status = "Ok";
                    reply.messege = "Impresión exitosa";

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
        #region App Entregas
        public ReplyViewModel GetCartera()
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _data = _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                var _data2 = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");
                reply.messege = "Consulta exito api Externa";
                reply.status = "Ok";
                reply.data = _data2;

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
        public async Task<ReplyViewModel> GetRoute(string idcamion, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                Task<List<GetCoberturaCarteraViewModel>> data;
                var CarteraCob = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                });

                var _RouteUser = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + idcamion);
                //var _RouteUser = _FacturaEntrega.Where(x => x.camion.ToString() == idcamion);

                List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                var clienteRoute = from f in _RouteUser

                                   select f.codigocliente.ToString();

                var clienteRouteInCompleto = from f in _RouteUser
                                             where !NumeroDeFaturasEntregadas.Contains(f.factura)
                                             select f.codigocliente.ToString();

                List<DeliveryBranches> detailBranch = _ordersDao.GetBranchbyListCode(clienteRoute.ToList(), idaccount);

                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result;

                if (clienteRoute.Count() > 0)
                {
                    var PagosDeCarteraPorFacturas = _ordersDao.SelectEntity<vw_pagoxcarteraDevolucion>().Where(x => clienteRoute.ToList().Contains(x.cO_CODCLI.ToString()));

                    var _CarteraConPagos = (from CA in _Cartera
                                            join CP in PagosDeCarteraPorFacturas on CA.codcli equals CP.cO_CODCLI into AP
                                            from LCP in AP.DefaultIfEmpty()
                                            select new GetCoberturaCarteraViewModel
                                            {

                                                codcli = CA.codcli,
                                                f_FACTURA = CA.f_FACTURA,
                                                nrodocumento = CA.nrodocumento,
                                                valor = Math.Round((Double)(CA.valor - (LCP?.valor ?? 0)), 2)

                                            }).ToList();

                    detailBranch.AsParallel()
                            .ForAll(
                                    s =>
                                    {
                                        s.camion = _RouteUser.Where(x => x.codigocliente.ToString() == s.Code).Select(x => x.camion).FirstOrDefault();
                                        s.factura = _RouteUser.Where(x => x.codigocliente.ToString() == s.Code).Select(x => x.factura).FirstOrDefault();
                                        s.Receivables = _CarteraConPagos
                                                                    .Where(x => x.codcli.ToString().Trim() == s.Code.Trim())
                                                                    .Select(c => new ReceivableModel
                                                                    {
                                                                        f_FACTURA = c.f_FACTURA,
                                                                        nrodocumento = c.nrodocumento,
                                                                        valor = c.valor
                                                                    }).ToList();
                                        s.estado = clienteRouteInCompleto.Contains(s.Code) ? "P" : "C";

                                    }
                            );

                }
                else
                {
                    detailBranch.AsParallel()
                            .ForAll(
                                    s =>
                                    {
                                        s.camion = _RouteUser.Where(x => x.codigocliente.ToString() == s.Code).Select(x => x.camion).FirstOrDefault();
                                        s.factura = _RouteUser.Where(x => x.codigocliente.ToString() == s.Code).Select(x => x.factura).FirstOrDefault();
                                        s.Receivables = _Cartera
                                                                    .Where(x => x.codcli.ToString().Trim() == s.Code.Trim())
                                                                    .Select(c => new ReceivableModel
                                                                    {
                                                                        f_FACTURA = c.f_FACTURA,
                                                                        nrodocumento = c.nrodocumento,
                                                                        valor = c.valor
                                                                    }).ToList();

                                    }
                            );

                }

                reply.messege = "Consulta exito api Externa e info cliente Mardis";
                reply.status = "Ok";
                reply.data = detailBranch;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        public async Task<ReplyViewModel> ObtenerRutas(string idcamion, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                Task<List<GetCoberturaCarteraViewModel>> data;


                var _RouteUser = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + idcamion);
                // var _RouteUser = _FacturaEntrega.Where(x => x.camion.ToString() == idcamion);
                var ClienteCartera = await _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtenerxcamion?codigo=0" + idcamion);
                List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();
                var facturasCliente = from f in _RouteUser

                                      select f.codigocliente.ToString();
                var clienteRoute = (from f in _RouteUser

                                    select f.codigocliente.ToString())
                                   .Union(from f in ClienteCartera

                                          select f.codcli.ToString())
                                   ;

                var clienteRouteInCompleto = from f in _RouteUser
                                             where !NumeroDeFaturasEntregadas.Contains(f.factura)
                                             select f.codigocliente.ToString();

                List<DeliveryBranches> detailBranch = _ordersDao.GetBranchbyListCode(clienteRoute.ToList(), idaccount);



                if (clienteRoute.Count() > 0)
                {
                    var PagosDeCarteraPorFacturas = _ordersDao.SelectEntity<vw_pagoxcarteraDevolucion>().Where(x => clienteRoute.ToList().Contains(x.cO_CODCLI.ToString()));


                    detailBranch.AsParallel()
                            .ForAll(
                                    s =>
                                    {
                                        s.camion = int.Parse(idcamion);
                                        s.factura = facturaNumero(_RouteUser, ClienteCartera, s.Code);

                                        s.estado = facturasCliente.Contains(s.Code) ? (clienteRouteInCompleto.Contains(s.Code) ? "P" : "C") : "B";

                                    }
                            );

                }
                else
                {
                    detailBranch.AsParallel()
                            .ForAll(
                                    s =>
                                    {
                                        s.camion = int.Parse(idcamion); ;
                                        s.factura = facturaNumero(_RouteUser, ClienteCartera, s.Code);


                                    }
                            );

                }

                reply.messege = "Consulta exito api Externa e info cliente Mardis";
                reply.status = "Ok";
                reply.data = detailBranch;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        private int facturaNumero(List<GetCoberturaFacturaRutaViewModel> factura, List<GetCoberturaCarteraViewModel> cartera, string cliente)
        {

            int numero = 0;
            if (factura.Where(x => x.codigocliente.ToString() == cliente).Select(x => x.factura).Count() > 0)
            {
                numero = factura.Where(x => x.codigocliente.ToString() == cliente).Select(x => x.factura).FirstOrDefault();

            }
            else
            {
                numero = cartera.Where(x => x.codcli.ToString() == cliente).Select(x => x.nrodocumento).FirstOrDefault();

            }
            return numero;
        }

        public async Task<ReplyViewModel> ObtenerDatosDeCarteraXCliente(int idcliente, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var CarteraCob = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                });


                //    var  FacturasActivas = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");




                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result.Where(x => x.codcli == idcliente).ToList();

                List<FacturaDeuda> _FacturaDeudas = new List<FacturaDeuda>();

                //    List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var FacturaConDeuda in _Cartera)
                {
                    FacturaDeuda _FacturaDeuda = new FacturaDeuda();
                    var valorespagadosPordia = _ordersDao.ConsultarDatosDePagosCarteraXfactura(FacturaConDeuda.nrodocumento);
                    var valor = valorespagadosPordia != null ? valorespagadosPordia.FirstOrDefault().valor : 0;
                    _FacturaDeuda.codigoCliente = idcliente;
                    _FacturaDeuda.numeroFactura = FacturaConDeuda.nrodocumento;

                    _FacturaDeuda.total = FacturaConDeuda.valor - valor;
                    var FacturasActivas = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxfactura?factura=" + FacturaConDeuda.nrodocumento.ToString());
                    FacturasActivas.Wait();
                    var ExisteFactura = FacturasActivas.Result.Where(x => x.factura == FacturaConDeuda.nrodocumento);

                    _FacturaDeuda.EstadoFactura = ExisteFactura.Count() > 0 ? "POR ENTREGAR" : "POR COBRAR";
                    _FacturaDeuda.Fecha = DateTime.ParseExact(FacturaConDeuda.f_FACTURA.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                    _FacturaDeuda.codigoVendedor = FacturaConDeuda.codvendedor.ToString();
                    _FacturaDeudas.Add(_FacturaDeuda);
                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _FacturaDeudas.OrderByDescending(x => x.Fecha);

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }



        public async Task<ReplyViewModel> ObtenerDatosDeCarteraXClienteTotal(int camion, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var CarteraCob = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                });

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + camion);

                var ListaDeCodigoCliente = _FacturaEntrega.Select(x => x.codigocliente).Distinct().ToList();
                //    var  FacturasActivas = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");




                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result.Where(x => ListaDeCodigoCliente.Contains(x.codcli)).ToList();

                List<FacturaDeuda> _FacturaDeudas = new List<FacturaDeuda>();

                //    List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var FacturaConDeuda in _Cartera)
                {
                    FacturaDeuda _FacturaDeuda = new FacturaDeuda();
                    var valorespagadosPordia = _ordersDao.ConsultarDatosDePagosCarteraXfactura(FacturaConDeuda.nrodocumento);
                    var valor = valorespagadosPordia != null ? valorespagadosPordia.FirstOrDefault().valor : 0;
                    _FacturaDeuda.codigoCliente = FacturaConDeuda.codcli;
                    _FacturaDeuda.numeroFactura = FacturaConDeuda.nrodocumento;

                    _FacturaDeuda.total = FacturaConDeuda.valor - valor;

                    var ExisteFactura = _FacturaEntrega.Where(x => x.factura == FacturaConDeuda.nrodocumento);

                    _FacturaDeuda.EstadoFactura = ExisteFactura.Count() > 0 ? "POR ENTREGAR" : "POR COBRAR";
                    _FacturaDeuda.Fecha = DateTime.ParseExact(FacturaConDeuda.f_FACTURA.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                    _FacturaDeuda.codigoVendedor = FacturaConDeuda.codvendedor.ToString();
                    _FacturaDeudas.Add(_FacturaDeuda);
                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _FacturaDeudas.OrderByDescending(x => x.Fecha);

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }

        public async Task<ReplyViewModel> ObtenerDatosDeCarteraXClienteTotalTest(int camion, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                //var CarteraCob = Task.Factory.StartNew(() => {
                //    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                //});
                var CarteraCob = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtenerxcamion?codigo=0" + camion);
                });

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + camion);

                var ListaDeCodigoCliente = _FacturaEntrega.Select(x => x.codigocliente).Distinct().ToList();
                //    var  FacturasActivas = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");




                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result.ToList();

                List<FacturaDeuda> _FacturaDeudas = new List<FacturaDeuda>();

                //    List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var FacturaConDeuda in _Cartera)
                {
                    FacturaDeuda _FacturaDeuda = new FacturaDeuda();
                    var valorespagadosPordia = _ordersDao.ConsultarDatosDePagosCarteraXfactura(FacturaConDeuda.nrodocumento);
                    var valor = valorespagadosPordia != null ? valorespagadosPordia.FirstOrDefault().valor : 0;
                    _FacturaDeuda.codigoCliente = FacturaConDeuda.codcli;
                    _FacturaDeuda.numeroFactura = FacturaConDeuda.nrodocumento;
                    _FacturaDeuda.Pagos = valor;
                    _FacturaDeuda.total = FacturaConDeuda.valor - valor;

                    var ExisteFactura = _FacturaEntrega.Where(x => x.factura == FacturaConDeuda.nrodocumento);

                    _FacturaDeuda.EstadoFactura = ExisteFactura.Count() > 0 ? "POR ENTREGAR" : "POR COBRAR";
                    _FacturaDeuda.EstadoFactura = ExisteFactura.Count() > 0 ? "POR ENTREGAR" : "POR COBRAR";
                    _FacturaDeuda.credito = FacturaConDeuda.formA_PAGO == "CREDITO" ? "C" : "P";
                    _FacturaDeuda.Fecha = DateTime.ParseExact(FacturaConDeuda.f_FACTURA.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                    _FacturaDeuda.codigoVendedor = FacturaConDeuda.codvendedor.ToString();
                    _FacturaDeudas.Add(_FacturaDeuda);
                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _FacturaDeudas.OrderByDescending(x => x.Fecha);

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        public async Task<ReplyViewModel> GetInvoice(int iddevice, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");

                var Listfact = _FacturaEntrega.Where(x => x.codigocliente == iddevice).Select(x => x.factura).Distinct().ToList();
                List<InvoiceViewModel> _InvoiceViewModel = new List<InvoiceViewModel>();

                List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var item in Listfact)
                {

                    if (!NumeroDeFaturasEntregadas.Contains(item))
                    {
                        InvoiceViewModel data = new InvoiceViewModel();

                        data = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.factura)
                                .Select(x => new InvoiceViewModel
                                {
                                    factura = x.Key,
                                    fecha = x.First().fecha,
                                    total = Math.Round(CalculoFacturasXpago(Math.Round((Double)x.Sum(pc => pc.total), 2), x.Key), 2),
                                    codvend = x.First().codvend,
                                    nombrevend = x.First().nombrevend

                                }).FirstOrDefault();

                        var itemFactura = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.codigoprod)
                                .Select(x => new ProductoFacturaViewModel
                                {
                                    factura = item,
                                    codigoprod = x.First().codigoprod,
                                    cantidad = x.Sum(pc => pc.cantidad),
                                    nombreprod = x.First().nombreprod,
                                    precio = x.Max(m => m.precio),
                                    iva = x.Max(m => m.iva)

                                }).ToList();
                        data.Invoice_details = itemFactura.Where(x => x.factura == item).Select(x => new Invoice_detailViewModel
                        {
                            cantidad = DisminuirInventario(x.cantidad, item, x.codigoprod),
                            codigoprod = x.codigoprod,
                            nombreprod = x.nombreprod,
                            precio = x.precio,
                            iva = x.iva

                        }).ToList();

                        _InvoiceViewModel.Add(data);
                    }

                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _InvoiceViewModel;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }

        public async Task<ReplyViewModel> GetInvoicXFact(int Fact, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxfactura?factura=" + Fact.ToString());

                var Listfact = _FacturaEntrega.Where(x => x.factura == Fact).Select(x => x.factura).Distinct().ToList();
                List<InvoiceViewModel> _InvoiceViewModel = new List<InvoiceViewModel>();

                List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var item in Listfact)
                {

                    if (!NumeroDeFaturasEntregadas.Contains(item))
                    {
                        InvoiceViewModel data = new InvoiceViewModel();

                        data = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.factura)
                                .Select(x => new InvoiceViewModel
                                {
                                    factura = x.Key,
                                    fecha = x.First().fecha,
                                    total = Math.Round(CalculoFacturasXpago(Math.Round((Double)x.Sum(pc => pc.total), 2), x.Key), 2),
                                    codvend = x.First().codvend,
                                    nombrevend = x.First().nombrevend

                                }).FirstOrDefault();

                        var itemFactura = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.codigoprod)
                                .Select(x => new ProductoFacturaViewModel
                                {
                                    factura = item,
                                    codigoprod = x.First().codigoprod,
                                    cantidad = x.Sum(pc => pc.cantidad),
                                    nombreprod = x.First().nombreprod,
                                    precio = x.Max(m => m.precio),
                                    iva = x.Max(m => m.iva)

                                }).ToList();
                        data.Invoice_details = itemFactura.Where(x => x.factura == item).Select(x => new Invoice_detailViewModel
                        {
                            cantidad = DisminuirInventario(x.cantidad, item, x.codigoprod),
                            codigoprod = x.codigoprod,
                            nombreprod = x.nombreprod,
                            precio = x.precio,
                            iva = x.iva

                        }).ToList();

                        _InvoiceViewModel.Add(data);
                    }

                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _InvoiceViewModel;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        public async Task<ReplyViewModel> GetInvoicXFactTOTAL(int camion, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {


                var CarteraCob = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                });

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + camion);

                var ListaDeCodigoCliente = _FacturaEntrega.Select(x => x.codigocliente).Distinct().ToList();
                //    var  FacturasActivas = _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");




                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result.Where(x => ListaDeCodigoCliente.Contains(x.codcli)).ToList();




                var Listfact = _FacturaEntrega.Where(x => ListaDeCodigoCliente.Contains(x.codigocliente)).Select(x => x.factura).Distinct().ToList();
                List<InvoiceViewModel> _InvoiceViewModel = new List<InvoiceViewModel>();

                List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var item in Listfact)
                {

                    if (!NumeroDeFaturasEntregadas.Contains(item))
                    {
                        InvoiceViewModel data = new InvoiceViewModel();

                        data = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.factura)
                                .Select(x => new InvoiceViewModel
                                {
                                    factura = x.Key,
                                    fecha = x.First().fecha,
                                    total = Math.Round(CalculoFacturasXpago(Math.Round((Double)x.Sum(pc => pc.total), 2), x.Key), 2),
                                    codvend = x.First().codvend,
                                    nombrevend = x.First().nombrevend

                                }).FirstOrDefault();

                        var itemFactura = _FacturaEntrega.Where(x => x.factura == item)
                                .GroupBy(l => l.codigoprod)
                                .Select(x => new ProductoFacturaViewModel
                                {
                                    factura = item,
                                    codigoprod = x.First().codigoprod,
                                    cantidad = x.Sum(pc => pc.cantidad),
                                    nombreprod = x.First().nombreprod,
                                    precio = x.Max(m => m.precio),
                                    iva = x.Max(m => m.iva)

                                }).ToList();
                        data.Invoice_details = itemFactura.Where(x => x.factura == item).Select(x => new Invoice_detailViewModel
                        {
                            cantidad = DisminuirInventario(x.cantidad, item, x.codigoprod),
                            codigoprod = x.codigoprod,
                            nombreprod = x.nombreprod,
                            precio = x.precio,
                            iva = x.iva

                        }).ToList();

                        _InvoiceViewModel.Add(data);
                    }

                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _InvoiceViewModel;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        public async Task<ReplyViewModel> ObtenerInformacionHistoricaFactura(int Fact, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFactura/obtenerxfactura?factura=" + Fact.ToString());
                var CoberturaDevolucion = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaDevolucion>("CoberturaFacturaDevolucion/obtenerxfactura?factura=" + Fact.ToString());
                });


                var Listfact = _FacturaEntrega.Where(x => x.factura == Fact).Select(x => x.factura).Distinct().ToList();
                List<Model_FacturaHistorica> _InvoiceViewModel = new List<Model_FacturaHistorica>();

                //    List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                foreach (var item in Listfact)
                {

                    Model_FacturaHistorica data = new Model_FacturaHistorica();
                    data.NumeroFactura = item;
                    var totalFactura = _FacturaEntrega.Where(x => x.factura == item)
                            .GroupBy(l => l.factura)
                            .Select(x =>

                             Math.Round((Double)x.Sum(pc => pc.total), 2)

                            ).FirstOrDefault();
                    data.TotalFactura = totalFactura;
                    double totaldevolucion = 0;
                    if (CoberturaDevolucion.Result.Result.Count() > 0)
                    {

                        var listaDevolucion = CoberturaDevolucion.Result.Result.Where(x => x.dF_FACTURA == item)
                      .GroupBy(l => l.dF_PRODUCTO)
                      .Select(x =>

                        Math.Round((Double)(x.Sum(pc => pc.dF_PRECIO) * x.Sum(pc => pc.dF_CANTIDAD) + x.Sum(pc => pc.dF_IVA)), 2)

                      ).ToList();

                        totaldevolucion = listaDevolucion.Sum();
                        data.detalleDevoluciones = CoberturaDevolucion.Result.Result.Where(x => x.dF_FACTURA == item)
                       .GroupBy(l => l.dF_PRODUCTO)
                       .Select(x => new Modelo_DetalleDevolucion
                       {
                           // factura = item,
                           codigoprod = x.First().dF_PRODUCTO,
                           cantidad = x.Sum(pc => pc.dF_CANTIDAD),
                           nombreprod = x.First().dF_NOMBREPRO,
                           precio = x.Max(m => m.dF_PRECIO),
                           iva = x.Max(m => m.dF_IVA)

                       }).ToList();
                    }
                    data.TotalDevolucion = totaldevolucion;


                    data.detalleFacturas = _FacturaEntrega.Where(x => x.factura == item)
                            .GroupBy(l => l.codigoprod)
                            .Select(x => new Model_DellateFactura
                            {
                                // factura = item,
                                codigoprod = x.First().codigoprod,
                                cantidad = x.Sum(pc => pc.cantidad),
                                nombreprod = x.First().nombreprod,
                                precio = x.Max(m => m.precio),
                                iva = x.Max(m => m.iva)

                            }).ToList();


                    _InvoiceViewModel.Add(data);


                }

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _InvoiceViewModel;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }
        public async Task<ReplyViewModel> ObtenerInformacionHistoricaFacturaTotal(List<int> Fact, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                List<Model_FacturaHistorica> _InvoiceViewModel = new List<Model_FacturaHistorica>();
                //  var _FacturaEntregaca = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");
                foreach (int facturaIndividual in Fact)
                {
                    try
                    {

                        var _FacturaEntrega = Context.FacturasApis.Where(x => x.factura == facturaIndividual).ToList();

                        // await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFactura/obtenerxfactura?factura=" + facturaIndividual.ToString());

                        //if (_FacturaEntrega.Count() > 0) {
                        //   var  ca = _FacturaEntregaca.Where(x=>x.factura == facturaIndividual);
                        //    if (ca.Count() > 0)
                        //    {

                        //        _FacturaEntrega = ca.ToList();

                        //    }

                        //}

                        var CoberturaDevolucion = new List<GetCoberturaFacturaDevolucion>();
                        //  await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaDevolucion>("CoberturaFacturaDevolucion/obtenerxfactura?factura=" + facturaIndividual.ToString());



                        var Listfact = _FacturaEntrega.Where(x => x.factura == facturaIndividual).Select(x => x.factura).Distinct().ToList();

                        //    List<int> NumeroDeFaturasEntregadas = _ordersDao.SelectEntity<FacturasEntregadas>().Select(s => s.cO_FACTURA).ToList();

                        foreach (var item in Listfact)
                        {

                            Model_FacturaHistorica data = new Model_FacturaHistorica();
                            data.NumeroFactura = item;
                            var totalFactura = _FacturaEntrega.Where(x => x.factura == item)
                                    .GroupBy(l => l.factura)
                                    .Select(x =>

                                     Math.Round((Double)x.Sum(pc => pc.total), 2)

                                    ).FirstOrDefault();
                            data.TotalFactura = totalFactura;
                            double totaldevolucion = 0;
                            if (CoberturaDevolucion.Count() > 0)
                            {

                                var listaDevolucion = CoberturaDevolucion.Where(x => x.dF_FACTURA == item)
                              .GroupBy(l => l.dF_PRODUCTO)
                              .Select(x =>

                                Math.Round((Double)(x.Sum(pc => pc.dF_PRECIO) * x.Sum(pc => pc.dF_CANTIDAD) + x.Sum(pc => pc.dF_IVA)), 2)

                              ).ToList();

                                totaldevolucion = listaDevolucion.Sum();
                                data.detalleDevoluciones = CoberturaDevolucion.Where(x => x.dF_FACTURA == item)
                               .GroupBy(l => l.dF_PRODUCTO)
                               .Select(x => new Modelo_DetalleDevolucion
                               {
                                   // factura = item,
                                   codigoprod = x.First().dF_PRODUCTO,
                                   cantidad = x.Sum(pc => pc.dF_CANTIDAD),
                                   nombreprod = x.First().dF_NOMBREPRO,
                                   precio = x.Max(m => m.dF_PRECIO),
                                   iva = x.Max(m => m.dF_IVA)

                               }).ToList();
                            }
                            data.TotalDevolucion = totaldevolucion;


                            data.detalleFacturas = _FacturaEntrega.Where(x => x.factura == item)
                                    .GroupBy(l => l.codigoprod)
                                    .Select(x => new Model_DellateFactura
                                    {
                                        // factura = item,
                                        codigoprod = x.First().codigoprod,
                                        cantidad = x.Sum(pc => pc.cantidad),
                                        nombreprod = x.First().nombreprod,
                                        precio = x.Max(m => m.precio),
                                        iva = x.Max(m => m.iva)

                                    }).ToList();


                            _InvoiceViewModel.Add(data);

                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }


                }
                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _InvoiceViewModel;






                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }

        Double CalculoFacturasXpago(Double valor, int factura)
        {

            var DIFERENCIA = Context.PagoCarteras.Where(x => x.cO_FACTURA == factura);

            if (DIFERENCIA.Count() > 0)
            {
                var DIFERENCIADevolucion = Context.DevolucionFacturas.Where(x => x.d_FACTURA == factura);
                double pagosdevolucion = 0;
                if (DIFERENCIADevolucion.Count() > 0)
                {
                    foreach (var item in DIFERENCIADevolucion)
                    {

                        pagosdevolucion += item.d_CANTIDAD * item.d_PRECIO;
                    }

                }
                Double valorcobrado = DIFERENCIA.GroupBy(l => l.cO_FACTURA)
                        .Select(x => x.Sum(s => s.cO_VALOR_COBRO)).FirstOrDefault();

                return valor - valorcobrado - pagosdevolucion;

            }
            else
            {
                var DIFERENCIADevolucion = Context.DevolucionFacturas.Where(x => x.d_FACTURA == factura);
                double pagosdevolucion = 0;
                if (DIFERENCIADevolucion.Count() > 0)
                {
                    foreach (var item in DIFERENCIADevolucion)
                    {

                        pagosdevolucion += item.d_CANTIDAD * item.d_PRECIO;
                    }

                }
                return valor - pagosdevolucion;
            }
        }

        int DisminuirInventario(int cantidad, int factura, string pedido)
        {

            var cantidadDevuelta = Context.DevolucionFacturas.Where(x => x.d_FACTURA == factura && x.d_PRODUCTO == pedido).Select(x => x.d_CANTIDAD);

            if (cantidadDevuelta.Count() > 0)
            {

                cantidad = cantidad - (int)cantidadDevuelta.Sum();

            }
            return cantidad < 0 ? 0 : cantidad;
        }
        public async Task<ReplyViewModel> GetTruck(string iddevice, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                List<TruckViewModel> _TruckViewModelViewModel = new List<TruckViewModel>();
                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtenerxcamion?codigo=" + iddevice);

                var ListadoCodigoCamiones = _FacturaEntrega.Select(x => x.camion).Distinct().ToList();

                foreach (var Camion in ListadoCodigoCamiones)
                {

                    TruckViewModel data = new TruckViewModel();
                    var DATOS = _FacturaEntrega.Where(x => x.camion == Camion).ToList();
                    data = DATOS.Select(x => new TruckViewModel
                    {
                        truck_numbre = x.camion,
                        truck_plate = x.placa
                    }).FirstOrDefault();
                    data.TruckDetails = DATOS
                            .GroupBy(l => l.codigoprod)
                            .Select(x => new TruckDetailViewModel
                            {
                                CodProduct = x.Key,
                                NameProduct = x.First().nombreprod,
                                stock = x.Sum(pc => pc.cantidad),

                            }).ToList();

                    _TruckViewModelViewModel.Add(data);

                }

                TruckViewModel data1 = new TruckViewModel();

                reply.messege = "Consulta exito api Externa e info factura Mardis";
                reply.status = "Ok";
                reply.data = _TruckViewModelViewModel;

                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No existen datos  en la tabla";
                reply.status = "Fail";
                reply.error = e.Message;
                return reply;
            }

        }

        public async Task<ReplyViewModel> BSSActualizarEstadoEntregaFacturaXFumero(int NumeroFactura, String CodigoLocal, string cO_observacion, string cO_estado, double lat, double lon)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                List<PostEstadoFactura> postEstadoFacturas = new List<PostEstadoFactura>();
                PostEstadoFactura _PostEstadoFactura = new PostEstadoFactura();
                _PostEstadoFactura.o_FACTURA = NumeroFactura;
                _PostEstadoFactura.o_OBSERVACION = cO_observacion;
                _PostEstadoFactura.o_ESTADO = cO_estado;
                postEstadoFacturas.Add(_PostEstadoFactura);

                var json = JsonConvert.SerializeObject(postEstadoFacturas);
                var RespuestaActualizacionEstadoFacturaExter = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.PostApi("CoberturaFacturaEstado/AgregarLista", json);
                });

                bool RespuestaActualizacionEstadoFacturaExterNum = await _helpersHttpClientBussiness.GettApiParam($"CoberturaFacturaRuta/actualizar?Factura={NumeroFactura}");

                bool RespuestaActualizacionEstadoFactura = _ordersDao.GuardarfacturaEntregadas(CodigoLocal, NumeroFactura, cO_observacion, cO_estado, lat, lon, 1);
                RespuestaActualizacionEstadoFacturaExter.Wait();

                if (RespuestaActualizacionEstadoFactura && RespuestaActualizacionEstadoFacturaExter.Result.Result)
                {
                    reply.messege = "Actualizo el estado ";
                    reply.status = "Ok";

                }
                else
                {
                    var facturasEntregadas = Context.FacturasEntregadas.Where(x => x.cO_FACTURA == NumeroFactura);
                    if (facturasEntregadas.Count() > 0)
                    {
                        reply.messege = "Actualizo el estado ";
                        reply.status = "Ok";
                    }
                    else
                    {
                        reply.messege = "Existio un inconveniente Error";
                        reply.status = "Fail";
                    }



                }

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

        public async Task<ReplyViewModel> BSSActualizarEstadoEntregaFacturaXFumeroTest(int NumeroFactura, String CodigoLocal, string cO_observacion, string cO_estado, double lat, double lon)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                List<PostEstadoFactura> postEstadoFacturas = new List<PostEstadoFactura>();


                bool RespuestaActualizacionEstadoFactura = _ordersDao.GuardarfacturaEntregadas(CodigoLocal, NumeroFactura, cO_observacion, cO_estado, lat, lon, 0);


                if (RespuestaActualizacionEstadoFactura)
                {
                    reply.messege = "Actualizo el estado ";
                    reply.status = "Ok";

                }
                else
                {
                    reply.messege = "Existio un inconveniente Error";
                    reply.status = "Fail";
                }

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
        public async Task<ReplyViewModel> BSSActualizarPagosCarteraXFumero(CarteraPagoViewModel _datoCarteraPago)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _PagosRealizadoCartera = await _helpersHttpClientBussiness.GetApi<CarteraPagoViewModel>("CoberturaCobroMardis/obtener");
                List<CarteraPagoViewModel> _datoCarteraPagos = new List<CarteraPagoViewModel>();
                _datoCarteraPago.cO_ID = _PagosRealizadoCartera.Max(s => s.cO_ID) + 1;
                _datoCarteraPago.cO_FECHA_COBRO = long.Parse(DateTime.Now.AddHours(-5).ToString("yyyyMMdd"));

                _datoCarteraPagos.Add(_datoCarteraPago);
                var json = JsonConvert.SerializeObject(_datoCarteraPagos);
                var EstadoRespuestaPagosCartera = Task.Factory.StartNew(() =>
                {
                    return _helpersHttpClientBussiness.PostApi("CoberturaCobroMardis/agregarlista", json);
                });
                PagoCartera TablaPagoDeCartera = _mapper.Map<PagoCartera>(_datoCarteraPago);
                TablaPagoDeCartera.cO_FECHA_COBRO = _datoCarteraPago.cO_FECHA_COBRO = long.Parse(DateTime.Now.AddHours(-5).ToString("yyyyMMdd"));
                bool GuardoPagoCartera = _ordersDao.GuardarPagoDeCartera(TablaPagoDeCartera);
                EstadoRespuestaPagosCartera.Wait();

                if (GuardoPagoCartera && EstadoRespuestaPagosCartera.Result.Result)
                {
                    reply.messege = "Actualizo el estado ";
                    reply.status = "Ok";

                }
                else
                {
                    reply.messege = "Existio un inconveniente Error";
                    reply.status = "Fail";
                }

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
        #endregion

        #region App Bodega

        public ReplyViewModel ObtenerVendedorXdistribuidor(int Iddistribuidor)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var vistaResultado = _ordersDao.ConsularVendedoresXDistribuidor(Iddistribuidor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                if (vistaResultado.Count() > 0)
                {

                    List<DataObject.SurtiApp.EntregadorDetalle> _entregadorDetalle = vistaResultado.Select(x => new DataObject.SurtiApp.EntregadorDetalle
                    {
                        id = x.id,
                        username = x.nombre,
                        first_name = x.nombre,
                        email = x.nombre,
                        last_name = "",
                        active = true,
                        deleted = false,
                        delivery_count = x.cantidad.ToString(),
                        status = x.statusV,




                    }).ToList();
                    DataObject.SurtiApp.EntregadorModeloApp _entregadorModeloApp = new DataObject.SurtiApp.EntregadorModeloApp();
                    _entregadorModeloApp.entregadores.AddRange(_entregadorDetalle);
                    reply.data = _entregadorModeloApp;
                }
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
        public DataObject.SurtiApp.EntregadorModeloApp ObtenerVendedorXdistribuidorEngine(int Iddistribuidor)
        {
            DataObject.SurtiApp.EntregadorModeloApp _entregadorModeloApp = new DataObject.SurtiApp.EntregadorModeloApp();
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var vistaResultado = _ordersDao.ConsularVendedoresXDistribuidor(Iddistribuidor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                if (vistaResultado.Count() > 0)
                {

                    List<DataObject.SurtiApp.EntregadorDetalle> _entregadorDetalle = vistaResultado.Select(x => new DataObject.SurtiApp.EntregadorDetalle
                    {
                        id = x.id,
                        username = x.nombre,
                        first_name = x.nombre,
                        email = x.nombre,
                        last_name = "",
                        active = true,
                        deleted = false,
                        delivery_count = x.cantidad.ToString(),
                        status = x.statusV,




                    }).ToList();

                    _entregadorModeloApp.entregadores.AddRange(_entregadorDetalle);
                    reply.data = _entregadorModeloApp;
                }
                return _entregadorModeloApp;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                return null;
            }

        }
        public int Distribuidor(string iduser)
        {
            int Iddistribuidor = _ordersDao.DistribuidorID(Guid.Parse(iduser));
            return Iddistribuidor;

        }
        public ReplyViewModel ObtenerProductoEnBodegaCentralDistribuidor(int Iddistribuidor)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var vistaResultado = _ordersDao.ConsularBodegaCentralXDistribuidor(Iddistribuidor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";

                var warehouse_quantity = vistaResultado.Sum(x => x.cantidad);
                if (vistaResultado.Count() > 0)
                {
                    List<ConsolidadoInventarioDetalebodega> bodegas = new List<ConsolidadoInventarioDetalebodega>();
                    foreach (var productobodega in vistaResultado)
                    {
                        ConsolidadoInventarioDetalebodega bodega = new ConsolidadoInventarioDetalebodega();
                        ConsolidadoProductoBodega _entregadorDetalle = vistaResultado.Where(x => x.idproducto == productobodega.idproducto).Select(x => new ConsolidadoProductoBodega
                        {
                            name = x.nombre,
                            short_description = x.nombre,
                            full_description = x.nombre,
                            sku = x.barcode,

                            price = x.precio,
                            stock_quantity = x.cantidad,
                            category_ids = { x.idcategoria },
                            images = ImagenProducto(),
                            conversion_product_id = x.idproducto,
                            inventory_warehouse = x.cantidad,
                            is_price_by_unit = false,
                            price_by_unit = x.precioUnitario,
                            unit_type = "kgs",
                            id = x.idproducto



                        }).FirstOrDefault();
                        bodega.product = _entregadorDetalle;
                        bodega.quantity = productobodega.cantidad;
                        bodega.weight = productobodega.cantidad;
                        bodega.warehouse_quantity = warehouse_quantity;
                        bodega.id = productobodega.iddistribuidor;
                        bodegas.Add(bodega);

                    }


                    ConsolidadoItemSurtiApp _entregadorModeloApp = new ConsolidadoItemSurtiApp();
                    _entregadorModeloApp.warehouse_inventory.AddRange(bodegas);
                    reply.data = _entregadorModeloApp;
                }
                return reply;
            }
            catch (Exception e)
            {

                ConsolidadoItemSurtiApp _entregadorModeloApp = new ConsolidadoItemSurtiApp();
                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = _entregadorModeloApp;
                return reply;
            }

        }

        public ReplyViewModel CrearInventarioMovil(int warehouseid, int productid, int quantity, int entregadorid, int userid, string comment)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                var FueGuardoExitoso = _ordersDao.GuardarBodegaMovil(warehouseid, productid, quantity, entregadorid, userid, comment, 1);
                if (FueGuardoExitoso)
                {
                    reply.data = "Ok";
                }
                else
                {
                    reply.data = "GenericError.ProductNotFound";
                }



                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = "GenericError.WarehouseMissingInventory";
                return reply;
            }

        }
        public ReplyViewModel ObtenerBodegaCentralXDistribuidor(int Iddistribuidor)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                List<vw_resumen_Stock_bodegaCentral> vistaResultado = _ordersDao.ConsularBodegaCentralXDistribuidor(Iddistribuidor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";

                reply.data = vistaResultado.Select(x => new { codigo = x.codigoProducto, sku = x.nombre, cantidad = x.cantidad, precio = x.precio, barcode = x.barcode }).ToList();




                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se encontrar inventario";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = "GenericError.WarehouseMissingInventory";
                return reply;
            }

        }

        public ReplyViewModel ObtenerProductoEnBodegaCentralCamion(int Idvendedor)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var vistaResultado = _ordersDao.ConsularBodegaCentralXCambion(Idvendedor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";

                var warehouse_quantity = vistaResultado.Sum(x => x.cantidad);
                if (vistaResultado.Count() > 0)
                {
                    List<ConsolidadoInventarioDetalebodega> bodegas = new List<ConsolidadoInventarioDetalebodega>();
                    foreach (var productobodega in vistaResultado)
                    {
                        ConsolidadoInventarioDetalebodega bodega = new ConsolidadoInventarioDetalebodega();
                        ConsolidadoProductoBodega _entregadorDetalle = vistaResultado.Where(x => x.idproducto == productobodega.idproducto).Select(x => new ConsolidadoProductoBodega
                        {
                            name = x.nombre,
                            short_description = x.nombre,
                            full_description = x.nombre,
                            price = x.precio,
                            stock_quantity = x.cantidad,
                            category_ids = { x.idcategoria },
                            images = ImagenProducto(),
                            conversion_product_id = x.idproducto,
                            inventory_warehouse = x.cantidad,
                            is_price_by_unit = false,
                            price_by_unit = x.precioUnitario,
                            unit_type = "kgs",
                            id = x.idproducto,
                            sku = x.barcode


                        }).FirstOrDefault();
                        bodega.product = _entregadorDetalle;
                        bodega.quantity = productobodega.cantidad;
                        bodega.weight = productobodega.cantidad;
                        bodega.warehouse_quantity = warehouse_quantity;
                        bodega.id = productobodega.idproducto;
                        bodegas.Add(bodega);

                    }


                    ConsolidadoItemSurtiApp _entregadorModeloApp = new ConsolidadoItemSurtiApp();
                    _entregadorModeloApp.consolidadoItem.AddRange(bodegas);
                    reply.data = _entregadorModeloApp;
                }
                return reply;
            }
            catch (Exception e)
            {
                ConsolidadoItemSurtiApp _entregadorModeloApp = new ConsolidadoItemSurtiApp();
                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = _entregadorModeloApp;
                return reply;
            }

        }
        public ReplyViewModel ObtenerProductoEnBodegaCentralCamionEngine(int Idvendedor)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                var vistaResultado = _ordersDao.ConsularBodegaCentralXCambion(Idvendedor);
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";

                var warehouse_quantity = vistaResultado.Sum(x => x.cantidad);

                reply.data = vistaResultado.Select(x => new { codigo = x.codigoProducto, sku = x.nombre, cantidad = x.cantidad, precio = x.precio, barcode = x.barcode, code = x.barcode, producto = x.nombre, }).ToList();

                return reply;
            }
            catch (Exception e)
            {
                ConsolidadoItemSurtiApp _entregadorModeloApp = new ConsolidadoItemSurtiApp();
                reply.messege = "No existe inventario para el vendedor";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = _entregadorModeloApp;
                return reply;
            }

        }
        public ReplyViewModel CrearInventarioExcel(CargaStockModeloWeb cargaStockModeloWeb)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                int tipo = cargaStockModeloWeb.option == 1 ? 1 : -1;
                string errores = "";
                int Iddistribuidor = _ordersDao.DistribuidorID(Guid.Parse(cargaStockModeloWeb.iduser));
                int idvendedor = _ordersDao.VendedorID(cargaStockModeloWeb.stockCamion.Cedula);
                int idProducto = _ordersDao.ProductoID(cargaStockModeloWeb.stockCamion.Id_Producto, cargaStockModeloWeb.account);
                if (Iddistribuidor == 0)
                    errores = "El distribuidor no se encuentra registrado-";
                if (idvendedor == 0)
                    errores = errores + "El vendedor no se cuentra asignado al distruibuidor-";
                if (idProducto == 0)
                    errores = errores + "El producto no se encuentra registrado-";
                if (cargaStockModeloWeb.option == 1)
                {
                    String stockValido = _ordersDao.Stock(idProducto, Iddistribuidor, int.Parse(cargaStockModeloWeb.stockCamion.Cantidad));
                    if (stockValido != "")
                        errores = errores + "No tiene suficiente STOCK. Cantidad actual: " + stockValido;
                }

                var FueGuardoExitoso = false;
                if (errores == "")
                    FueGuardoExitoso = _ordersDao.GuardarBodegaMovil(Iddistribuidor,
                                                                        idProducto,
                                                                        int.Parse(cargaStockModeloWeb.stockCamion.Cantidad),
                                                                        idvendedor,
                                                                       1,
                                                                        "Cargado Engine", tipo);

                if (FueGuardoExitoso)
                {

                    reply.status = "Ok";
                    reply.messege = "Producto Cargado";
                    reply.data = cargaStockModeloWeb;
                }
                else
                {
                    reply.status = "Error";
                    reply.messege = "Producto Cargado";
                    cargaStockModeloWeb.stockCamion.Errores = errores;
                    reply.data = cargaStockModeloWeb.stockCamion;
                }



                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = "GenericError.WarehouseMissingInventory";
                return reply;
            }

        }
        public ReplyViewModel CrearInventarioAPP(int cuenta, int cantidad, int idvendedorapp, string usuario, int opcion, string codigoproducto)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                reply.messege = "Los datos fueron guardados correctamente";
                reply.status = "Ok";
                int tipo = opcion == 1 ? 1 : -1;
                string errores = "";
                int Iddistribuidor = _ordersDao.DistribuidorID(Guid.Parse(usuario));
                int idvendedor = idvendedorapp;
                int idProducto = _ordersDao.ProductoID(codigoproducto, cuenta);

                var FueGuardoExitoso = false;

                FueGuardoExitoso = _ordersDao.GuardarBodegaMovil(Iddistribuidor,
                                                                    idProducto,
                                                                   cantidad,
                                                                    idvendedor,
                                                                   1,
                                                                    "Cargado Engine", tipo);

                if (FueGuardoExitoso)
                {

                    reply.status = "Ok";
                    reply.messege = "Producto Cargado";
                    reply.data = "";
                }
                else
                {
                    reply.status = "Error";
                    reply.messege = "Producto Cargado";


                }



                return reply;
            }
            catch (Exception e)
            {

                reply.messege = "No se pudo guardar la información";
                reply.status = "Fail";
                reply.error = e.Message;
                reply.data = "GenericError.WarehouseMissingInventory";
                return reply;
            }

        }


        private List<ImagePoductoBodega> ImagenProducto()
        {
            List<ImagePoductoBodega> _imagenes = new List<ImagePoductoBodega>();
            ImagePoductoBodega _imagePoductoBodega = new ImagePoductoBodega();
            _imagePoductoBodega.id = 58;
            _imagePoductoBodega.picture_id = 81;
            _imagePoductoBodega.position = 0;
            _imagePoductoBodega.src = "http://surti-test-nopc.azurewebsites.net/images/thumbs/0000081_aji-05-libras.jpeg";
            _imagePoductoBodega.attachment = null;

            _imagenes.Add(_imagePoductoBodega);
            return _imagenes;
        }
        #endregion

        #region Promociones
        public List<PromocionesHelpers> GetPromociones(int Idaccount, string idVendedor)

        {
            ReplyViewModel reply = new ReplyViewModel();
            List<PromocionesHelpers> Promociones = new List<PromocionesHelpers>();
            if (Idaccount == 13)
            {
                Promociones = _ordersDao.GetPromociones(Idaccount);

                return Promociones;

            }
            else
            {
                reply.messege = "Existio un Error";
                reply.status = "Fail";
                return null;
            }


        }

        public List<RegalosHelpers> GetRegalos(int Idaccount, string idVendedor)

        {
            ReplyViewModel reply = new ReplyViewModel();
            List<RegalosHelpers> Regalos = new List<RegalosHelpers>();
            if (Idaccount == 13)
            {
                Regalos = _ordersDao.GetRegalos(Idaccount);

                return Regalos;

            }
            else
            {
                reply.messege = "Existio un Error";
                reply.status = "Fail";
                return null;
            }


        }
        #endregion




    }
}