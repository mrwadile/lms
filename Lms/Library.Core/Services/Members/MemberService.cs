using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.Members
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Adds a new member asynchronously.
        /// </summary>
        public async Task AddMemberAsync(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }
            try
            {
                await _memberRepository.AddAsync(member);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding member.", ex);
            }
        }


        /// <summary>
        /// Deletes a book by ID asynchronously.
        /// </summary>
        public async Task DeleteMemberAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid member ID.", nameof(id));
            }
            try
            {
                await _memberRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting member.", ex);
            }
        }


        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            try
            {
                return await _memberRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching members.", ex);
            }
        }

        /// <summary>
        /// Retrieves a book by ID asynchronously.
        /// </summary>
        public Task<Member> GetMemberByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid member ID.", nameof(id));
            }
            try
            {
                return _memberRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching member with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Updates a book asynchronously.
        /// </summary>
        public Task UpdateMemberAsync(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }
            if (member.MemberId <= 0)
            {
                throw new ArgumentException("Invalid member ID.", nameof(member.MemberId));
            }
            try
            {
                return _memberRepository.UpdateAsync(member);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating member.", ex);
            }
        }
    }
}
