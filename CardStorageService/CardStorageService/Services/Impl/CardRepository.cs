using CardStorageService.Data;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        #region Services
        private readonly CardStorageServiceDbContext _dbContext;
        private readonly ILogger<CardRepository> _logger;
        #endregion

        #region Constructors
        public CardRepository(CardStorageServiceDbContext dbContext, ILogger<CardRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        public string Create(Card data)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            _dbContext.Cards.Add(data);
            _dbContext.SaveChanges();

            return data.CardId.ToString();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetByClientId(int id)
        {
            /*Плохой метод разработки это составления прямого запроса к БД без использования
             * entityframework, простой пример можно будет сделать sql инъекцию
             */
            return _dbContext.Cards.Where(card => card.ClientId == id).ToList();
        }

        public Card GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(Card data)
        {
            throw new NotImplementedException();
        }
    }
}
