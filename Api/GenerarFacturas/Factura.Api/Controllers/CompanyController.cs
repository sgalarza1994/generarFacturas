using LayerBusiness.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Home;
using ViewModel.Security;
using ViewModel.Validator;

namespace Factura.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public ICompanyService CompanyService { get; }
        public CompanyController(ICompanyService companyService)
        {
            CompanyService = companyService;
        }


        [HttpGet("[action]/{userId}/{isAdmin}")]
        public async Task<ActionResult<Response<List<CompanyEditRequest>>>> GetCompany(int userId,bool isAdmin)
        {

            var response = new Response<List<CompanyEditRequest>>();
            try
            {
                response = await CompanyService.Get(userId, isAdmin);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                
            }
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> AddCompany(CompanyEditRequest request)
        {

            var response = new Response();
            try
            {
                response = ValidatorCustom<CompanyEditRequest>.Validator(request);
                if(response.Success)
                    response = await CompanyService.Edit(request);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

            }
            return Ok(response);
        }

    }
}
