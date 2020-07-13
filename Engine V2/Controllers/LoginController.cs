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
using Microsoft.AspNetCore.Authorization;
using Chariot.Framework.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace Engine_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : AController<LoginController>
    {
        #region Variables
        private readonly TaskCampaignBusiness _TaskCampaignBusiness;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger,
                                            RedisCache distributedCache
                                            , IOptions<AppSettings> appSettings,
                                            ChariotContext _chariotContext) : base(_chariotContext, distributedCache, appSettings)
        {
            _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache);
            _logger = logger;
        }
        #endregion

        #region APIs
        #region autenticacion by bd
            /// <summary>
            /// // POST: api/Login/Authenticate
            /// </summary>
            /// <param name="userLogin"></param>
            /// <returns>token valid</returns>
            [HttpPost]
            [AllowAnonymous]
            [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(LoginViewModel userLogin)
            {
                //if (ModelState.IsValid)
                //{
                    userLogin.Email= "sertecomcell@prospeccionclaro.com.ec";
                    userLogin.Password = "00";
                    var passwordHasher = new PasswordHasher<AppUser>();
                    var user = new AppUser();
                    var hashedPassword = passwordHasher.HashPassword(user, password);
            var _userInfo =  auth(userLogin.Email, userLogin.Password);
               
                    if (_userInfo != null)
                    {
                        _userInfo.token = generateJwtToken(_userInfo);
                           return Ok(_userInfo);
                    
                    }
                    else
                    {
                        return Unauthorized();
                    }

                //}
         


            }

        #endregion

        #endregion



    }
}
