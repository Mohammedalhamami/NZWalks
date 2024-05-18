using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.DifficultyDTO;
using NZWalks.API.Models.DTOs.RegionDTO;

namespace NZWalks.API.Models.DTOs.WalkDTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
         
        //navigation properties.
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
