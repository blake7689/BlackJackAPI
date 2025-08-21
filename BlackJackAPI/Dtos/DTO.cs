using System;
using System.ComponentModel.DataAnnotations;

namespace BlackJackAPI.Dtos
{
    public class PlayerReadDto
    {
        public int PlayerId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public decimal Credits { get; set; }

        public DateTime? InActive { get; set; }
    }

    public class PlayerCreateDto
    {
        [Required]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string? LastName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credits must be a non-negative value.")]
        public decimal Credits { get; set; }

        public DateTime? InActive { get; set; }
    }

    public class PlayerUpdateDto
    {
        [Required]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string? LastName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credits must be a non-negative value.")]
        public decimal Credits { get; set; }

        public DateTime? InActive { get; set; }
    }

    public class PlayerDeleteDto
    {
        [Required]
        public int PlayerId { get; set; }

        public DateTime? InActive { get; set; }
    }
}
