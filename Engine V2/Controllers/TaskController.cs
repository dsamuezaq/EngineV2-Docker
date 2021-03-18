﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chariot.Engine.Business.Mardiscore;
using Chariot.Framework.CampaingViewModels;
using Engine_V2.Libraries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Chariot.Framework.SystemViewModel;
using Chariot.Framework.Helpers;
using Chariot.Framework.TaskViewModel;
using Chariot.Framework.Complement;
using Microsoft.Extensions.Options;
using Chariot.Engine.DataObject;
using Microsoft.AspNetCore.Http;
using Chariot.Framework.MardiscoreViewModel.Route;

namespace Engine_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : AController<TaskController>
    {

        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly ILogger<TaskCampaignController> _logger;
        private IHostingEnvironment _Env;
        // private readonly IMapper _mapper;

        public TaskController(ILogger<OrderController> logger,
                                           RedisCache distributedCache
                                           , IOptions<AppSettings> appSettings,
                                           ChariotContext _chariotContext, IMapper mapper, IHostingEnvironment envrnmt) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);
            _Env = envrnmt;
        }

        [HttpPost]
        [Route("getFactura")]
        public async Task<IActionResult> GetFacturaNutri(GetFacturaViewModel _data)
        {
            FactNutriMasiveModel _modelVue = new FactNutriMasiveModel();
            FactNutriViewModel data = new FactNutriViewModel();

            var model = _taskCampaignBusiness.GetListFactLoad(_data.urlfile);

            //_modelVue.idaccount = ApplicationUserCurrent.AccountId;
            //_modelVue.iduser = Guid.Parse(ApplicationUserCurrent.UserId);
            _modelVue.Listbills = model.ToList();
            var regCarga = _modelVue.Listbills.Select(z => z.bardcode).Distinct().Count();

            ReplyViewModel reply = new ReplyViewModel();

            reply.status = "Ok";
            reply.messege = "Procesado";
            reply.data = _modelVue;

            return Ok(reply);
        }

        [HttpPost]
        public async Task<IActionResult> LoadProductNutriAxios(string idpath, string account, string iduser, string option, string campaign, string status)
        {
            reply.status = "Ok";
            reply.messege = "Local Guardado";
            
            try
            {
                var model = JSonConvertUtil.Deserialize<FactNutriViewModel>(idpath);
                List<FactNutriViewModel> _model = new List<FactNutriViewModel>();
                _model.Add(model);

                Guid idAccount = Guid.Parse(account);
                //var data = _taskCampaignBusiness.DataFactXml(_model, idAccount, iduser, option, campaign, status);

                //reply.data = data;
                return Ok(reply);
            }
            catch (Exception e)
            {


                IList<TaskMigrateResultViewModel> data = new List<TaskMigrateResultViewModel>();
                data.Add(new TaskMigrateResultViewModel { description = e.Message, Element = "0", Code = "0" });
                var rows = from x in data
                           select new
                           {
                               description = x.description,
                               data = x.Element,
                               Code = x.Code

                           };

                var jsondata = rows.ToArray();
                reply.data = jsondata;
                return Ok(reply);

            }
        }


    }
}