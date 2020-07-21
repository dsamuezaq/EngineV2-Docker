using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.SystemViewModel;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Engine_V2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : AController<TaskCampaignController>
    {

        #region Variable
        private readonly TrackingBusiness _TrackingBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
       // private readonly IMapper _mapper;
        public TrackingController(ILogger<LoginController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _TrackingBusiness = new TrackingBusiness(_chariotContext,distributedCache, mapper);

        }
        #endregion

        #region APIs
        [HttpPost]
        [Route("SaveStatusPerson")]
        public ReplyViewModel SaveStatusPerson([FromBody] TrackingViewModel _request)
        {
            _TrackingBusiness.SaveTracking(_request);
            reply.messege = "Success Save Data";
            reply.status = "Ok";
            return reply;
        }
        [HttpPost]
        [Route("SaveBranchTracking")]
        public async Task<IActionResult> SaveBranchTracking([FromBody] List<TrackingBranchViewModel>_request)
        {
            try
            {
                _TrackingBusiness.SaveTrackingBranch(_request);
                reply.messege = "Success Save Data";
                reply.status = "Ok";
                return Ok(reply); ;
            }
            catch (Exception e)
            {

                reply.error = e.Message;
                reply.status = "Error";
                return Ok(reply);
            }
        
        }

        [HttpPost]
        [Route("TrackingPersonalByCampaign")]
        public async Task<IActionResult> TrackingPersonalByCampaign([FromBody] GetTrackingViewModel _request)
        {
            try
            {
                reply = _TrackingBusiness.GetTracking(_request);
                return  Ok(reply);
            }
            catch (Exception e)
            {
                reply.error = e.Message;
                reply.status = "Error";
                return Ok(reply);
            }
          
        }
        #endregion

    }

}