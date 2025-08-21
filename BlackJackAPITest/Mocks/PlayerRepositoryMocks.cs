using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BlackJackAPI.Interfaces;
using BlackJackAPI.Models;

namespace BlackJackAPITest.Mocks
{
    public class PlayerRepositoryMocks
    {
        public static Mock<IPlayerRepository> GetPlayerRepository()
        {
            var Players = new List<Player>
            {
                new Player {
                    PlayerId = 1, 
                    UserName = "Player1",
                    Password = "Password1",
                    Email = "email1@gmail.com",
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    Credits = 1000,
                    InActive = null
                },
                new Player {
                    PlayerId = 2,
                    UserName = "Player2",
                    Password = "Password2",
                    Email = "email2@gmail.com",
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    Credits = 2000,
                    InActive = null
                }
            };

            var mockPlayerRepository = new Mock<IPlayerRepository>();

            mockPlayerRepository.Setup(repo => repo.GetAllPlayers()).ReturnsAsync(Players.Where(p => p.InActive == null).AsEnumerable());

            mockPlayerRepository.Setup(repo => repo.GetPlayerById(It.IsAny<int>())).ReturnsAsync((int id) => Players.FirstOrDefault(p => p.PlayerId == id && p.InActive == null));

            mockPlayerRepository.Setup(repo => repo.AddPlayer(It.IsAny<Player>()))
                .ReturnsAsync((Player player) => 
                {
                    player.PlayerId = Players.Max(p => p.PlayerId) + 1;
                    player.UserName = "AddedUser";
                    player.Email = "AddedEmail@gmail.com";
                    player.Password = "AddedPassword";
                    player.FirstName = "AddedFirstName";
                    player.LastName = "AddedLastName";
                    player.Credits = 1000;  
                    player.InActive = null;
                    Players.Add(player);
                    return player;
                });

            mockPlayerRepository.Setup(repo => repo.UpdatePlayer(It.IsAny<Player>()))
                .ReturnsAsync((Player player) => 
                {
                    var existingPlayer = Players.FirstOrDefault(p => p.PlayerId == player.PlayerId && p.InActive == null);
                    if (existingPlayer != null)
                    {
                        existingPlayer.UserName = "UpdateUserName";
                        existingPlayer.Password = "UpdatedPassword";
                        existingPlayer.Email = "UpdatedEmail@gmail.com";
                        existingPlayer.FirstName = "UpdatedFirstName";
                        existingPlayer.LastName = "UpdateLastName";
                        existingPlayer.Credits = 5000;
                        existingPlayer.InActive = null;
                        return true;
                    }
                    return false;
                });

            mockPlayerRepository.Setup(repo => repo.DeletePlayer(It.IsAny<Player>()))
                .ReturnsAsync((Player player) => 
                {
                    var existingPlayer = Players.FirstOrDefault(p => p.PlayerId == player.PlayerId && p.InActive == null);
                    if (existingPlayer != null)
                    {
                        existingPlayer.InActive = DateTime.UtcNow; 
                        //Players.Remove(existingPlayer);
                        return true;
                    }
                    return false;
                });
            return mockPlayerRepository;
        }

        public static Mock<IPlayerRepository> GetErrorPlayerRepository()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            mockPlayerRepository.Setup(repo => repo.GetAllPlayers())
                .ThrowsAsync(new Exception("Database connection error"));
            mockPlayerRepository.Setup(repo => repo.GetPlayerById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database connection error"));
            mockPlayerRepository.Setup(repo => repo.AddPlayer(It.IsAny<Player>()))
                .ThrowsAsync(new Exception("Database connection error"));
            mockPlayerRepository.Setup(repo => repo.UpdatePlayer(It.IsAny<Player>()))
                .ThrowsAsync(new Exception("Database connection error"));
            mockPlayerRepository.Setup(repo => repo.DeletePlayer(It.IsAny<Player>()))
                .ThrowsAsync(new Exception("Database connection error"));
            return mockPlayerRepository;
        }
    }
}
