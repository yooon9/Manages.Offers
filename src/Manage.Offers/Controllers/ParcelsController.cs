namespace Manage.Offers.Controllers
{
    using Manage.Offers.Data.Entities;
    using Manage.Offers.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelService _parcelService;

        public ParcelsController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }
        [HttpGet]
        public async Task<IActionResult> GetParcels()
        {
            return Ok(new { parcels = await _parcelService.GetParcels() });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Parcel parcels)
        {
            return Ok(new { parcelId = await _parcelService.AddParcel(parcels) });
        }

    }
}
