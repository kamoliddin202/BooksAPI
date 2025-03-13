using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Common;
using DTOs.BookDtos;

namespace BusinessLogicLayer.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>> Filter(FilterParametres filterParametres);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task AddBookAsync(AddBookDto book);
        Task UpdateBookAsync(UpdateBookDto book);
        Task DeleteBookAsync(int id);
    }
}
