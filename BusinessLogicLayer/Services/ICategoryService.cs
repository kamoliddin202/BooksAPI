using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Common;
using DTOs.CategoryDtos;

namespace BusinessLogicLayer.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync();
        Task<PagedList<CategoryDto>> GetPagedList(int pagesize, int pageNumber);
        Task SeedData(AddCategoryDto addCategoryDto);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id); 
        Task AddCategoryAsync(AddCategoryDto categoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
