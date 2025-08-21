using AutoMapper;
using BlackJackAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlackJackAPITest.Controllers
{
    [Collection("PlayerControllerTests")]
    public partial class PlayerControllerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<PlayerController>> _loggerMock;

        public PlayerControllerTests(PlayerControllerTestFixture fixture)
        {
            _mapperMock = fixture.MapperMock;
            _loggerMock = fixture.LoggerMock;
        }
    }
}