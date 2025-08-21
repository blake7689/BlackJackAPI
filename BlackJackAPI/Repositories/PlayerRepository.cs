using BlackJackAPI.DbContext;
using BlackJackAPI.Interfaces;
using BlackJackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackJackAPI.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly BlackJackAPIDbContext _blackJackDbcontext;

        public PlayerRepository(BlackJackAPIDbContext context)
        {
            _blackJackDbcontext = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _blackJackDbcontext.Players
                .Where(p => p.InActive == null)
                .ToListAsync();
        }

        public async Task<Player?> GetPlayerById(int playerId)
        {
            return await _blackJackDbcontext.Players
                .FirstOrDefaultAsync(p => p.PlayerId == playerId && p.InActive == null);
        }

        public async Task<Player> AddPlayer(Player player)
        {
            await _blackJackDbcontext.Players.AddAsync(player);
            await _blackJackDbcontext.SaveChangesAsync();
            return player;
        }

        public async Task<bool> UpdatePlayer(Player player)
        {
            var existingPlayer = await _blackJackDbcontext.Players
                .FirstOrDefaultAsync(p => p.PlayerId == player.PlayerId && p.InActive == null);

            if (existingPlayer == null)
                return false;

            existingPlayer.UserName = player.UserName;
            existingPlayer.Password = player.Password;
            existingPlayer.Email = player.Email;
            existingPlayer.FirstName = player.FirstName;
            existingPlayer.LastName = player.LastName;
            existingPlayer.Credits = player.Credits;

            await _blackJackDbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayer(Player player)
        {
            var existingPlayer = await _blackJackDbcontext.Players
                .FirstOrDefaultAsync(p => p.PlayerId == player.PlayerId && p.InActive == null);

            if (existingPlayer == null)
                return false;

            existingPlayer.InActive = DateTime.Now; // Soft delete by setting InActive date

            //_blackJackDbcontext.Players.Remove(existingPlayer);
            await _blackJackDbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Player?> AuthenticatePlayer(string usernameOrEmail, string password)
        {
            return await _blackJackDbcontext.Players
                .FirstOrDefaultAsync(p =>
                    (p.UserName == usernameOrEmail || p.Email == usernameOrEmail) &&
                    p.Password == password &&
                    p.InActive == null
                );
        }
    }
}