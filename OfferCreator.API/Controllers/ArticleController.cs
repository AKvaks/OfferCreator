using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferCreator.Core.Features.Articles.Commands;
using OfferCreator.Core.Features.Articles.Queries;

namespace OfferCreator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IMediator mediator, ILogger<ArticleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemsPaginated(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllArticlesPaginatedQuery(pageNumber, pageSize));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetArticleById(int Id)
        {
            var result = await _mediator.Send(new GetArticleByIdQuery(Id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleCommand command)
        {
            //var commandValidation = new AddHotelCommandValidator().Validate(command);
            //if (!commandValidation.IsValid) return BadRequest(commandValidation.Errors.Select(x => x.ErrorMessage));

            var result = await _mediator.Send(command);
            return Ok(result);
            //return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotelById(UpdateArticleCommand command)
        {
            //var commandValidation = new EditHotelCommandValidator().Validate(command);
            //if (!commandValidation.IsValid) return BadRequest(commandValidation.Errors.Select(x => x.ErrorMessage));

            var result = await _mediator.Send(command);
            return Ok(result);
            //return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHotelById(int Id)
        {
            var result = await _mediator.Send(new DeleteArticleCommand(Id));
            return Ok(result);
        }
    }
}
