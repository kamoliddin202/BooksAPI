using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public record FilterParametres(int pageSize,
                                int pageNumber,
                                int maxNumber,
                                int minNumber,
                                bool orderBy,
                                string Title)
    {
        public  int pageSize = pageSize;
        public  int pageNumber = pageNumber;
        public  int maxNumber = maxNumber;
        public  int minNumber = minNumber;
        public  bool orderBy = orderBy;
        public  string title = Title;
    }
}
