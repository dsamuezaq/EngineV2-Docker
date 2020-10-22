using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.MardisOrders;
using Chariot.Engine.Business.Redis;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Engine_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : AController<RedisController>
    {
        #region Variable
        private readonly RedisBusiness _redisBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;
        public RedisController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper, IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _redisBusiness = new RedisBusiness(_chariotContext, distributedCache, mapper);
            _Env = envrnmt;
        }
        [HttpPost]
        [Route("GetBankBG")]
        public async Task<IActionResult> GetBankBG()
        {
            return Ok(_redisBusiness.DataBankBG());
        }

        [HttpPost]
        [Route("GetServiceType")]
        public async Task<IActionResult> GetServiceType(string type)
        {


            return Ok(_redisBusiness.ServiciosGet(type));
        }

        [HttpPost]
        [Route("POSTServiceDetailbyType")]
        [Authorize]
        public async Task<IActionResult> GetServiceDetailbyType(string type)
        {


            return Ok(_redisBusiness.ServiciosGet(type));
        }

        [HttpPost]
        [Route("POSTListBankBG")]
        [Authorize]
        public async Task<IActionResult> POSTListBankBG()
        {
            return Ok(_redisBusiness.DataBankBG());
        }
        #endregion
    }
}