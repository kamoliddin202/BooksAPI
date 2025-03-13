using AutoMapper;
using BusinessLogicLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.BookDtos;

namespace BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, 
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddBookAsync(AddBookDto book)
        {
            var mapped = _mapper.Map<Book>(book);
            await _unitOfWork.Book.AddEntityAsync(mapped);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Book.GetEntityByIdAsync(id);
            if(book == null) 
                throw new KeyNotFoundException(nameof(book));
            await _unitOfWork.Book.DeleteEntityAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedList<BookDto>> Filter(FilterParametres filterParametres)
        {
            var list = await _unitOfWork.Book.GetAllEntitiesAsync();

            if (filterParametres.title is not null)
            {
                list = list.Where(c => c.Title.ToLower().Contains(filterParametres.title.ToLower()));
            }

            list = list.Where(c => c.Price >= filterParametres.minNumber &&
                                               c.Price <= filterParametres.maxNumber);

            var dtos = list.Select(c => _mapper.Map<BookDto>(c)).ToList();


            if (filterParametres.orderBy)
            {
                dtos = dtos.OrderBy(c => c.Title).ToList();
            }
            else
            {
                dtos = dtos.OrderByDescending(c => c.Title).ToList();
            }

            PagedList<BookDto> paged = new(dtos, dtos.Count(), filterParametres.pageSize, filterParametres.pageNumber);

            return paged.ToPagedList(dtos, filterParametres.pageSize, filterParametres.pageNumber);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await  _unitOfWork.Book.GetAllEntitiesAsync();
            return books.Select(c => _mapper.Map<BookDto>(c));  
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Book.GetEntityByIdAsync(id);
            if (book == null) 
                throw new KeyNotFoundException(nameof(book));
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateBookAsync(UpdateBookDto book)
        {
            var existbook = await _unitOfWork.Book.GetEntityByIdAsync(book.Id);
            if(existbook == null)
                throw new KeyNotFoundException(nameof(existbook));  

            _mapper.Map(book, existbook);
            await _unitOfWork.Book.UpdateEntityAsync(existbook);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
