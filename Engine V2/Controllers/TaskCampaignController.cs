using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.SystemViewModel;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Engine_V2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCampaignController : AController<TaskCampaignController>
    {
        private readonly TaskCampaignBusiness _TaskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        public TaskCampaignController(ILogger<LoginController> logger,
                                            RedisCache distributedCache
                                            , IOptions<AppSettings> appSettings,
                                            ChariotContext _chariotContext,  IMapper mapper) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
        _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);

    }



        [HttpGet]
        [Route("Get")]
        public ReplyViewModel Get([FromBody] RequestViewModel _request)
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
