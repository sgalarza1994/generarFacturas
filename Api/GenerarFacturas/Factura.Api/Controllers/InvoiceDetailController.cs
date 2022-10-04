using LayerBusiness.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Home;
using ViewModel.Security;

namespace Factura.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        public IInvoiceDetailService InvoiceDetailService { get; }

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
        {
            InvoiceDetailService = invoiceDetailService;
        }


        [HttpGet("[action]/{invoiceId}")]
        public async Task<ActionResult<Response<List<InvoiceDetailRequest>>>> GetInvoices(int invoiceId)
        {

            var response = new Response<List<InvoiceDetailRequest>>();
            try
            {
                response = await InvoiceDetailService.Get(invoiceId);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

            }
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> AddInvoiceDetail(List<InvoiceDetailRequest> request)
        {

            var response = new Response();
            try
            {
               
                    response = await InvoiceDetailService.Add(request);
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
