using AutoMapper;
using BlackJackAPI.Dtos;
using BlackJackAPI.Interfaces;
using BlackJackAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlackJackAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;

        public PlayerController
            (IPlayerRepository playerRepository, ILogger<PlayerController> logger, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerReadDto>>> GetPlayers()
        {
            _logger.LogInformation("Fetching all players...");
            var players = await _playerRepository.GetAllPlayers();
            _logger.LogInformation("Retrieved {Count} players.", players.Count());

            return Ok(_mapper.Map<IEnumerable<PlayerReadDto>>(players));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerReadDto>> GetPlayer(int id)
        {
            _logger.LogInformation("Fetching player with ID {Id}", id);
            var player = await _playerRepository.GetPlayerById(id);

            if (player == null)
            {
                _logger.LogWarning("Player with ID {Id} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Player with ID {Id} found.", id);
            return Ok(_mapper.Map<PlayerReadDto>(player));
        }

        [HttpPost]
        public async Task<ActionResult<PlayerReadDto>> AddPlayer(PlayerCreateDto playerCreateDto)
        {
            _logger.LogInformation("Adding new player: {UserName}", playerCreateDto.UserName);
            var playerEntity = _mapper.Map<Player>(playerCreateDto);
            var createdPlayer = await _playerRepository.AddPlayer(playerEntity);

            var playerReadDto = _mapper.Map<PlayerReadDto>(createdPlayer);
            _logger.LogInformation("Player created with ID {Id}", playerReadDto.PlayerId);

            return CreatedAtAction(nameof(GetPlayer), new { id = playerReadDto.PlayerId }, playerReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, PlayerUpdateDto playerUpdateDto)
        {
            _logger.LogInformation("Updating player with ID {Id}", id);
            var existingPlayer = await _playerRepository.GetPlayerById(id);

            if (existingPlayer == null)
            {
                _logger.LogWarning("Cannot update. Player with ID {Id} not found.", id);
                return NotFound();
            }

            _mapper.Map(playerUpdateDto, existingPlayer);
            await _playerRepository.UpdatePlayer(existingPlayer);

            _logger.LogInformation("Player with ID {Id} updated successfully.", id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id, PlayerDeleteDto playerDeleteDto)
        {
            _logger.LogInformation("Deleting player with ID {Id}", id);
            var player = await _playerRepository.GetPlayerById(id);

            if (player == null)
            {
                _logger.LogWarning("Cannot delete. Player with ID {Id} not found.", id);
                return NotFound();
            }

            _mapper.Map(playerDeleteDto, player);
            await _playerRepository.DeletePlayer(player);

            _logger.LogInformation("Player with ID {Id} deleted successfully.", id);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<PlayerReadDto>> Login(PlayerLoginDto loginDto)
        {
            _logger.LogInformation("Attempting login for {User}", loginDto.UsernameOrEmail);

            var player = await _playerRepository.AuthenticatePlayer(loginDto.UsernameOrEmail, loginDto.Password);

            if (player == null)
            {
                _logger.LogWarning("Invalid login attempt for {User}", loginDto.UsernameOrEmail);
                return Unauthorized("Invalid username/email or password.");
            }

            var playerReadDto = _mapper.Map<PlayerReadDto>(player);
            return Ok(playerReadDto);
        }
    }
}