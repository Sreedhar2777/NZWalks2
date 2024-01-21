using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var regionsDomain = nZWalksDbContext.Regions.ToList();

            var regionDTO=new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain) 
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute]Guid id)
        {
            var regionDomain = nZWalksDbContext.Regions.Find(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };


            return Ok(regionDTO);
        }

        [HttpPost]

        public IActionResult AddRegion([FromBody]AddRegionRequestDTO addRegionRequestDTO) 
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
            };


            nZWalksDbContext.Regions.Add(regionDomainModel);
            nZWalksDbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDTO);

        }

        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult UpdateRegion([FromRoute]Guid id, [FromBody]UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel=nZWalksDbContext.Regions.FirstOrDefault(x =>x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            nZWalksDbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public IActionResult DeleteRegion([FromRoute]Guid id)
        {
            var regionDomainModel = nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            nZWalksDbContext.Regions.Remove(regionDomainModel);
            nZWalksDbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

    }
}
