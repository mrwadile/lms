using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories.Interfaces
{
    public interface IMember
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int bookId);
        Task AddAsync(Member book);
        Task UpdateAsync(Member book);
        Task DeleteAsync(int bookId);
    }
}
