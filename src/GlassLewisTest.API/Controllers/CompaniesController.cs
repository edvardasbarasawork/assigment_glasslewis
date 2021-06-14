using GlassLewisTest.API.Infrastructure;
using GlassLewisTest.Application.Services.Interfaces;
using GlassLewisTest.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GlassLewisTest.API.Controllers
{
    public class CompaniesController : ApiController
    {
        public readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpGet]
        [BasicAuth]
        public async Task<IActionResult> GetAllAsync([FromQuery] string ISIN)
        {
            if (string.IsNullOrEmpty(ISIN))
            {
                return Ok(await _companiesService.GetAllAsync());
            }

            return Ok(await _companiesService.GetByISINAsync(ISIN));
        }

        [HttpPost]
        [BasicAuth]
        public async Task<IActionResult> CreateAsync([FromBody] Company company)
        {
            var createdCompany = await _companiesService.CreateAsync(company);

            return Created(nameof(GetByIdAsync), createdCompany);
        }

        [HttpPut]
        [Route("{id}")]
        [BasicAuth]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Company company)
        {
            company.Id = id;
            var updatedCompany = await _companiesService.UpdateAsync(company);

            if (updatedCompany == null)
            {
                return NotFound();
            }

            return Ok(updatedCompany);
        }

        [HttpGet]
        [Route("{id}")]
        [BasicAuth]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var company = await _companiesService.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
    }
}
