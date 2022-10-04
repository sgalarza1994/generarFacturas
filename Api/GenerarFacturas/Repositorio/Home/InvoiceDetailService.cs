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
    public interface IInvoiceDetailService
    {
        Task<Response> Add(List<InvoiceDetailRequest> request);
        Task<Response<List<InvoiceDetailRequest>>> Get(int invoiceId);
    }
    public class InvoiceDetailService : IInvoiceDetailService
    {
        #region property
        public InvoiceContext Database { get; }
        #endregion
        public InvoiceDetailService(InvoiceContext database)
        {
            Database = database;

        }

        public async Task<Response> Add(List<InvoiceDetailRequest> request)
        {

            int invoiceId = request.FirstOrDefault().InvoiceId;

            var items = await Database.InvoiceDetails.Where(x => x.InvoiceId == invoiceId).ToListAsync();
            if(items.Count > 0)
            {
                Database.RemoveRange(items);
                await Database.SaveChangesAsync();
            }


            var invoiceDetail = request.Select(s=> new InvoiceDetail
            {
                Amount = s.Amount,
                Description = s.Description,
                InvoiceId = s.InvoiceId,
                Tax = s.Tax,
                UnitPrice = s.UnitPrice
                
            });
            await Database.InvoiceDetails.AddRangeAsync(invoiceDetail);
            await Database.SaveChangesAsync();
            return new Response { Success = true, Message = "Proceso exitoso" };
        }

        public async Task<Response<List<InvoiceDetailRequest>>> Get(int invoiceId)
        {
            var list = await Database.InvoiceDetails.Where(x => x.InvoiceId == invoiceId)
                    .Select(s => new InvoiceDetailRequest
                    {
                        Amount = s.Amount,
                        Description = s.Description,
                        InvoiceDetailId = s.InvoiceDetailId,
                        InvoiceId = s.InvoiceId,
                        Tax = s.Tax,
                        UnitPrice = s.UnitPrice

                    }).ToListAsync();


            return new Response<List<InvoiceDetailRequest>> { Success = true, Result = list };
        }
    }
}
