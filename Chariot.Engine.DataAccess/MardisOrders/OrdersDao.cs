using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Engine.DataObject.MardisOrders.Vistas;
using Chariot.Framework.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisOrders
{
    public class OrdersDao : ADao
    {
        public OrdersDao(ChariotContext context) : base(context)
        {

        }


        public bool SaveDataPedido(List<Order> _data) {
            try
            {
                int idVende = 0;
                foreach (var x in _data)
                {
                    Context.Orders.Add(x);
                    
                    //Nutri
                    if (x.Idaccount == 13) {

                        IQueryable<Salesman> vendedor = Enumerable.Empty<Salesman>().AsQueryable();
                        vendedor = Context.Salesmans.Where(v => v.idVendedor == x.idVendedor && v.idaccount == x.Idaccount);

                        if (vendedor.Count() > 0) {
                            idVende = vendedor.First().id;
                        }

                        foreach (var detalle in x.pedidosItems)
                        {
                            IQueryable<Product> producto = Enumerable.Empty<Product>().AsQueryable();
                            Movil_Warenhouse movilw = new Movil_Warenhouse();

                            producto = Context.ProductOrders.Where(p => p.IdArticulo == detalle.idArticulo);
                            

                            movilw.BALANCE = detalle.cantidad;
                            movilw.DESCRIPTION = "Venta";
                            movilw.IDVENDEDOR = vendedor.Count() > 0 ? vendedor.First().id : 0;
                            movilw.IDPRODUCTO = producto.Count() > 0 ? producto.First().Id : 0;
                            movilw.MOVEMENT = "-1";
                            //Guardar
                            Context.Movil_Warenhouses.Add(movilw);
                        }
                    }
                }
                Context.SaveChanges();
                Context.Query<string>($@"EXEC dbo.sp_actualiza_movil_warehouse_app @idvendedor = {idVende}");
                //db.PEDIDOS.Add(pEDIDOS);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

          
        }
        public double SaveDataPedidoI(List<Order> _data)
        {
            try
            {
                return _data.First().Idaccount == 15 ? GuardarPedidoIndustrial(_data) : GuardarPedidosutri(_data);
            }
            catch (Exception)
            {

                return -2.0;
            }
               

        }

        double GuardarPedidoIndustrial(List<Order> _data)
        {
            try
            {
                var databaseTable = Context.Branches.Where(x => x.ExternalCode == _data.First().codCliente && x.IdAccount == _data.First().Idaccount);
                string codigonuevo = "";
                if (databaseTable.Count() > 0) {
       
                    codigonuevo = VerificaionBranch(databaseTable.FirstOrDefault());
                    if (codigonuevo != "")
                       _data.First().codCliente = codigonuevo;
                }
                String json = null;
                foreach (Order orden in _data)
                {

               
                    List<PedidosAPI> Post = new List<PedidosAPI>();
                    try
                    {
                        int order = 0;
                        foreach (var itemorden in orden.pedidosItems)
                        {
                            order = order + 1;
                            Post.Add(new PedidosAPI
                            {

                                p_PEDIDO = int.Parse(orden.codigoext),
                                p_ORDEN = order,
                                p_CANTIDAD = (int)itemorden.cantidad,
                                p_FECHA = int.Parse(orden.fecha),
                                p_CLIENTE = int.Parse(orden.codCliente),
                                p_PRODUCTO = itemorden.idArticulo,
                                p_PRECIO =(decimal) itemorden.importeUnitario,//(decimal)Context.ProductOrders.Where(x => x.IdArticulo == itemorden.idArticulo && x.StatusRegister == "A" && x.Idaccount == orden.Idaccount).FirstOrDefault().Precio1,
                                p_VENDEDOR = int.Parse(orden.idVendedor),
                                p_ESTADO = 0,
                                p_PEDIDO_MARDIS = orden.p_PEDIDO_MARDIS,
                                p_NUEVO_CLIENTE = orden.codCliente,
                                p_FORMA_PAGO= itemorden.formapago

                            }); ; ;

                        }
                         json = JsonConvert.SerializeObject(Post);

                        HelpersHttpClient _helpersHttpClientBussiness = new HelpersHttpClient();
                        var EstadoRespuestaCrearClienteIM = Task.Factory.StartNew(() =>
                        {
                            return _helpersHttpClientBussiness.PostApi("pedidocobertura/agregarlista", json);
                        });

                        var respuesta = EstadoRespuestaCrearClienteIM.Result.Result;
                        if (respuesta)
                        {

                            json = null;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    orden.errorenvio = json;
                    Context.Orders.Add(orden);
                    Context.SaveChanges();

                }


                //db.PEDIDOS.Add(pEDIDOS);
                return 1.0;
            }
            catch (Exception ex)
            {
                return -2.0;
            }

        }

        String VerificaionBranch(Branch local) {

            if (local.CommentBranch == "nuevo") {

                if (local.ExternalCode == local.Id.ToString())
                {
                    return "";
                }
                else {
                    local.ExternalCode = local.Id.ToString();
                    Context.Branches.Update(local);
                    Context.SaveChanges();
                    return local.ExternalCode;
                }
            
            }

            return "";
        }

        double GuardarPedidosutri(List<Order> _data) {
            try
            {
                int idVende = 0;
                foreach (var x in _data)
                {
                    Context.Orders.Add(x);
                    Context.SaveChanges();
                    //Nutri
                    if (x.Idaccount == 13)
                    {

                        IQueryable<Salesman> vendedor = Enumerable.Empty<Salesman>().AsQueryable();
                        vendedor = Context.Salesmans.Where(v => v.codigoDeValidacion == x.idVendedor && v.idaccount == x.Idaccount);

                        if (vendedor.Count() > 0)
                        {
                            idVende = vendedor.First().id;
                        }

                        foreach (var detalle in x.pedidosItems)
                        {
                            IQueryable<Product> producto = Enumerable.Empty<Product>().AsQueryable();
                            Movil_Warenhouse movilw = new Movil_Warenhouse();

                            producto = Context.ProductOrders.Where(p => p.IdArticulo == detalle.idArticulo);


                            movilw.BALANCE = detalle.cantidad;
                            movilw.DESCRIPTION = "Venta";
                            movilw.IDVENDEDOR = vendedor.Count() > 0 ? vendedor.First().id : 0;
                            movilw.IDPRODUCTO = producto.Count() > 0 ? producto.First().Id : 0;
                            movilw.MOVEMENT = "-1";
                            //Guardar
                            Context.Movil_Warenhouses.Add(movilw);
                        }
                    }
                }

                Context.Query<string>($@"EXEC dbo.sp_actualiza_movil_warehouse_app @idvendedor = {idVende}");
                //db.PEDIDOS.Add(pEDIDOS);
                return 1.0;
            }
            catch (Exception ex)
            {
                return -2.0;
            }

        }
        public bool SaveDataDevolucion(List<Devolucion> _data)
        {
            try
            {
                foreach (var x in _data)
                {
                    Context.Devoluciones.Add(x);
                }
                Context.SaveChanges();
                //db.PEDIDOS.Add(pEDIDOS);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        public bool GuardarRegistroVisita(List<Visitas> DatosVisitaEntidad)
        {
            try
            {
                foreach (var vista in DatosVisitaEntidad)
                {
                    Context.RegistroVisitalocales.Add(vista);
                }
                Context.SaveChanges();
                //db.PEDIDOS.Add(pEDIDOS);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public int GetLastCode()
        {
            try
            {
               var a= Context.SequenceOrders.OrderByDescending(x=>x.Id).FirstOrDefault().Id;
                //db.PEDIDOS.Add(pEDIDOS);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }


        }
        public bool SaveDataInventarios(List<Inventory> _data)
        {
            try
            {
                foreach (var x in _data)
                {
                    Context.Inventories.Add(x);
                }
                Context.SaveChanges();
                //db.PEDIDOS.Add(pEDIDOS);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public List<Product> GetProductByIdaccount(int account)
        {
            try
            {
                var _data = Context.ProductOrders.Where(x => x.Idaccount == account && x.StatusRegister!=CStatusRegister.Repetido);

                return _data.Count() > 0 ? _data.ToList() : null;
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
          
        }

        public bool InactiveProductById(int Id)
        {
            try
            {
                var _data = Context.ProductOrders.Where(x => x.Id == Id);
                if (_data.Count() > 0) {

                    var _table = _data.First();
                    _table.StatusRegister = CStatusRegister.Delete;
                    return InsertUpdateOrDelete(_table, "U");
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
                return false;
            }

        }
        public bool ActiveProductById(int Id)
        {
            try
            {
                var _data = Context.ProductOrders.Where(x => x.Id == Id);
                if (_data.Count() > 0)
                {

                    var _table = _data.First();
                    _table.StatusRegister = CStatusRegister.Active;
                    return InsertUpdateOrDelete(_table, "U");
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
                return false;
            }

        }
        public bool GuardardevolucionFactura(List<DevolucionFactura> _data)
        {
            try
            {
                foreach (var x in _data)
                {
                    Context.DevolucionFacturas.Add(x);
                }
                Context.SaveChanges();
                //db.PEDIDOS.Add(pEDIDOS);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public List<Product> DAOObtenerProductoXCodigo(string CodigoProducto)
        {
            try
            {
               var DatoProducto = Context.ProductOrders.Where(x => x.IdArticulo == CodigoProducto);
               return DatoProducto.Count() > 0 ? DatoProducto.ToList(): null;

            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public List<vw_pagoxcarteraDevolucion_factura> ConsultarDatosDePagosCarteraXfactura(int factura)
        {
            try
            {
                var ResultadoCartera = Context.pagoxcarteraDevolucion_factura.Where(x => x.cO_FACTURA.ToString().Equals(factura.ToString()) );
                return ResultadoCartera.Count() > 0 ? ResultadoCartera.ToList() : null;

            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public Boolean ConsularFacturaEntregada(int NumeroFactura)
        {
            try
            {
                var ResultadoCartera = Context.FacturasEntregadas.Where(x => x.cO_FACTURA == NumeroFactura);
                return ResultadoCartera.Count() > 0 ? true : false;

            }
            catch (Exception ex)
            {

                throw;
                return false;
            }

        }
        #region APP entregas
        public String GetPollsteruserCell(string Iddevice, int account)
        {
            try
            {
                var _data = Context.Pollsters.Where(x => x.IMEI == Iddevice && x.idaccount== account);
                if (_data.Count() > 0)
                {

                    var _table = _data.First();

                    return _table.UserCel;
                }

                return"";
            }
            catch (Exception ex)
            {

                throw;
                return "";
            }

        }

        /// <summary>
        /// Obtiene el registro de locales
        /// </summary>
        /// <param name="code"></param>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public List<DeliveryBranches> GetBranchbyListCode(List<string> code, int idAccount)
        {

            try
            {

                var consulta1 = from b in Context.Branches
                                join p in Context.Persons on b.IdPersonOwner equals p.Id
                                where code.Contains(b.Code)  && b.IdAccount == idAccount
                                select new DeliveryBranches
                                {
                                    Id=b.Id,
                                    Code = b.Code,
                                    ExternalCode = b.ExternalCode,
                                    Name = b.Name,
                                    Neighborhood = b.Neighborhood,
                                    MainStreet = b.MainStreet,
                                    SecundaryStreet = b.SecundaryStreet,
                                    NumberBranch = b.NumberBranch,
                                    LatitudeBranch = b.LatitudeBranch,
                                    LenghtBranch = b.LenghtBranch,
                                    Reference = b.Reference,

                                    TypeBusiness = b.TypeBusiness,
                                    ClientName = p.Name,
                                    TypeDocument = p.TypeDocument,
                                    Document = p.Document,
                                    Phone = p.Phone,
                                    Mobile = p.Mobile


                                };



                var _model = consulta1.ToList();


                if (_model.Count() > 0)
                {
                    return _model;

                }
                else
                {
                    return null;

                }


            }
            catch (Exception e)
            {

                throw new Exception("En locales");
            }

        }
        public DeliveryBranches GetBranchbyCode(List<string> code, int idAccount)
        {

            try
            {

                var consulta1 = from b in Context.Branches
                                join p in Context.Persons on b.IdPersonOwner equals p.Id
                                where code.Contains(b.Code) && b.IdAccount == idAccount
                                select new DeliveryBranches
                                {
                                    Code = b.Code,
                                    ExternalCode = b.ExternalCode,
                                    Name = b.Name,
                                    Neighborhood = b.Neighborhood,
                                    MainStreet = b.MainStreet,
                                    SecundaryStreet = b.SecundaryStreet,
                                    NumberBranch = b.NumberBranch,
                                    LatitudeBranch = b.LatitudeBranch,
                                    LenghtBranch = b.LenghtBranch,
                                    Reference = b.Reference,

                                    TypeBusiness = b.TypeBusiness,
                                    ClientName = p.Name,
                                    TypeDocument = p.TypeDocument,
                                    Document = p.Document,
                                    Phone = p.Phone,
                                    Mobile = p.Mobile


                                };



                var _model = consulta1.ToList();


                if (_model.Count() > 0)
                {
                    return _model.First();

                }
                else
                {
                    return null;

                }


            }
            catch (Exception e)
            {

                throw new Exception("En locales");
            }

        }

        public Boolean GuardarfacturaEntregadas(String CodigoLocal, int NumeroFactura, string cO_observacion, string cO_estado ,double lat, double lon,int enviado)
        {

            try
            {
                FacturasEntregadas _DatoDeFacturaEntregada = new FacturasEntregadas();
                _DatoDeFacturaEntregada.cO_FACTURA = NumeroFactura;
                _DatoDeFacturaEntregada.cO_CODCLI = CodigoLocal;
                _DatoDeFacturaEntregada.cO_observacion = cO_observacion;
                _DatoDeFacturaEntregada.cO_estado = cO_estado;
                _DatoDeFacturaEntregada.LAT = lat;
                _DatoDeFacturaEntregada.LON = lon;
                _DatoDeFacturaEntregada.Enviado = enviado;
                Context.FacturasEntregadas.Add(_DatoDeFacturaEntregada);
                Context.SaveChanges();




                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("En locales");
            }

        }

        public Boolean GuardarPagoDeCartera(PagoCartera DatoPagoCartera)
        {

            try
            {
             
                Context.PagoCarteras.Add(DatoPagoCartera);
                Context.SaveChanges();




                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("En locales");
            }

        }

        public Boolean GuardarP(PagoCartera DatoPagoCartera)
        {

            try
            {

                Context.PagoCarteras.Add(DatoPagoCartera);
                Context.SaveChanges();




                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("En locales");
            }

        }
        #endregion

        #region App Bodega

        public List<vw_resumen_Stock_vendedor> ConsularVendedoresXDistribuidor(int idnumeroDisitribuidores)
        {
            try
            {

                var _dataTable = Context.Query<vw_resumen_Stock_vendedor>($@"SELECT cantidad,
                                                                                        id,
                                                                                        idVendedor,
                                                                                        nombre,
                                                                                        codigoDeValidacion,
                                                                                        Idaccount,
                                                                                        IDDISTRIBUTOR,
                                                                                        statusV 
                                                                                   FROM MardisOrders.vw_resumen_Stock_vendedor where IDDISTRIBUTOR={idnumeroDisitribuidores}");
                return _dataTable.Count() > 0 ? _dataTable.ToList() : null;
           

            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public List<vw_resumen_Stock_bodegaCentral> ConsularBodegaCentralXDistribuidor(int idnumeroDisitribuidores)
        {
            try
            {

                var _dataTable = Context.Query<vw_resumen_Stock_bodegaCentral>($@"SELECT nombre,
                                                                                   precio,
                                                                                   precioUnitario,
                                                                                   cantidad,
                                                                                   categoria,
                                                                                   IDDISTRIBUTOR,
                                                                                   IDPRODUCTO,
                                                                                   idcategoria,
                                                                                   barcode  FROM MardisOrders.vw_resumen_Stock_bodegaCentral where IDDISTRIBUTOR={idnumeroDisitribuidores}");
                return _dataTable.Count() > 0 ? _dataTable.ToList() : null;


            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public List<vw_resumen_Stock_bodegaCentral> ConsularBodegaCentralXCambion(int IdVendedor)
        {
            try
            {

                var _dataTable = Context.Query<vw_resumen_Stock_bodegaCentral>($@"SELECT nombre,
                                                                                   precio,
                                                                                   precioUnitario,
                                                                                   cantidad,
                                                                                   categoria,
                                                                                   IDDISTRIBUTOR,
                                                                                   IDPRODUCTO,
                                                                                   idcategoria,
                                                                                   barcode FROM MardisOrders.vw_resumen_Stock_bodegaCentral_CAMION where IDVENDEDOR={IdVendedor}");
                return _dataTable.Count() > 0 ? _dataTable.ToList() : null;


            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public Boolean GuardarBodegaMovil(int warehouseid, int productid, int quantity, int entregadorid, int userid, string comment)
        {
            try
            {

                Movil_Warenhouse movil_Warenhouse = new Movil_Warenhouse();
                movil_Warenhouse.IDVENDEDOR = entregadorid;
                movil_Warenhouse.BALANCE = quantity;
                movil_Warenhouse.DESCRIPTION = "INGRESO DE INVENTARIO APP";
                movil_Warenhouse.MOVEMENT = "1";
                movil_Warenhouse.IDPRODUCTO = productid;
                movil_Warenhouse.COMMENT = comment;
                Context.Movil_Warenhouses.Add(movil_Warenhouse);
                Context.SaveChanges();


                Central_Warenhouse central_Warenhouse = new Central_Warenhouse();
                central_Warenhouse.IDDISTRIBUTOR = warehouseid;
                central_Warenhouse.BALANCE = quantity;
                central_Warenhouse.DESCRIPTION = "INGRESO DE INVENTARIO APP";
                central_Warenhouse.MOVEMENT = "-1";
                central_Warenhouse.IDPRODUCTO = productid;

                Context.Central_Warenhouses.Add(central_Warenhouse);



                Context.SaveChanges();
                var _dataTable = Context.Query<List<int>>($@"EXEC dbo.sp_actuaiza_movil_warehouse_resume_APP @idvendedor ={entregadorid} ,@idproducto = { productid}  ,@iddistribuidor ={warehouseid}");
                return true;


            }
            catch (Exception ex)
            {

                throw;
                return false;
            }

        }

        public int DistribuidorID(Guid IdUsuario)
        {
            try
            {
                var Distribuidor = Context.Distributors.Where(x => x.Iduser == IdUsuario);
                return Distribuidor.Count() > 0 ? Distribuidor.First().IDDISTRIBUTOR :0;

            }
            catch (Exception ex)
            {

                throw;
                return 0;
            }

        }

        public int VendedorID(string Cedula)
        {
            try
            {
                var Vendedor = Context.Salesmans.Where(x => x.idVendedor ==Cedula);
                return Vendedor.Count() > 0 ? Vendedor.First().id : 0;

            }
            catch (Exception ex)
            {

                throw;
                return 0;
            }

        }
        public int ProductoID(string CodigoProducto, int idcuenta)
        {
            try
            {
                var  Producto = Context.ProductOrders.Where(x => x.IdArticulo ==CodigoProducto && x.Idaccount==13 && x.StatusRegister==CStatusRegister.Active);
                return Producto.Count() > 0 ? Producto.First().Id : 0;

            }
            catch (Exception ex)
            {

                throw;
                return 0;
            }

        }
        #endregion

    }
}
