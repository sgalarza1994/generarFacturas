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
    public class InvoiceController : ControllerBase
    {
        public InvoiceController(IInvoiceService invoiceService,IPdfService pdfService)
        {
            InvoiceService = invoiceService;
            PdfService = pdfService;
        }

        public IInvoiceService InvoiceService { get; }
        public IPdfService PdfService { get; }

        [HttpGet("[action]/{userId}/{isAdmin}")]
        public async Task<ActionResult<Response<List<InvoiceResponse>>>> GetInvoices(int userId, bool isAdmin)
        {

            var response = new Response<List<InvoiceResponse>>();
            try
            {
                response = await InvoiceService.Get(userId, isAdmin);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

            }
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> AddInvoice(InvoiceRequest request)
        {

            var response = new Response();
            try
            {
                response = ValidatorCustom<InvoiceRequest>.Validator(request);
                if (response.Success)
                    response = await InvoiceService.Add(request);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

            }
            return Ok(response);
        }

        [HttpGet("[action]/{invoiceId}")]
        public async Task<ActionResult<Response>> GenerarPdf(int invoiceId)
        {
            return Ok(await PdfService.GenerarPdf(invoiceId));
        }
    }
}
