using Book_Store.Models.Domains;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountModel> GetAllAsync();
        Task<AccountModel> GetByEmailAsync(string email);
        Task<AccountModel> GetByUserNameAsync(string username);
        Task<AccountModel> UpdateAsync(AccountModel account);
        Task<AccountModel> DeleteAsync(int id);
        Task<AccountModel> CreateAsync(AccountModel account);
    }
}
