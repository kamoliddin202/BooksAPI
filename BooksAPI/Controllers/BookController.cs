using BusinessLogicLayer.Common;
using BusinessLogicLayer.Services;
using DTOs.BookDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);    
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateBookDto updateBookDto)
        {
            await _bookService.UpdateBookAsync(updateBookDto);
            return Ok("Updated successfully !");
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddBookDto addBookDto)
        {
            await _bookService.AddBookAsync(addBookDto);
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return Ok("Deleted successfully !");
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterParametres filterParametres)
        {
            var fiters = await _bookService.Filter(filterParametres);
            return Ok(fiters);
        }

    }

}
