﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories
{
    public class UniteOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IProductRepository Products { get;}

        public ICategoryRepository Categories { get;}

        public IPhotoRepository Photos { get; }
        public UniteOfWork(
         AppDbContext context,
         IProductRepository productRepo,
         ICategoryRepository categoryRepo,
         IPhotoRepository photoRepo)
        {
            _context = context;
            Products = productRepo;
            Categories = categoryRepo;
            Photos = photoRepo;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
