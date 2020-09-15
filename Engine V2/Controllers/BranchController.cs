using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.Business.MardisOrders;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardiscoreViewModel;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Engine_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : AController<BranchController>
    {

        #region Variable
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        // private readonly IMapper _mapper;
        public BranchController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);

        }
        #endregion
        #region APIs
        [HttpGet]
        [Route("RouteBranch")]
        public async Task<IActionResult> Get( int idaccount , string iddevice)
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_taskCampaignBusiness.GetBranches(idaccount,iddevice));
        }

        [HttpGet]
        [Route("Campaign")]
        public async Task<IActionResult> Campaign(int idaccounte)
        {
            reply.messege = "No puedo guardar la ubicación del encuestador";
            reply.status = "Fail";

            return Ok(_taskCampaignBusiness.GetCampanigAccount());
        }


        #endregion
    }
}
