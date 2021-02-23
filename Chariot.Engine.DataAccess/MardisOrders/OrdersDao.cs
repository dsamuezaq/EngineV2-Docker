using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.Helpers;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Framework.Resources;
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
                foreach (var x in _data)
                {
                    Context.Orders.Add(x);
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
                var _data = Context.ProductOrders.Where(x => x.Idaccount == account && x.StatusRegister==CStatusRegister.Active);

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

        public Boolean GuardarfacturaEntregadas(String CodigoLocal, int NumeroFactura, string cO_observacion, string cO_estado ,double lat, double lon)
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



    }
}
