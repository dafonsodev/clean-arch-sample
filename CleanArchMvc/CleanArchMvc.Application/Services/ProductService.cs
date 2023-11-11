using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productsQuery = new GetProductsQuery() ?? 
            throw new Exception($"Entity could not be loaded.");

        var result = await mediator.Send(productsQuery);
        return mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetByIdAsync(int? id)
    {
        var productByIdQuery = new GetProductByIdQuery(id.Value) ?? 
            throw new Exception($"Entity could not be loaded.");

        var result = await mediator.Send(productByIdQuery);
        return mapper.Map<ProductDTO>(result);
    }

    //public async Task<ProductDTO> GetProductCategoryAsync(int? id)
    //{
    //    var productByIdQuery = new GetProductByIdQuery(id.Value) ??
    //        throw new Exception($"Entity could not be loaded.");

    //    var result = await mediator.Send(productByIdQuery);
    //    return mapper.Map<ProductDTO>(result);
    //}

    public async Task CreateAsync(ProductDTO productDto)
    {
        var productCreateCommand = mapper.Map<ProductCreateCommand>(productDto);
        await mediator.Send(productCreateCommand);
    }

    public async Task UpdateAsync(ProductDTO productDto)
    {
        var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productDto);
        await mediator.Send(productUpdateCommand);
    }

    public async Task DeleteAsync(int? id)
    {
        var productDeleteCommand = new ProductDeleteCommand(id.Value) ??
            throw new Exception($"Entity could not be loaded.");

        await mediator.Send(productDeleteCommand);
    }
}
