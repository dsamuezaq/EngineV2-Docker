using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engine_V2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : AController<WeatherForecastController>
    {

  

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly TaskCampaignBusiness _TaskCampaignBusiness;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(    ILogger<WeatherForecastController> logger,
                                            RedisCache distributedCache, 
                                            ChariotContext _chariotContext) : base(_chariotContext,distributedCache)
        {
            _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache);
            _logger = logger;
         }


   
        [HttpGet]
        public object Get()
        {
            //39C09A41-2488-499D-A32A-01D986297E21
           // var Td = GetUserToken("sertecomcell@prospeccionclaro.com.ec", "00");
            //var Td =_distributedCache.Get<List<object>>("StatusTask");
            var d = "";
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return d;
        }
    }
}
