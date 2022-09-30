using LayerAccess;
using LayerAccess.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Admin;
using ViewModel.Security;
using ViewModel.User;

namespace LayerBusiness.User
{
    public interface IUserService
    {
        Task<Response> CreateUser(CreateUserRequest request);
        Task<Response<LoginUserResponse>> LoginUser(LoginUserRequest request);
    }
    public class UserService : IUserService
    {
        #region property
        public InvoiceContext Database { get; }
        public TokenSetting TokenSetting { get; }
        #endregion
        public UserService(InvoiceContext database,TokenSetting tokenSetting)
        {
            Database = database;
            TokenSetting = tokenSetting;
        }

       

        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            //Validamos si hay mas registro en base de datos
            int rolId = 0;
            var count = await Database.Users.CountAsync();
            rolId = count == 0 ? await Database.Rols.Where(x => x.Description.Equals("Administrador")).Select(x => x.RolId).FirstOrDefaultAsync() : await Database.Rols.Where(x => x.Description.Equals("Usuario")).Select(x => x.RolId).FirstOrDefaultAsync();

            //Validamos si el Email se encuentra registrado
            var user = await Database.Users.Where(x => x.Email.Equals(request.Email)).FirstOrDefaultAsync();
            if (user != null)
                return new Response { Success = false, Message = "Email ya se encuentra registrado" };

            var security = Security.Generatehashed(request.Password);


            Company company = new Company
            {
                Address = request.Address,
                BusinessName = $"{request.FirstName} {request.LastName}",
                Email = request.Email,
                Phone = request.Phone,
                User = new LayerAccess.Admin.User
                {
                    Created = DateTime.Now,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    RolId = rolId,
                    Status = "A",
                    Password = security.Item2,
                    Valor = security.Item1

                }
            };


            await Database.Companies.AddAsync(company);
            await Database.SaveChangesAsync();

            return new Response { Success = true, Message = "Usuario creado exitosamente" };
        }

        public async Task<Response<LoginUserResponse>> LoginUser(LoginUserRequest request)
        {
            var user = await Database.Users.Include(x=>x.Rol).
                Where(x => x.Email.Equals(request.Email)).FirstOrDefaultAsync();
            if (user == null)
                return new Response<LoginUserResponse> { Success = false, Message = "Credenciales incorrectas" };

            var confirmation = Security.ConfirmationPassword(password: request.Password,
                                                                 salt: user.Valor,
                                                                  passwordHash: user.Password);

            if(!confirmation)
                return new Response<LoginUserResponse> { Success = false, Message = "Credenciales incorrectas" };

            var claims = new List<Claim>
            {
                new Claim("UserId" , user.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role,user.Rol.Description)
            };

            var token = Security.GenerateToken(TokenSetting, claims);

            var companyId = await Database.Companies.Where(x => x.UserId == user.UserId).Select(x=>x.CompanyId).FirstOrDefaultAsync();

            return new Response<LoginUserResponse>
            {
                Success = true,
                Message = "Usuario autenticado",
                Result = new LoginUserResponse
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    RolId = user.RolId,
                    RolName = user.Rol.Description,
                    Token = token,
                    UserId = user.UserId,
                    CompanyId = companyId
                }
            };



        }
    }
}
