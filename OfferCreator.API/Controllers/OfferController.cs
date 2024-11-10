using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferCreator.Core.Features.Offers.Commands;
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
            if (Id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await _mediator.Send(new GetOfferByIdQuery(Id));

            if (result.Id == -1) return NotFound();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOfferForEditById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await _mediator.Send(new GetOfferByIdQuery(Id));

            if (result.Id == -1) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffersPaginated(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllOffersPaginatedQuery(pageNumber, pageSize));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffer(AddOfferCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer(UpdateOfferCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (result == -1) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOfferById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await _mediator.Send(new DeleteOfferCommand(Id));

            if (result == -1) return NotFound();
            return Ok(result);
        }
    }
}
