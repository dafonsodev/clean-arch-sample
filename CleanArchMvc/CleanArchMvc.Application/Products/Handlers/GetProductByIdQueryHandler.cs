using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository ??
            throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(GetProductByIdQuery request, 
        CancellationToken cancellationToken)
    {
        return await productRepository.GetByIdAsync(request.Id);
    }
}
