using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DTOs.BookDtos;

namespace DTOs.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
