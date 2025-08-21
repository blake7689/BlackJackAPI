using System.ComponentModel.DataAnnotations;

namespace BlackJackAPI.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        //public Strat Strategy { get; set; }
        //public Image PlayerImage { get; set; } 

        public decimal Credits { get; set; }

        public DateTime? InActive { get; set; }
    }
}
