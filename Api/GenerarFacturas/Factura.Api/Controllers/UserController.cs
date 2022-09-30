using LayerBusiness.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModel.Security;
using ViewModel.User;
using ViewModel.Validator;

namespace Factura.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService UserService { get; }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> CreateUser(CreateUserRequest request)
        {
            var response = new Response();
            try
            {
                var validator = ValidatorCustom<CreateUserRequest>.Validator(request);
                if (validator.Success)
                {
                    response = await UserService.CreateUser(request);
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Response<LoginUserResponse>>> LoginUser(LoginUserRequest request)
        {
            var response = new Response<LoginUserResponse>();
            try
            {
                var validator = ValidatorCustom<LoginUserRequest>.Validator(request);
                if (validator.Success)
                {
                    response = await UserService.LoginUser(request);
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
