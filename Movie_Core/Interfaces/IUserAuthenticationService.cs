using Movie_Core.Dtos;
using Movie_Core.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginDtoModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegisterDtoModel model);
       // Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
