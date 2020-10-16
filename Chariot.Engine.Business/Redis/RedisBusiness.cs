using AutoMapper;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.RedisViewModel;
using Chariot.Framework.SystemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.Redis
{
   public class RedisBusiness : ABusiness
    {
       
        public RedisBusiness(ChariotContext _chariotContext,
                                    RedisCache distributedCache,
                                    IMapper mapper) : base(_chariotContext, distributedCache, mapper)
        {
       

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <returns>20200922</returns>
        public ReplyViewModel DataBankBG()
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                List<BancoBgViewModelReply> _model = new List<BancoBgViewModelReply>();
                reply.messege = "success";
               _model = _RedisCache.Get<List<BancoBgViewModelReply>>("_redisBancos");
                reply.status = "Ok";
                
                //var _Reply = _taskCampaignDao.GetStatusTask(_data.IdAccount).Select(x => new CampaignModelReply
                //{
                //    Id = x.Id,
                //    Name = x.Name
                //}).ToList(); ;
                reply.data = _model;
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <returns>20200922</returns>
        public ReplyViewModel ServiciosGet(string tpo)
        {
            ReplyViewModel reply = new ReplyViewModel();
            try
            {
                List<tb_bancos_descripcion> serviciosT = new List<tb_bancos_descripcion>(); ;
                try
                {
                    serviciosT = _RedisCache.Get<List<tb_bancos_descripcion>>("_redisBancosServ");
                    if (serviciosT == null)
                    {
                        reply.messege = "Cargar el modelo en redis";
                      
                        reply.status = "Fallo la consulta";
                        return reply;
                    }
                }
                catch (Exception)
                {
                    reply.messege = "Cargar el modelo en redis";

                    reply.status = "Fallo la consulta";
                    return reply;
                }

      
                List<ServiceModel> _data = new List<ServiceModel>(); ;
            foreach (var item in serviciosT.Where(x => x.codigo == tpo))
            {
                string titulo = "";
                List<CaracteristicasModel> _datac = new List<CaracteristicasModel>(); ;

                var descripcion = item.descripcion.Split('/');
                if (descripcion.Length > 1)
                {
                    titulo = descripcion[0];
                    var cart = descripcion[1].Split('-');

                    if (cart.Length > 0)
                    {

                        _datac.AddRange(cart.ToList().Select(x => new CaracteristicasModel { caract = x }));


                    }
                }
                if (descripcion.Length == 1 && descripcion[0] != "")
                {
                    var cart = descripcion[0].Split('-');

                    if (cart.Length > 0)
                    {

                        _datac.AddRange(cart.ToList().Select(x => new CaracteristicasModel { caract = x }));


                    }
                    else
                    {

                    }
                }


                _data.Add(new ServiceModel
                {
                    servicio = item.nombre,
                    descript = titulo,
                    caract = _datac


                });



            }


            reply.messege = "success";

                reply.status = "Ok";

            
                reply.data = _data;
                return reply;
            }
            catch (Exception e)
            {
                reply.messege = "No existen datos de campaña";
                reply.data = e.Message;
                reply.status = "Fallo la consulta";
                return reply;

            }
        }
    }
}
