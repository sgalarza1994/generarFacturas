using LayerAccess;
using LayerAccess.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Home;
using ViewModel.Security;

namespace LayerBusiness.Home
{
    public interface IInvoiceService
    {
        Task<Response> Add(InvoiceRequest request);
        Task<Response<List<InvoiceResponse>>> Get(int companyId, bool isAdmin);
    }
    public class InvoiceService : IInvoiceService
    {
        #region property
        public InvoiceContext Database { get; }
        #endregion
        public InvoiceService(InvoiceContext database)
        {
            Database = database;

        }

        public async Task<Response> Add(InvoiceRequest request)
        {
            var invoice = new Invoice
            {
                ClienteAddress = request.ClientAddress,
                ClientePhone = request.ClientPhone,
                ClientId = request.ClientId,
                ClientName = request.ClientName,
                CompanyId = request.CompanyId,
                InvoiceNumber = request.InvoiceNumber,
                Created  = DateTime.Now,
                Status   = "A",
            };

            await Database.Invoices.AddAsync(invoice);
            await Database.SaveChangesAsync();
            return new Response { Success = true, Message = "Proceso exitoso" };
        }

        public async Task<Response<List<InvoiceResponse>>> Get(int companyId, bool isAdmin)
        {
            List<InvoiceResponse> list = null;
            if(isAdmin)
            {
                list = await Database.Invoices.Include(x => x.Company).Include(x => x.Items)
                       .Select(x => new InvoiceResponse
                       {
                           ClientAddress = x.ClienteAddress,
                           ClientId = x.ClientId,
                           ClientName = x.ClientName,
                           ClientPhone = x.ClientePhone,
                           CompanyId = x.CompanyId,
                           CompanyName = x.Company.BusinessName,
                           InvoiceId = x.InvoiceId,
                           InvoiceNumber = x.InvoiceNumber,
                           Total = x.Items.Sum(s=>s.UnitPrice*s.Amount)
                       }).ToListAsync();
            }
            else
            {
                list = await Database.Invoices.Include(x => x.Company).Include(x => x.Items)
                    .Where(x=>x.CompanyId == companyId)
                      .Select(x => new InvoiceResponse
                      {
                          ClientAddress = x.ClienteAddress,
                          ClientId = x.ClientId,
                          ClientName = x.ClientName,
                          ClientPhone = x.ClientePhone,
                          CompanyId = x.CompanyId,
                          CompanyName = x.Company.BusinessName,
                          InvoiceId = x.InvoiceId,
                          InvoiceNumber = x.InvoiceNumber,
                          Total = x.Items.Sum(s => s.UnitPrice * s.Amount)
                      }).ToListAsync();
            }



            return new Response<List<InvoiceResponse>> { Success = true, Result = list };
        }
    }
}
