using AutoMapper;
using CardStorageService.Data;
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
    public class ClientController : ControllerBase
    {
        #region Services
        private readonly ILogger<ClientController> _logger; //в логере указываем тип 
        private readonly IClientRepositoryService _clientRepositoryService;
        private IMapper _mapper;
        #endregion

        #region Constructors
        public ClientController(ILogger<ClientController> logger, IClientRepositoryService clientRepositoryService, IMapper mapper)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _mapper = mapper;
        }
        #endregion

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                return Ok(new CreateClientResponse { ClientId = clientId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create client error.");
                return Ok(new CreateClientResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create card error."
                });
            }
        }
    }
}
