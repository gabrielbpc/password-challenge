using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using User.Domain.Configurations;
using User.Domain.Constants;
using User.Domain.Contracts.Repository;

namespace User.Repository.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Domain.Entities.User> _userCollection;
        public UserRepository(IOptions<MongoSettings> mongosettings)
        {
            var client = new MongoClient(mongosettings.Value.ConnectionString);
            var database = client.GetDatabase(mongosettings.Value.DatabaseName);
            _userCollection = database.GetCollection<Domain.Entities.User>(RepositoryConstants.User.CollectionName);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _userCollection.CountDocumentsAsync(obj => obj.Email.Equals(email)) > 0;
        }

        public async Task<Domain.Entities.User> GetByEmailAsync(string email)
        {
            var cursor = await _userCollection.FindAsync(obj => obj.Email.Equals(email));
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<string> SaveAsync(Domain.Entities.User newUser)
        {
            await _userCollection.InsertOneAsync(newUser);

            return newUser.Id.ToString();
        }
    }
}
