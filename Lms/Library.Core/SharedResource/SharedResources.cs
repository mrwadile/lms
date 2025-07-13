using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SharedResource
{
    public static class SharedResources
    {
        public static string ErrorWhileFetchingLoginDetails = "Username and password cannot be null or empty.";
        public static string ErrorWhileFetchingBooks = "Error while fetching books.";
        public static string ErrorWhileFetchingBookWithId = "Error while retrieving book with ID.";
        public static string ErrorWhileAddingBook = "Error while adding book.";
        public static string ErrorWhileupdatingBook = "Error while updating book with ID";
        public static string ErrorWhileDeletingingBook = "Error while deleting book with ID";
        public static string ErrorWhileIssueBook = "Error While issue book with ID";
        public static string ErrorWhileReturn = "Error while returning book with issue ID";
        public static string ErrorWhileAddingMember = "Error while adding member";
        public static string ErrorWhileDeletingMember = "Error while deleting member with ID";
        public static string ErrorWhileFetchingMembers = "Error while fetching members.";
        public static string ErrorWhileFetchingMemberWithId = "Error while retrieving member with ID";
        public static string ErrorWhileUpdatingMemberWithId = "Error while updating memeber with ID";
        public static string ErrorMemberCanNotBeNull = "Member cannot be null.";
        public static string ErrorInvalidMemberId = "Invalid member ID.";
        public static string ErrorInvalidBookId = "Invalid member ID.";
        public static string ErrorInvalidMember = "Invalid member";
        public static string ErrorWhileOverdueBooks = "Error occurred while fetching overdue books.";
        public static string ErrorWhileFetchingUnretrnedIssuesBoks= "Error occurred while fetching unreturned issues.";
        public static string ErrorWhileReturnBook= "Error returning book:";
        public static string ErrorWhileFetchingHistoryBooks= "Error occurred while fetching the history for book with ID";
        public static string ErrorWhileCheckingIssuedRecords = "Cannot delete. Book has issue history.";
    }
}
