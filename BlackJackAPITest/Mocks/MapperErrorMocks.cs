using AutoMapper;
using Moq;
using System;
using BlackJackAPI.Dtos;
using BlackJackAPI.Models;
using System.Collections.Generic;

namespace BlackJackAPITest.Mocks
{
    public static class MapperErrorMocks
    {
        public static Mock<IMapper> GetErrorMapper()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(m => m.Map<IEnumerable<PlayerReadDto>>(It.IsAny<IEnumerable<Player>>()))
                .Throws(new Exception("Mapping error: Cannot map players list"));

            mapperMock
                .Setup(m => m.Map<PlayerReadDto>(It.IsAny<Player>()))
                .Throws(new Exception("Mapping error: Cannot map single player"));

            mapperMock
                .Setup(m => m.Map<Player>(It.IsAny<PlayerCreateDto>()))
                .Throws(new Exception("Mapping error: Cannot map create DTO"));

            mapperMock
                .Setup(m => m.Map(It.IsAny<PlayerUpdateDto>(), It.IsAny<Player>()))
                .Throws(new Exception("Mapping error: Cannot map update DTO"));

            mapperMock
                .Setup(m => m.Map(It.IsAny<PlayerDeleteDto>(), It.IsAny<Player>()))
                .Throws(new Exception("Mapping error: Cannot map delete DTO"));

            return mapperMock;
        }
    }
}
