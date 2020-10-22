﻿using AutoMapper;
using Chariot.Engine.Business.ClientRest;
using Chariot.Engine.DataAccess.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Framework.Complement;
using Chariot.Framework.MardisClientRestModel;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardisOrdersViewModel;
using Chariot.Framework.Resources;
using Chariot.Framework.SystemViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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


        public List<VendedoresViewModel> GetVentas()
        {

            List<VendedoresViewModel> mapperVendedores = _mapper.Map<List<Salesman>, List<VendedoresViewModel>>(_ordersDao.SelectEntity<Salesman>());
            return mapperVendedores;



        }
        public List<RubrosViewModel> GetRubros()
        {

            List<RubrosViewModel> mapperRubros = _mapper.Map<List<RubrosViewModel>>(_ordersDao.SelectEntity<Items>());
            return mapperRubros;

        }
        public List<ClientViewModel> GetClientes()
        {

            List<ClientViewModel> mapperRubros = _mapper.Map<List<ClientViewModel>>(_ordersDao.SelectEntity<Client>());
            return mapperRubros;

        }
        public List<ReplyViewModel> SaveClientes(List<ClientViewModel> _responselist)
        {
            List<ReplyViewModel> _data = new List<ReplyViewModel>();
            foreach (ClientViewModel _response in _responselist) {
                ReplyViewModel reply = new ReplyViewModel();
                Client mapperCliente = _mapper.Map<Client>(_response);
                var _insert = _ordersDao.InsertUpdateOrDeleteSelectAll(mapperCliente, "I");


                if (_insert != null)
                {
                    reply.data = _insert;
                    reply.messege = "";
                    reply.status = "Ok";

                }
                else {

                    reply.data = _responselist;
                    reply.messege = "";
                    reply.status = "Error";
                }
                _data.Add(reply);


            }
            return _data;

        }

        public List<ArticulosViewModel> GetArticulos()
        {
            var ProductoCartera = Task.Factory.StartNew(() => {
                return _helpersHttpClientBussiness.GetApi<GetCoberturaProductoViewModel>("CoberturaProducto/obtener");
            });

            List<ArticulosViewModel> mapperRubros = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.SelectEntity<Product>());
            ProductoCartera.Wait();

            List<ArticulosViewModel> _reply = (from ext in ProductoCartera.Result.Result
                                              join ar in mapperRubros on ext.codigoprod.Trim() equals ar.IdArticulo.Trim()
                                              select new ArticulosViewModel {
                                                  Id = ar.Id,
                                                  IdArticulo = ar.IdArticulo,
                                                  Descripcion = ar.Descripcion,
                                                  IdRubro = ar.IdRubro,
                                                  Iva = ar.Iva,
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
                                              }).ToList();
            return _reply;

        }
        public ArticulosViewModel BSSObtenerProductoXCodigo(string CodigoProducto)
        {
            try
            {
                var ProductoCartera = Task.Factory.StartNew(() => {
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
        public List<DepositosViewModel> GetDepositos()
        {

            List<DepositosViewModel> mapperRubros = _mapper.Map<List<DepositosViewModel>>(_ordersDao.SelectEntity<Deposit>());
            return mapperRubros;

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
                    IVA = x.Iva.ToString() == "1" ? "Si" : "No",
                    Precio = x.Precio1,
                    Sku = x.Descripcion



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
                _table.Iva = _response.product.IVA == "si" ? 1 : 0;
                _table.ImpuestosInternos = _response.product.Impuesto_interno == "si" ? 1 : 0;
                _table.Exento = _response.product.Exento == "si" ? 1 : 0;

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
                if (!_ordersDao.InactiveProductById(Idproduct)) {
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
        public ReplyViewModel PrintErrorTask(List<ExcelProductViewModelReply> model, FileInfo file)
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
        public async Task<ReplyViewModel> GetRoute(string iddevice, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                //  Task taskA = new Task(() => List<GetCoberturaCarteraViewModel> a= _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener"));
                // Start the task.
                //taskA.Start();
                Task<List<GetCoberturaCarteraViewModel>> data;
                var CarteraCob = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaCarteraViewModel>("CoberturaCartera/obtener");
                });

                string usercel = _ordersDao.GetPollsteruserCell(iddevice, idaccount);
             //   var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");
                // var _RouteUser = _FacturaEntrega.Where(x => x.camion.ToString() == usercel && x.viaje == 101901)

               // var _RouteUser = _FacturaEntrega.ToList();

                CarteraCob.Wait();
                var _Cartera = CarteraCob.Result.Result;

                //var clienteRoute = from f in _FacturaEntrega
                //                   join c in _Cartera on f.factura equals c.f_FACTURA
                //                   select c.codcli.ToString();
                var clienteRoute = _Cartera.Select(x => x.codcli.ToString());

                List<DeliveryBranches> detailBranch = _ordersDao.GetBranchbyListCode(clienteRoute.ToList(), idaccount);

                detailBranch.AsParallel()
                    .ForAll(
                    s =>
                    {

                        s.Receivables = _Cartera
                        .Where(x => x.codcli.ToString().Trim() == s.Code.Trim())
                        .Select(c => new ReceivableModel {
                            f_FACTURA = c.f_FACTURA,
                            nrodocumento = c.nrodocumento,
                            valor = c.valor
                        }).ToList();

                    }
                    );

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
        public async Task<ReplyViewModel> GetInvoice(int iddevice, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
           
                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");


                var Listfact = _FacturaEntrega.Where(x => x.codvend.ToString() == "74").Select(x => x.factura ).Distinct().ToList();
                List<InvoiceViewModel> _InvoiceViewModel = new List<InvoiceViewModel>();


                foreach (var item in Listfact) {



                    InvoiceViewModel data = new InvoiceViewModel();
                    data = _FacturaEntrega.Where(x => x.factura == item &&  x.codvend.ToString() == "74").Select(x => new InvoiceViewModel {
                        factura = x.factura,
                        fecha = x.fecha,
                        precio = x.precio,
                        total=x.total,
                        subtotal = x.subtotal,
                        iva = x.iva,
                        codvend = x.codvend,
                        nombrevend = x.nombrevend
                    }).FirstOrDefault();
                    data.Invoice_details = _FacturaEntrega.Where(x => x.factura == item).Select(x => new Invoice_detailViewModel {
                        cantidad=x.cantidad,
                        codigoprod=x.codigoprod,
                        nombreprod=x.nombreprod
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

        public async Task<ReplyViewModel> GetTruck(string iddevice, int idaccount)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {

                var _FacturaEntrega = await _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaRutaViewModel>("CoberturaFacturaRuta/obtener");


                var Listfact = _FacturaEntrega.Where(x => x.codvend.ToString() == "74").Select(x => x.factura).Distinct().ToList();
                List<TruckViewModel> _TruckViewModelViewModel = new List<TruckViewModel>();


                foreach (var item in Listfact)
                {



                    TruckViewModel data = new TruckViewModel();
                    data = _FacturaEntrega.Where(x => x.camion == item && x.codvend.ToString() == "74").Select(x => new TruckViewModel
                    {
                        truck_numbre=x.camion,
                        truck_plate=x.placa
                    }).FirstOrDefault();
                    data.TruckDetails = _FacturaEntrega.Where(x => x.camion == item)
                          .GroupBy(l => l.codigoprod)
                        .Select(x => new TruckDetailViewModel
                        {
                            CodProduct = x.Key,
                            NameProduct = x.First().nombreprod,
                            stock = x.Sum(pc => pc.cantidad),


                        }).ToList();

                    _TruckViewModelViewModel.Add(data);



                }



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
        #endregion

    }
}