using Home_Heart.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Contracts
{
    public interface IUserAppService
    {
        Task<IdentityUser> RegistrationAsync(RegistrationDto obj);
        Task<CustomToken> LoginAsync(LoginDto obj);
    }
}
