using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repasitories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UnitOfWork(ICategoryInterface categoryInterface,
                            IBookInterface bookInterface,
                            AppDbContext appDbContext,
                            IMapper mapper)
        {
            Category = categoryInterface;
            Book = bookInterface;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public ICategoryInterface Category { get; }

        public IBookInterface Book { get; }

        public void Dispose()
        => GC.SuppressFinalize(this);

        public async Task SaveChangesAsync()
        => await _appDbContext.SaveChangesAsync();
    }
}
