using Core.Interface.Repositories;
using Core.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return  FindAll();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await FindByCondition(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await Create(product);
        }

        public async Task UpdateAsync(Product product)
        {
             Update(product);
        }

        public async Task DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                Delete(product);
            }
        }
    }
}
