using System.Threading.Tasks;

namespace User.Domain.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email);
        Task<string> SaveAsync(Entities.User newUser);
        Task<Domain.Entities.User> GetByEmailAsync(string email);
    }
}
