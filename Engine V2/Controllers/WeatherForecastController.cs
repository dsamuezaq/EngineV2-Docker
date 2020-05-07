using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Engine.DataObject;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engine_V2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : AController<WeatherForecastController>
    {

        private readonly TaskCampaignBusiness _TaskCampaignBusiness;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
                                        , ChariotContext _chariotContext) : base(_chariotContext)
        {
            _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext);
            _logger = logger;
        }


        [HttpGet]
        public List<int> Get()
        {
        
              var d=   _TaskCampaignBusiness.Get();
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
