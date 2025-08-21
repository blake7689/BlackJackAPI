using BlackJackAPI.Models;  

namespace BlackJackAPI.Interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayers();

        Task<Player?> GetPlayerById(int playerId);

        Task<Player> AddPlayer(Player player);

        Task<bool> UpdatePlayer(Player player);

        Task<bool> DeletePlayer(Player player);

        Task<Player?> AuthenticatePlayer(string usernameOrEmail, string password);
    }
}