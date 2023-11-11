using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Product>
{
    private readonly IProductRepository productRepository;

    public ProductDeleteCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository ??
            throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductDeleteCommand request, 
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product == null) 
        {
            throw new ApplicationException($"Entity could not be found");
        }
        else
        {
            var result = await productRepository.DeleteAsync(product);
            return result;
        }
    }
}
