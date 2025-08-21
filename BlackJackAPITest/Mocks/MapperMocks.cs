using AutoMapper;
using Moq;
using BlackJackAPI.Dtos;
using BlackJackAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackAPITest.Mocks
{
    public static class MapperMocks
    {
        public static Mock<IMapper> GetMapper()
        {
            var mapperMock = new Mock<IMapper>();

            // Default mappings for tests
            mapperMock
                .Setup(m => m.Map<IEnumerable<PlayerReadDto>>(It.IsAny<IEnumerable<Player>>()))
                .Returns((IEnumerable<Player> players) =>
                    players.Select(p => new PlayerReadDto
                    {
                        PlayerId = p.PlayerId,
                        UserName = p.UserName
                    }));

            mapperMock
                .Setup(m => m.Map<PlayerReadDto>(It.IsAny<Player>()))
                .Returns((Player p) =>
                {
                    if (p == null) return null;
                    return new PlayerReadDto
                    {
                        PlayerId = p.PlayerId,
                        UserName = p.UserName
                    };
                });

            mapperMock
                .Setup(m => m.Map<Player>(It.IsAny<PlayerCreateDto>()))
                .Returns(new Player());

            mapperMock
                .Setup(m => m.Map(It.IsAny<PlayerUpdateDto>(), It.IsAny<Player>()));

            mapperMock
                .Setup(m => m.Map(It.IsAny<PlayerDeleteDto>(), It.IsAny<Player>()));

            return mapperMock;
        }
    }
}
