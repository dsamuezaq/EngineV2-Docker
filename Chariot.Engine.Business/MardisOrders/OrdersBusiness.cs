using AutoMapper;
using Chariot.Engine.DataAccess.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Framework.Complement;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardisOrdersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.MardisOrders
{
    public class OrdersBusiness : ABusiness
    {
        protected OrdersDao _ordersDao;
        public OrdersBusiness(ChariotContext _chariotContext,
                                    RedisCache distributedCache,
                                    IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {
            _ordersDao = new OrdersDao(_chariotContext);

        }


        public List<VendedoresViewModel> GetVentas()
        {
            
            List<VendedoresViewModel> mapperVendedores = _mapper.Map< List <Salesman> , List <VendedoresViewModel>>(_ordersDao.SelectEntity<Salesman>());
            return mapperVendedores;



        }
        public List<RubrosViewModel> GetRubros()
        {

            List<RubrosViewModel> mapperRubros = _mapper.Map< List<RubrosViewModel>>(_ordersDao.SelectEntity<Items>());
            return mapperRubros;

        }
        public List<RubrosViewModel> GetClientes()
        {

            List<RubrosViewModel> mapperRubros = _mapper.Map<List<RubrosViewModel>>(_ordersDao.SelectEntity<Client>());
            return mapperRubros;

        }

        public List<ArticulosViewModel> GetArticulos()
        {

            List<ArticulosViewModel> mapperRubros = _mapper.Map<List<ArticulosViewModel>>(_ordersDao.SelectEntity<Product>());
            return mapperRubros;

        }

        public List<DepositosViewModel> GetDepositos()
        {

            List<DepositosViewModel> mapperRubros = _mapper.Map<List<DepositosViewModel>>(_ordersDao.SelectEntity<Deposit>());
            return mapperRubros;

        }
    }
}