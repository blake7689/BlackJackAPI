using Xunit;

namespace BlackJackAPITest.Controllers
{
    [CollectionDefinition("PlayerControllerTests")]
    public class PlayerControllerTestCollection : ICollectionFixture<PlayerControllerTestFixture>
    {
    }
}
