using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.WalkDTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        

        //create walk
        //POST: 
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //map dto ==> domain.
            var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

            //from db to domain.
            walkDomain =  await walkRepository.CreateAsync(walkDomain);

            return Ok(mapper.Map<WalkDto>(walkDomain));
         

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                                               [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //from db to ==> domain.
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //map domain ==> dto.
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //db ==> domain.
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel is null)
            {
                return NotFound();
            }

            //returndomain model ==> DTO.
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //dto ==> domain
            var walkdomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //db ==> domain to updating.
            walkdomainModel = await walkRepository.UpdateAsync(id, walkdomainModel);

            if(walkdomainModel is null)
            {
                return NotFound();
            }

            //return domain ==> dto.

            return Ok(mapper.Map<WalkDto>(walkdomainModel));
            
           
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //from db to domain model.
            var WalkDomainModel = await walkRepository.DeleteAsync(id);

            if(WalkDomainModel is null)
            {
                return NotFound();
            }

            //mapping from domain to dto
            return Ok(mapper.Map<WalkDto>(WalkDomainModel));

        }
       

    }
}
