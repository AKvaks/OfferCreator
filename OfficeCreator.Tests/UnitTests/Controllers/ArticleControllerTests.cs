using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OfferCreator.API.Controllers;
using OfferCreator.Core.Features.Articles.Commands;
using OfferCreator.Core.Features.Articles.Queries;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfficeCreator.Tests.UnitTests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ArticleController>> _loggerMock;
        private readonly ArticleController _controller;

        public ArticleControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<ArticleController>>();
            _controller = new ArticleController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllItemsPaginated_ReturnsOkResult_WithListOfArticles()
        {
            // Arrange
            var expectedItems = new List<ArticleModel>
            {
                new() { Id = 1, ArticleName = "TestName1" },
                new() { Id = 2, ArticleName = "TestName2" },
                new() { Id = 3, ArticleName = "TestName3" }
            };

            var expectedResult = new PagedResponse<ArticleModel>(expectedItems, 6, 1, 3);
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllArticlesPaginatedQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllItemsPaginated(1, 3);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetArticleById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var articleId = 1;
            var expectedArticle = new ArticleModel { Id = 1, ArticleName = "TestName" };
            _mediatorMock
                .Setup(m => m.Send(It.Is<GetArticleByIdQuery>(q => q.Id == articleId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedArticle);

            // Act
            var result = await _controller.GetArticleById(articleId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedArticle, okResult.Value);
        }

        [Fact]
        public async Task GetArticleById_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = -1;

            // Act
            var result = await _controller.GetArticleById(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetArticleById_ValidId_ReturnsNotFound()
        {
            // Arrange
            var articleId = 15;
            var expectedArticle = new ArticleModel { Id = -1 };
            _mediatorMock
                .Setup(m => m.Send(It.Is<GetArticleByIdQuery>(q => q.Id == articleId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedArticle);

            // Act
            var result = await _controller.GetArticleById(articleId);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Ensures that the result is a 404 NotFound
            var notFoundResult = result as NotFoundResult;
            Assert.Equal(404, notFoundResult?.StatusCode);
        }

        [Fact]
        public async Task AddArticle_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var command = new AddArticleCommand { Id = 0, ArticleName = "TestName" };
            var expectedArticleId = 1;
            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedArticleId);

            // Act
            var result = await _controller.AddArticle(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedArticleId, okResult.Value);
        }

        [Fact]
        public async Task AddArticle_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var command = new AddArticleCommand { Id = 0 };
            _controller.ModelState.AddModelError("ArticleName", "Article name is required");

            // Act
            var result = await _controller.AddArticle(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}
