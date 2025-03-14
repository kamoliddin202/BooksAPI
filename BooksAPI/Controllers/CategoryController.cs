using BusinessLogicLayer.Common;
using BusinessLogicLayer.Services;
using DTOs.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return  Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCategoryDto addCategoryDto)
        {
            try
            {
                await _categoryService.AddCategoryAsync(addCategoryDto);
                return Created();
            }
            catch (CategoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                return Ok("Category Updated !");
            }
            catch (CategoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok("Successfully deleted !");
        }

        [HttpGet("categorywithbooks")]
        public async Task<IActionResult> GetWithBooks()
        {
            var categories = await _categoryService.GetCategoriesWithBooksAsync();
            return Ok(categories);
        }

        [HttpPost("SeedData")]
        public async Task<IActionResult> SeedData(AddCategoryDto categoryDto)
        {
            await _categoryService.SeedData(categoryDto);
            return Ok("Added Successfully !");
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageSize, int pageNumber)
        {
            var paged = await _categoryService.GetPagedList(pageSize, pageNumber);

            var metadata = new
            {
                paged.TotalCount,
                paged.PageSize,
                paged.CurrentPage,
                paged.HasNext,
                paged.HasPrevious,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));    

            return Ok(paged.Data);

        }
    }

}
