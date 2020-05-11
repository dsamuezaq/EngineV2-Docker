using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.SystemViewModel;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engine_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCampaignController : AController<TaskCampaignController>
    {
        private readonly TaskCampaignBusiness _TaskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        public TaskCampaignController(ILogger<TaskCampaignController> logger,
                                            RedisCache distributedCache,
                                            ChariotContext _chariotContext) : base(_chariotContext, distributedCache)
    {
        _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache);
        _logger = logger;
    }



        [HttpGet]
        [Route("GetTasks")]
        public ReplyViewModel GetTasks([FromBody] RequestViewModel _request)
    {
            if (Isvalidauthtoken(_request.token)) {
                reply.status = "A-OK";
                reply.messege = "Test";
            }
       
    
        return reply;
    }
        [HttpGet]
        [Route("GetTa")]
        public ReplyViewModel GetT()
        {
           

                reply.messege = "Test";
           


            return reply;
        }
    }

}
