using AutoMapper;
using BlackJackAPI.Controllers;
using BlackJackAPITest.Mocks;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlackJackAPITest.Controllers
{
    public class PlayerControllerTestFixture
    {
        public Mock<IMapper> MapperMock { get; }
        public Mock<ILogger<PlayerController>> LoggerMock { get; }

        public PlayerControllerTestFixture()
        {
            MapperMock = MapperMocks.GetMapper();
            LoggerMock = LoggerMocks.GetLogger<PlayerController>();
        }
    }
}
