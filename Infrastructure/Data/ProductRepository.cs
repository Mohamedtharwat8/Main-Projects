using Core.Entities;
using Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        public Task<IReadOnlyList<Product>> GetProductsAsync()
        {
<<<<<<< Updated upstream
            throw new NotImplementedException();
=======
            return await _context.Products
                .Include(p=>p.ProductBrand)
                .Include(p=>p.ProductType)
                .ToListAsync();
>>>>>>> Stashed changes
        }
        public Task<Product> GetProductByIdAsync(int id)
        {
<<<<<<< Updated upstream
            throw new NotImplementedException();
=======
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.Id == id);
>>>>>>> Stashed changes
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
