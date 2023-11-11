using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository productRepository;

    public ProductUpdateCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository ?? 
            throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductUpdateCommand request, 
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        
        if (product == null)
        {
            throw new ApplicationException($"Entity could not be found");
        }
        else
        {
            product.Update(request.Name, request.Description, request.Price, 
                request.Stock, request.Image, request.CategoryId);

            return await productRepository.UpdateAsync(product);
        }
    }
}
