using Library.Core.Data;
using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<User> ValidateUserAsync(string username, string password)
        {

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand(@"
                SELECT UserId, Username, Role, IsActive 
                FROM Users
                WHERE Username = @Username 
                  AND Password = @Password 
                  AND IsActive = 1", conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User
                        {
                            UserId = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Role = reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        };
                    }
                }
            }

            return null;
        }
    }
}
