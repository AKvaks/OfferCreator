using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferCreator.Core.Features.Offers.Queries;

namespace OfferCreator.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OfferController> _logger;

        public OfferController(IMediator mediator, ILogger<OfferController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOfferDetailsById(int Id)
        {
            var result = await _mediator.Send(new GetOfferByIdQuery(Id));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOfferForEditById(int Id)
        {
            var result = await _mediator.Send(new GetOfferByIdQuery(Id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffersPaginated(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllOffersPaginatedQuery(pageNumber, pageSize));
            return Ok(result);
        }
    }
}
