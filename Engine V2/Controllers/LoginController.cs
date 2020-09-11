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
using AutoMapper;
using Chariot.Framework.MardisSecurityViewModel;

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
                                            ChariotContext _chariotContext, IMapper mapper) : base(_chariotContext, distributedCache, appSettings, mapper)
        {
            _TaskCampaignBusiness = new TaskCampaignBusiness(_chariotContext, distributedCache, mapper);
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
            if (ModelState.IsValid)
            {
                var _userInfo = auth(userLogin.Email, userLogin.Password);
                if (_userInfo != null)
                {
                 
                    return Ok(generateJwtToken(_userInfo));
                }
                else
                {
                    return Unauthorized();
                }

            }
            return Unauthorized();

        }
        [AllowAnonymous]
        [Route("Auth")]
        public async Task<IActionResult> Auth(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var _userInfo = auth(userLogin.Email, userLogin.Password);
                if (_userInfo != null)
                {
                    Token token = new Token();
                    token.token = generateJwtToken(_userInfo);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }

            }
            return Unauthorized();

        }
        [AllowAnonymous]
        [Route("Auth2")]
        public async Task<IActionResult> Auth2()
        {
            Token token = new Token();
             token.token = "das";
              return Ok(token); 

        }

        [AllowAnonymous]
        [Route("AuthEngineV2")]
        public async Task<IActionResult> Auth3(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var _userInfo = auth(userLogin.Email, userLogin.Password);
                if (_userInfo != null)
                {
                    Token token = new Token();
                    token.token = generateJwtTokenEngine(_userInfo);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }

            }
            return Unauthorized();

        }

        [HttpPost]
        [Authorize]
        [Route("GetMenubyProfile")]
        public async Task<IActionResult> GetMenubyProfile(string idprofile)
        {
            try
            {
            
              

                return Ok(_userBusiness.GetUserMenu(idprofile)); ;
            }
            catch (Exception e)
            {

                reply.error = e.Message;
                reply.status = "Error";
                return Ok(reply);
            }
            return Unauthorized();

        }
        [HttpPost]
        [Authorize]
        [Route("Register")]
        public async Task<IActionResult> Register( string password)
        {
             byte[] passwordHash, passwordSalt;
             CreatePasswordHash(password, out passwordHash, out passwordSalt);
             return Ok(passwordHash);
        }

        #endregion
        #region Methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion

        #endregion



    }
}
