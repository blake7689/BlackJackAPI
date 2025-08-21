using AutoMapper;
using BlackJackAPI.Controllers;
using BlackJackAPI.Dtos;
using BlackJackAPITest.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlackJackAPITest.Controllers
{
    public partial class PlayerControllerTests
    {
        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task GetPlayers_ReturnsOk_WithMappedPlayers()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.GetPlayers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPlayers = Assert.IsAssignableFrom<IEnumerable<PlayerReadDto>>(okResult.Value);
            Assert.Equal(2, returnedPlayers.Count());
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task GetPlayer_ExistingId_ReturnsOk()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.GetPlayer(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<PlayerReadDto>(okResult.Value);
            Assert.Equal(1, dto.PlayerId);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task GetPlayer_NonExistingId_ReturnsNotFound()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.GetPlayer(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task AddPlayer_Valid_ReturnsCreatedAt()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var createDto = new PlayerCreateDto { UserName = "TestUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.AddPlayer(createDto);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<PlayerReadDto>(createdAt.Value);
            Assert.Equal(3, dto.PlayerId);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task UpdatePlayer_Existing_ReturnsOk()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var updateDto = new PlayerUpdateDto { UserName = "UpdatedUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.UpdatePlayer(1, updateDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task UpdatePlayer_NonExisting_ReturnsNotFound()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var updateDto = new PlayerUpdateDto { UserName = "NoUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.UpdatePlayer(999, updateDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task DeletePlayer_Existing_ReturnsOk()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var deleteDto = new PlayerDeleteDto();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.DeletePlayer(1, deleteDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        [Trait("Category", "HappyPath")]
        public async Task DeletePlayer_NonExisting_ReturnsNotFound()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var deleteDto = new PlayerDeleteDto();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            var result = await controller.DeletePlayer(999, deleteDto);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}