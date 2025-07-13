using Library.Core.Data;
using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.SharedResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public async Task AddAsync(Member member)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_AddMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", member.FullName);
                    cmd.Parameters.AddWithValue("@Mobile", member.Mobile);
                    cmd.Parameters.AddWithValue("@Email", member.Email);
                    cmd.Parameters.AddWithValue("@IsActive", member.IsActive);


                    var result = await cmd.ExecuteScalarAsync();
                    member.MemberId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileAddingMember, ex);
            }
        }

        public async Task DeleteAsync(int memberId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_DeleteMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", memberId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileDeletingMember} {memberId}.", ex);
            }
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var members = new List<Member>();

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_GetAllMembers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            members.Add(new Member
                            {
                                MemberId = Convert.ToInt32(reader["MemberId"]),
                                FullName = reader["FullName"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                Email = reader["Email"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileFetchingMembers, ex);
            }

            return members;
        }

        public async Task<Member> GetByIdAsync(int memberId)
        {
            Member member = null;

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_GetMemberById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            member = new Member
                            {
                                MemberId = Convert.ToInt32(reader["MemberId"]),
                                FullName = reader["FullName"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                Email = reader["Email"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileFetchingMemberWithId} {memberId}.", ex);
            }

            return member;
        }

        public async Task UpdateAsync(Member member)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_UpdateMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", member.MemberId);
                    cmd.Parameters.AddWithValue("@FullName", member.FullName);
                    cmd.Parameters.AddWithValue("@Mobile", member.Mobile);
                    cmd.Parameters.AddWithValue("@Email", member.Email);
                    cmd.Parameters.AddWithValue("@IsActive", member.IsActive);


                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileUpdatingMemberWithId} {member.MemberId}.", ex);
            }
        }
    }
}
