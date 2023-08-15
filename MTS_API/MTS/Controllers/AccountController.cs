using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MTS.CommonLibrary.Email.Abstraction;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.ServiceInterface.Identity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MTS.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly IMTSLogger _logger;
        private readonly ITokenService _tokenService;

        public AccountController(IMapper mapper, IEmailSender emailSender, IUserService userService, IMTSLogger logger, ITokenService tokenService)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _userService = userService;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestModel model)
        {

            IdentityResult result;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _mapper.Map<UserResponseModel>(model);

                bool userNameExistsAlready = await _userService.IsValidUser(model.UserName).ConfigureAwait(false);

                if (!userNameExistsAlready)
                {
                    return Ok(new ReturnStatus
                    {
                        Status = false,
                        Message = "Account with this username already exist"
                    });
                }
                bool userEmailExistsAlready = await _userService.IsValidEmail(model.Email).ConfigureAwait(false);
                if (!userEmailExistsAlready)
                {
                    return Ok(new ReturnStatus
                    {
                        Status = false,
                        Message = "Account with this email already exist"
                    });
                }

                user.MailActivationTime = DateTime.UtcNow;
                result = await _userService.CreateAsync(user, model.Role, model.Password).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    var recentUser = await _userService.FindByEmailAsync(model.Email).ConfigureAwait(false);
                    // await SendConfirmationEmail(user).ConfigureAwait(false);
                    //_logger.Information("Exit from method : AccountController.Register");
                    return Ok(new Response { IsError = false, Data = recentUser, Message = "Success" });
                }
                else
                {
                    //_logger.Information("Exit from method : AccountController.Register");
                    return BadRequest("Validation error");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> ValidateAndGetToken([FromBody]LoginRequestModel loginRequest)
        {
            var token =await  _tokenService.GenerateToken(loginRequest.UserName, loginRequest.Password);

            if (token == null)
            {
                return Unauthorized();
            }
         

            return Ok(token);
        }


    }

}
