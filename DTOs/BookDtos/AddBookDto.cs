using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DTOs.BookDtos
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
