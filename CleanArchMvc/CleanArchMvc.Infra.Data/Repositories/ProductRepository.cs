using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;

    public ProductRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        //return await _context.Products.FindAsync(id);
        return await context.Products
            .Include(c => c.Category).SingleOrDefaultAsync(x => x.Id == id);
    }

    //public async Task<Product> GetProductCategoryAsync(int? id)
    //{
    //    return await _context.Products
    //        .Include(c => c.Category).SingleOrDefaultAsync(x => x.Id == id);
    //}

    public async Task<Product> CreateAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> DeleteAsync(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }
}
