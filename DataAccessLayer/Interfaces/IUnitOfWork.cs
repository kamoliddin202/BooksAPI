using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryInterface Category { get; }
        IBookInterface Book { get; }
        Task SaveChangesAsync();
    }
}
