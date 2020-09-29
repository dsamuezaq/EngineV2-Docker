using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisOrders;
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
                var _data = Context.ProductOrders.Where(x => x.Idaccount == account);

                return _data.Count() > 0 ? _data.ToList() : null;
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
          
        }

    }
}
