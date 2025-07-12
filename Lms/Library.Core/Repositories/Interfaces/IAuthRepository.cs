using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> ValidateUserAsync(string username, string password);
    }
}
