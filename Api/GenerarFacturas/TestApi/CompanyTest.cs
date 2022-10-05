using Factura.Api.Controllers;
using LayerBusiness.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Home;
using ViewModel.Security;

namespace TestApi
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public async Task GetCompany()
        {
            List<CompanyEditRequest> companies = new List<CompanyEditRequest>
            {
                new CompanyEditRequest
                {
                    Address = "",
                    BusinessName = "Empresa 1",
                    CompanyId = 1,
                    Email = "empresa1@hotmail.com",
                    Phone = "",
                    Ruc = "empresa1"
                },
                new CompanyEditRequest
                {
                    Address = "",
                    BusinessName = "Empresa 2",
                    CompanyId = 2,
                    Email = "empresa2@hotmail.com",
                    Phone = "",
                    Ruc = "empresa2"
                },
                 new CompanyEditRequest
                {
                    Address = "",
                    BusinessName = "Empresa 3",
                    CompanyId =3,
                    Email = "empresa3@hotmail.com",
                    Phone = "",
                    Ruc = "empresa3"
                }
            };


            Response<List<CompanyEditRequest>> response = new Response<List<CompanyEditRequest>>
            {
                Success = true,
                Result = companies
            };
            var mockMapeoDatosItem = new Mock<ICompanyService>();
            mockMapeoDatosItem.Setup(m => m.Get(1,true)).ReturnsAsync(response);

            CompanyController companyController = new CompanyController(mockMapeoDatosItem.Object);
            var actionResult = await companyController.GetCompany(1,true);



            //Asert - verificacion
            var result = actionResult.Result as OkObjectResult;
            var itemResult = (Response<List<CompanyEditRequest>>)result!.Value!;
            Assert.AreEqual(response, itemResult);
        }
    
    
        [TestMethod]
        public async Task EditCompany()
        {
            CompanyEditRequest request = new CompanyEditRequest
            {
                Address = "",
                BusinessName = "Empresa 1",
                CompanyId = 1,
                Email = "empresa1@hotmail.com",
                Phone = "",
                Ruc = "empresa1"
            };

            Response response = new Response { Success = true, Message = "" };

            var mockMapeoDatosItem = new Mock<ICompanyService>();
            mockMapeoDatosItem.Setup(m => m.Edit(request)).ReturnsAsync(response);

            CompanyController companyController = new CompanyController(mockMapeoDatosItem.Object);
            var actionResult = await companyController.AddCompany(request);
            //Asert - verificacion
            var result = actionResult.Result as OkObjectResult;
            var itemResult = (Response)result!.Value!;
            Assert.AreEqual(response, itemResult);
        }
    }
}
