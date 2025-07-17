using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.Dtos.Product;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenaricRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<ReturnProductDto> GetAllProduct(ProductParams productParams)
        {
             if (productParams.pagesize <= 0)
                productParams.pagesize = 3;

            const int MaxPageSize = 6;
            if (productParams.pagesize > MaxPageSize)
                productParams.pagesize = MaxPageSize;

            if (productParams.pagenumber <= 0)
                productParams.pagenumber = 1;
            var query = _context.Products
                                                         .Include(p => p.Category)
                                                         .Include(p => p.Photos)
                                                         .AsNoTracking();
            if(!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ');
                query = query.Where(m => searchWords.All(word =>
                m.Name.ToLower().Contains(word.ToLower())||
                m.Description.ToLower().Contains(word.ToLower())
                ));
            }
            if (productParams.categoryid.HasValue)
                query = query.Where(p=>p.CategoryId == productParams.categoryid);
            if (!string.IsNullOrEmpty(productParams.sort)) 
            {
                query = productParams.sort switch
                {
                    "PriceAce" => query.OrderBy(p => p.NewPrice),
                    
                    "PriceDce" => query.OrderByDescending(p => p.NewPrice),
                    _ => query.OrderBy(p => p.Name),
                };
            }
            ReturnProductDto returnProductDto = new ReturnProductDto();
            returnProductDto.TotalCount = await query.CountAsync();

            query = query.Skip((productParams.pagesize) *(productParams.pagenumber - 1)).Take(productParams.pagesize);
              returnProductDto.productDtos = _mapper.Map<List<ProductDto>>(query);
            return returnProductDto;
        }

        public async Task<bool> AddProductAsync(AddProductDto addProduct)
        {
            if (addProduct == null) return false;
            var product = _mapper.Map<Product>(addProduct);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var ImgPath = await _imageManagementService.AddImgAsync(addProduct.Photos, addProduct.Name);
            var photos = new List<Photo>();
            foreach (var path in ImgPath)
            {
                photos.Add(new Photo
                {
                    ImgName = path,
                    ProductId = product.Id
                });
            }
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateProductAsync(UpdateProductDto updateProduct)
        {
            if (updateProduct == null) return false;
            var findproduct = await _context.Products
                                                   .Include(p=>p.Category)
                                                   .Include(p=>p.Photos)
                                                   .FirstOrDefaultAsync(p=>p.Id == updateProduct.Id);
            if (findproduct == null) return false;
            _mapper.Map(updateProduct, findproduct);


            var findphoto = await _context.Photos.Where(p=>p.ProductId ==updateProduct.Id).ToListAsync();
            foreach (var item in findphoto) 
            {
                _imageManagementService.DeleteImgAsync(item.ImgName);
            }
            _context.Photos.RemoveRange(findphoto);


            var imgpath = await _imageManagementService.AddImgAsync(updateProduct.Photos, updateProduct.Name);
            var photo = imgpath.Select(imgpath => new Photo
            {
                ImgName = imgpath,
                ProductId = updateProduct.Id,
            });
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task DeleteProductAsync(Product product)
        {
            var photos = await _context.Photos.Where(p=>p.ProductId == product.Id).ToListAsync();
            foreach (var photo in photos)
            {
                _imageManagementService.DeleteImgAsync(photo.ImgName);
            }
            _context.Remove(product);
            await _context.SaveChangesAsync();

        }

       
    }
}
