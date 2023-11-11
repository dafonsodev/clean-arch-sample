using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<CategoryDTO> GetByIdAsync(int? id);
    Task CreateAsync(CategoryDTO categoryDto);
    Task UpdateAsync(CategoryDTO categoryDto);
    Task DeleteAsync(int? id);
}
