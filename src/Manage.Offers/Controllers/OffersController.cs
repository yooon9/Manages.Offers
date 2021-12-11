namespace Manage.Offers.Controllers
{
    using Manage.Offers.Models;
    using Manage.Offers.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferById(id);
            return Ok(new { offer });
        }

        [HttpGet]
        public async Task<IActionResult> GetOffers([FromQuery] SearchOfferModel model, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var offers = await _offerService.GetOffers(model, pageNumber ?? 1, pageSize ?? 25);
            return Ok(new { offers });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OfferRequestModel request)
        {
            var offerId = await _offerService.AddOffer(request);
            return Ok(new { offerId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OfferRequestModel request)
        {
            await _offerService.UpdateOffer(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _offerService.DeleteOffer(id);
            return Ok();
        }
    }
}
