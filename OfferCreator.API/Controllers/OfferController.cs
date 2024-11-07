using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferCreator.Core.Features.Offers.Queries;

namespace OfferCreator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherForecastController> _logger;

        public OfferController(IMediator mediator, ILogger<WeatherForecastController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffers()
        {
            var result = await _mediator.Send(new GetAllOffersQuery());
            return Ok(result);
        }
    }
}
