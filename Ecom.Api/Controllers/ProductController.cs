using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("Get-All-Product")]
        public async Task<IActionResult> GetAllProduct([FromQuery]ProductParams productParams)
        {
            try
            {
                int totalCount = await _unitOfWork.Products.CountAsync();
                var products = await _unitOfWork.Products.GetAllProduct(productParams);
                var paginated = new Pagination<ProductDto>
                {
                    PageNumber = productParams.pagenumber,
                    PageSize = productParams.pagesize,
                    TotalCount = totalCount,
                    Data = products
                };

                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpGet("Get-Product-ById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id,x => x.Photos, x => x.Category!);
                if (product is null) return NotFound(new ApiResponse(400));

                var productDtoList = _mapper.Map<ProductDto>(product); 
                return Ok(productDtoList);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto) 
        {
            try
            {
                var product = await _unitOfWork.Products.AddProductAsync(addProductDto);
                return Ok(product);
            }
            catch (Exception ex)
            {

              return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProduct)
        {
            try
            {
                var product = await _unitOfWork.Products.UpdateProductAsync(updateProduct);
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Product/{id}")]

        public async Task<IActionResult> DeleteProduct(int id) 
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id, p => p.Category, product => product.Photos);
               await _unitOfWork.Products.DeleteProductAsync(product);
                return Ok(new ApiResponse(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
    }
