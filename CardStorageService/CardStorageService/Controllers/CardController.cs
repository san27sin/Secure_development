using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        #region Services
        private readonly ILogger<CardController> _logger; //в логере указываем тип 
        private readonly ICardRepositoryService _cardRepositoryService;
        private IMapper _mapper;
        #endregion

        #region Constructors
        public CardController(ILogger<CardController> logger, ICardRepositoryService cardRepositoryService, IMapper mapper)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        
        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                var cardId = _cardRepositoryService.Create(_mapper.Map<Card>(request));
                return Ok(new CreateCardResponse { CardId = cardId.ToString() });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] int clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(card => _mapper.Map<CardDto>(card)).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        #endregion
    }
}
