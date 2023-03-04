using CardStorageService.Data;

namespace CardStorageService.Services.Impl
{
    public class ClientRepository : IClientRepositoryService
    {
        #region Services
        private readonly CardStorageServiceDbContext _dbContext;
        private readonly ILogger<ClientRepository> _logger;
        #endregion

        #region Constructors
        public ClientRepository(CardStorageServiceDbContext dbContext, ILogger<ClientRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        public int Create(Client data)
        {
            _dbContext.Clients.Add(data);
            _dbContext.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Client data)
        {
            throw new NotImplementedException();
        }
    }
}
