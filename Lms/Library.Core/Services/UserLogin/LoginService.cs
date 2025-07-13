using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.UserLogin
{
    public class LoginService : ILoginService
    {
        private readonly IAuthRepository _authRepository;

        public LoginService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<User> CheckValidateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(SharedResources.ErrorWhileFetchingLoginDetails);
            }
            return _authRepository.ValidateUserAsync(username, password);
        }
    }
}
