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
    }
}
