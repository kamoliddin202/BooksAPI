using AutoMapper;
using Azure;
using BusinessLogicLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.CategoryDtos;

namespace BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, 
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddCategoryAsync(AddCategoryDto categoryDto)
        {
            if (categoryDto == null)
                throw new ArgumentNullException(nameof(categoryDto));
            var mapped = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Category.AddEntityAsync(mapped);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Category.GetEntityByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            await _unitOfWork.Category.DeleteEntityAsync(category.Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var cateogries = await _unitOfWork.Category.GetAllEntitiesAsync();
            if (cateogries == null)
                throw new ArgumentNullException("Categorylarni olib bo'lmadi !");
            return cateogries.Select(c => _mapper.Map<CategoryDto>(c));
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync()
        {
            var categories = await _unitOfWork.Category.GetCategoryWithBooksAsync();
            return categories.Select(c => _mapper.Map<CategoryDto>(c));
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category =await _unitOfWork.Category.GetEntityByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<PagedList<CategoryDto>> GetPagedList(int pagesize, int pageNumber)
        {
            var categories = await _unitOfWork.Category.GetAllEntitiesAsync();

            var dtos = categories.Select(c => _mapper.Map<CategoryDto>(c)).ToList();

            PagedList<CategoryDto> pagedList = new(dtos, dtos.Count(), pagesize, pageNumber);

            return pagedList.ToPagedList(dtos, pagesize, pageNumber);
        }

        public async Task SeedData(AddCategoryDto addCategoryDto)
        {
            for (int i = 0; i < 1000; i++)
            {
                addCategoryDto.Name = i.ToString();
                var mapped = _mapper.Map<Category>(addCategoryDto);
                await _unitOfWork.Category.AddEntityAsync(mapped);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var category = await _unitOfWork.Category.GetEntityByIdAsync(categoryDto.Id);   
            if (category == null)
                throw new KeyNotFoundException(nameof(category));
            _mapper.Map(categoryDto, category);
            await _unitOfWork.Category.UpdateEntityAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
