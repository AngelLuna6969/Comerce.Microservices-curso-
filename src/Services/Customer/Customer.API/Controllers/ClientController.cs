using Customer.Service.EventHandler.Commands;
using Customer.Service.Queries;
using Customer.Service.Queries.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using Customer.Domain;
using Microsoft.AspNetCore;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : Controller
    {
        private readonly IClientQueryService _clientQueryService;
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(IClientQueryService clientQueryService, ILogger<ClientController> logger, IMediator mediator)
        {
            _clientQueryService = clientQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<DataCollection<ClientDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> clients = null;
            if (!string.IsNullOrEmpty(ids))
            {
                clients = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _clientQueryService.GetAllAsync(page, take, clients);
        }

        [HttpGet("{id}")]

        public async Task<ClientDTO> Get(int id)
        {
            return await _clientQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
