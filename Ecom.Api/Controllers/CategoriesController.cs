using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos.Category;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("Get-All-Category")]
        public async Task<IActionResult> GetAllCategory() {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();
                if (categories is null) return BadRequest(new ApiResponse(400));
                return Ok(categories);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-Category-ById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetByIdAsync(id);
                if (categories is null) return BadRequest(new ApiResponse(400, $"Not Found Category Id By The Id={id}"));
                return Ok(categories);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Insert-Category")]
        public async Task<IActionResult> InsertCategory(AddCategoryDto addCategoryDto)
        {
            try
            {
                
                var category =  _mapper.Map<Category>(addCategoryDto);
                await _unitOfWork.Categories.AddAsync(category);
                var Idnew = category.Id;
                return Ok(new ApiResponse(200, $"The Category Inserted Successfully with the Id Number {Idnew}"));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(updateCategoryDto);
                await _unitOfWork.Categories.UpdateAsync(category);
                var Id = updateCategoryDto.Id;
                return Ok(new ApiResponse(200, $"The Category with the Id Number {Id} Updated Successfully"));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Delete-Category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.Categories.DeleteAsync(id);
                return Ok(new ApiResponse(200, "The Category Deleted Successfully"));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
