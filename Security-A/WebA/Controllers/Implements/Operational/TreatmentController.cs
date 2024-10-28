﻿using Business.Interfaces.Operational;
using Entity.Dto.Operational;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using WebA.Controllers.Interfaces.Operational;

namespace WebA.Controllers.Implements.Operational
{
    [Route("api/Treatment")]
    [ApiController]
    public class TreatmentController : ControllerBase, ITreatmentController
    {
        private readonly ITreatmentBusiness business;

        public TreatmentController(ITreatmentBusiness business)
        {
            this.business = business;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await business.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TreatmentDto>>> Get(int id)
        {
            var result = await business.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TreatmentDto>>>> GetAll()
        {
            var result = await business.GetAll();
            return Ok(result);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TreatmentDto>>>> GetAllUser(int id)
        {
            var result = await business.GetAllUser(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("AllSelect")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DataSelectDto>>>> GetAllSelect()
        {
            var result = await business.GetAllSelect();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TreatmentDto Treatment)
        {
            if (Treatment == null)
            {
                return BadRequest("Entity is null.");
            }
            var result = await business.Save(Treatment);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TreatmentDto Treatment)
        {
            if (Treatment == null)
            {
                return BadRequest();
            }
            await business.Update(Treatment);
            return NoContent();
        }
    }
}
