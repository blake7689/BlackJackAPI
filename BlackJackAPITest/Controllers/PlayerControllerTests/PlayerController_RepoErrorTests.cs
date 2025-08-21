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
        [Trait("Category", "RepoError")]
        public async Task GetPlayers_WhenRepositoryThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetErrorPlayerRepository();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.GetPlayers());
        }

        [Fact]
        [Trait("Category", "RepoError")]
        public async Task GetPlayer_WhenRepositoryThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetErrorPlayerRepository();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.GetPlayer(1));
        }

        [Fact]
        [Trait("Category", "RepoError")]
        public async Task AddPlayer_WhenRepositoryThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetErrorPlayerRepository();
            var createDto = new PlayerCreateDto { UserName = "TestUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.AddPlayer(createDto));
        }

        [Fact]
        [Trait("Category", "RepoError")]
        public async Task UpdatePlayer_WhenRepositoryThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetErrorPlayerRepository();
            var updateDto = new PlayerUpdateDto { UserName = "UpdatedUser" };
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.UpdatePlayer(1, updateDto));
        }

        [Fact]
        [Trait("Category", "RepoError")]
        public async Task DeletePlayer_WhenRepositoryThrows_ExceptionBubblesUp()
        {
            var repoMock = PlayerRepositoryMocks.GetErrorPlayerRepository();
            var deleteDto = new PlayerDeleteDto();
            var controller = new PlayerController(repoMock.Object, _loggerMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.DeletePlayer(1, deleteDto));
        }
    }
}