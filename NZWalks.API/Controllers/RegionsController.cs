using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.RegionDTO;
using NZWalks.API.Repositories;
using Sqids;
using System.Globalization;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
           this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        
       
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database --> domain model
            var regionsDomain = await regionRepository.GetAllAsync();
            //Mapping domain model --> to DTO.
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //Get data from database --> domain model
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if(regionDomain is null)
            {
                return NotFound();
            }

            //Mapping domain model --> to DTO.
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            //dto ==> domain.
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);
            
            //domain ==> db.
            regionDomain = await regionRepository.CreateAsync(regionDomain);

           //domain ==> dto.
            var regionDto =  mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
           
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {
            
            //dto ==> domain.
            var regionDomain = mapper.Map<Region>(updateRegionRequestDto);

            //db ==> domain.
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if(regionDomain is null)
            {
                return NotFound();
            }

                //return domain ==> dto.
                return Ok(mapper.Map<RegionDto>(regionDomain));
           
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //db ==> domain.
            var regionDomain = await regionRepository.DeleteAsync(id);

            if(regionDomain is null)
            {
                return NotFound();
            }

            //return domain ==> dto.
            return Ok(mapper.Map<RegionDto>(regionDomain));
        
        }
        
    
    }
}
