using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.RegionDTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [Length(3, 3, ErrorMessage = "Code has to be a minimum and maximum of 3 charachters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Name has to be a maximum of 25 charachters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
