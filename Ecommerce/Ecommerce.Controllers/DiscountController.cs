using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;
using Ecommerce.Services.DiscountService.DTO;
using Ecommerce.Services.DiscountService.Interfaces;
using Ecommerce.Services.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Services.Common;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1/discounts")]
    [SwaggerTag("Create, read, update and delete Discounts")]
    public class DiscountController(IDiscountService discountService, IFileService fileService) : AppController<Discount, DiscountFilterOptions, GetDiscountDto>(discountService)
    {
        private readonly IDiscountService _discountService = discountService;
        private readonly IFileService _fileService = fileService;

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get a list of discounts", Description = "Permission: All users")]
        [SwaggerResponse(200, "The discounts were fetched successfully", typeof(PaginatedResult<Discount, GetDiscountDto>))]
        [SwaggerResponse(401, "Unauthorized")]
        async public override Task<ActionResult<PaginatedResult<Discount, GetDiscountDto>>> GetItems([FromQuery] DiscountFilterOptions filteringOptions)
        {
            return await base.GetItems(filteringOptions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get a discount by id", Description = "Permission: All users")]
        [SwaggerResponse(200, "The discount was fetched successfully", typeof(GetDiscountDto))]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "The discount was not found", typeof(ProblemDetails))]
        async public override Task<ActionResult<Discount>> GetItem(int id)
        {
            return await base.GetItem(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Create a new discount", Description = "Permission: Admin only")]
        [SwaggerResponse(201, "The discount was created successfully", typeof(GetDiscountDto))]
        [SwaggerResponse(400, "Validation failed", typeof(List<ValidationFailure>))]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden", typeof(ProblemDetails))]
        [SwaggerResponse(409, "The discount already exists", typeof(ProblemDetails))]
        public async Task<ActionResult<GetDiscountDto>> CreateAsync([FromForm] CreateOrUpdateDiscountDto dto)
        {
            var validationResult = new CreateOrUpdateDiscountDtoValidator().Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            
            var entity = await _discountService.UpsertAsync(dto);

            return CreatedAtAction(nameof(GetItem), new { id = entity.Id }, entity);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Update an existing discount", Description = "Permission: Admin only")]
        [SwaggerResponse(200, "The discount was updated successfully", typeof(GetDiscountDto))]
        [SwaggerResponse(400, "Validation failed", typeof(List<ValidationFailure>))]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden", typeof(ProblemDetails))]
        [SwaggerResponse(404, "The discount was not found", typeof(ProblemDetails))]
        [SwaggerResponse(409, "The discount already exists", typeof(ProblemDetails))]
        public async Task<ActionResult<GetDiscountDto>> PartialUpdateAsync(int id, [FromForm] PartialUpdateDiscountDto dto)
        {
            var validationResult = new PartialUpdateDiscountDtoValidator().Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var entity = await _discountService.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var updatedEntity = await _discountService.PartialUpdateByIdAsync(dto, id);

            return Ok(updatedEntity);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Update an existing discount", Description = "Permission: Admin only")]
        [SwaggerResponse(200, "The discount was updated successfully", typeof(GetDiscountDto))]
        [SwaggerResponse(201, "The discount was created successfully", typeof(GetDiscountDto))]
        [SwaggerResponse(400, "Validation failed", typeof(List<ValidationFailure>))]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden", typeof(ProblemDetails))]
        [SwaggerResponse(404, "The discount was not found", typeof(ProblemDetails))]
        [SwaggerResponse(409, "The discount already exists", typeof(ProblemDetails))]
        public async Task<ActionResult<GetDiscountDto>> UpdateAsync(int id, [FromForm] CreateOrUpdateDiscountDto dto)
        {
            var validationResult = new CreateOrUpdateDiscountDtoValidator().Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var entity = await _discountService.UpsertAsync(dto, id);
            if (entity.Id != id)
            {
                return CreatedAtAction(nameof(GetItem), new { id = entity.Id }, entity);
            }
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete a discount by id", Description = "Permission: Admin only")]
        [SwaggerResponse(204, "The discount was deleted successfully")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden", typeof(ProblemDetails))]
        [SwaggerResponse(404, "The discount was not found", typeof(ProblemDetails))]
        public override async Task<ActionResult<Discount>> DeleteItem(int id)
        {
            return await base.DeleteItem(id);
        }
    }
}
