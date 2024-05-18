using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.WalkDTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "code invalid cuz maximum length is 25")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "code invalid cuz maximum length is 25")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
