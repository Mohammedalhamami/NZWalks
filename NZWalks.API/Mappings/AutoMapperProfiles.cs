using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.DifficultyDTO;
using NZWalks.API.Models.DTOs.RegionDTO;
using NZWalks.API.Models.DTOs.WalkDTO;
using System.Runtime.CompilerServices;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //mapping for Region.
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();

            //mapping for Walk.
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            

        }
    }
}
