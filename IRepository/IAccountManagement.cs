using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.IRepository
{
    public interface IAccountManagement
    {
        Task<CompanyDetails> AuthenticateUserAsync(string username, string password);
    }
}
