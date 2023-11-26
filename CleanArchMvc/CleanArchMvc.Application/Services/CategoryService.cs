using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        var categoriesEntity = await categoryRepository.GetCategoriesAsync();
        return mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<CategoryDTO> GetByIdAsync(int? id)
    {
        var categoryEntity = await categoryRepository.GetByIdAsync(id);
        return mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task CreateAsync(CategoryDTO categoryDto)
    {
        var categoryEntity = mapper.Map<Category>(categoryDto);
        await categoryRepository.CreateAsync(categoryEntity);
        categoryDto.Id = categoryEntity.Id;
    }

    public async Task UpdateAsync(CategoryDTO categoryDto)
    {
        var categoryEntity = mapper.Map<Category>(categoryDto);
        await categoryRepository.UpdateAsync(categoryEntity);
    }

    public async Task DeleteAsync(int? id)
    {
        var categoryEntity = categoryRepository.GetByIdAsync(id).Result;
        await categoryRepository.DeleteAsync(categoryEntity);
    }
}
