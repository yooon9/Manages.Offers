namespace Manage.Offers.Controllers
{
    using Manage.Offers.Data.Entities;
    using Manage.Offers.Models;
    using Manage.Offers.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        private readonly IBrokerService _brokerService;

        public BrokersController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrokerById(int id)
        {
            return Ok(new { broker = await _brokerService.GetBrokerById(id) });
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterBrokers([FromBody] SearchBrokerModel model, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var brokers = await _brokerService.GetBrokers(model, pageNumber ?? 1, pageSize ?? 25);
            return Ok(new { brokers });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BrokerRequestModel request)
        {
            var brokerId = await _brokerService.AddBroker(request);
            return Ok(new { brokerId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BrokerRequestModel request)
        {
            await _brokerService.UpdateBroker(id, request);
            return Ok();
        }
    }
}
