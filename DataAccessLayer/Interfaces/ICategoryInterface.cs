using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface ICategoryInterface : IInterface<Category>
    {
        Task<IEnumerable<Category>> GetCategoryWithBooksAsync();
    }
}
