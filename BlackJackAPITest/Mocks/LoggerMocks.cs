using Microsoft.Extensions.Logging;
using Moq;

namespace BlackJackAPITest.Mocks
{
    public static class LoggerMocks
    {
        public static Mock<ILogger<T>> GetLogger<T>() where T : class
        {
            return new Mock<ILogger<T>>();
        }
    }
}
