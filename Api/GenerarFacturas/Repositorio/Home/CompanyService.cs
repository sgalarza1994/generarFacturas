using LayerAccess;
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

    public interface ICompanyService
    {
        Task<Response> Edit(CompanyEditRequest request);
        Task<Response<List<CompanyEditRequest>>> Get(int userId, bool isAdmin);
    }
    public class CompanyService : ICompanyService
    {
        #region property
        public InvoiceContext Database { get; }
        #endregion
        public CompanyService(InvoiceContext database)
        {
            Database = database;
           
        }

        public async Task<Response> Edit(CompanyEditRequest request)
        {
            var company = await Database.Companies.FindAsync(request.CompanyId);
            if (company == null)
                return new Response { Success = false, Message = "No se encontro registro" };

            company.BusinessName = request.BusinessName;
            company.Address = request.Address;
            company.Email = request.Email;
            company.Phone = request.Phone;

            await Database.SaveChangesAsync();
            return new Response { Success = true, Message = "Proceso exitoso" };
        }

        public async Task<Response<List<CompanyEditRequest>>> Get(int userId, bool isAdmin)
        {
            List<CompanyEditRequest> list = null;

            if (isAdmin)
                list = await Database.Companies.Select(s => new CompanyEditRequest
                {
                    Address = s.Address,
                    BusinessName = s.BusinessName,
                    CompanyId = s.CompanyId,
                    Email  = s.Email,
                    Phone = s.Phone
                }).ToListAsync();
            else
                list = await Database.Companies
                    .Where(x=>x.UserId == userId)
                    .Select(s => new CompanyEditRequest
                {
                    Address = s.Address,
                    BusinessName = s.BusinessName,
                    CompanyId = s.CompanyId,
                    Email = s.Email,
                    Phone = s.Phone
                }).ToListAsync();

            return new Response<List<CompanyEditRequest>> { Success = true, Result = list };

        }
    }
}
