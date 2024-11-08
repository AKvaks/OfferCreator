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

        [HttpPost]
        public async Task<IActionResult> AddOffer(AddOfferCommand command)
        {
            //var commandValidation = new AddHotelCommandValidator().Validate(command);
            //if (!commandValidation.IsValid) return BadRequest(commandValidation.Errors.Select(x => x.ErrorMessage));

            var result = await _mediator.Send(command);
            return Ok(result);
            //return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer(UpdateOfferCommand command)
        {
            //var commandValidation = new EditHotelCommandValidator().Validate(command);
            //if (!commandValidation.IsValid) return BadRequest(commandValidation.Errors.Select(x => x.ErrorMessage));

            var result = await _mediator.Send(command);
            return Ok(result);
            //return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOfferById(int Id)
        {
            var result = await _mediator.Send(new DeleteOfferCommand(Id));
            return Ok(result);
        }
    }
}
