using Chariot.Engine.DataObject;
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

    }
}
