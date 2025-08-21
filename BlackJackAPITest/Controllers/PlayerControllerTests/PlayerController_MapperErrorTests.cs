using BlackJackAPI.Controllers;
using BlackJackAPI.Dtos;
using BlackJackAPITest.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BlackJackAPITest.Controllers
{
    public partial class PlayerControllerTests
    {
        [Fact]
        [Trait("Category", "MapperError")]
        public async Task GetPlayers_WhenMapperThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var mapperMock = MapperErrorMocks.GetErrorMapper();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.GetPlayers());
        }

        [Fact]
        [Trait("Category", "MapperError")]
        public async Task GetPlayer_WhenMapperThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var mapperMock = MapperErrorMocks.GetErrorMapper();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.GetPlayer(1));
        }

        [Fact]
        [Trait("Category", "MapperError")]
        public async Task AddPlayer_WhenMapperThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var mapperMock = MapperErrorMocks.GetErrorMapper();
            var createDto = new PlayerCreateDto { UserName = "TestUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.AddPlayer(createDto));
        }

        [Fact]
        [Trait("Category", "MapperError")]
        public async Task UpdatePlayer_WhenMapperThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var mapperMock = MapperErrorMocks.GetErrorMapper();
            var updateDto = new PlayerUpdateDto { UserName = "UpdatedUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.UpdatePlayer(1, updateDto));
        }

        [Fact]
        [Trait("Category", "MapperError")]
        public async Task DeletePlayer_WhenMapperThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetPlayerRepository();
            var mapperMock = MapperErrorMocks.GetErrorMapper();
            var deleteDto = new PlayerDeleteDto();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.DeletePlayer(1, deleteDto));
        }
    }
}
