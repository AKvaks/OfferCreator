using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferCreator.Core.Features.Articles.Commands;
using OfferCreator.Core.Features.Articles.Queries;

namespace OfferCreator.API.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllArticlesForDropDown()
        {
            var result = await _mediator.Send(new GetAllArticlesForDropDownQurey());
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetArticleById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await _mediator.Send(new GetArticleByIdQuery(Id));

            if (result.Id == -1) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(UpdateArticleCommand command)
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
        public async Task<IActionResult> DeleteArticleById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await _mediator.Send(new DeleteArticleCommand(Id));

            if (result == -1) return NotFound();
            return Ok(result);
        }
    }
}
