using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public class CategoryException : Exception
    {
        private readonly string ErrorMessage;

        public CategoryException(string message): base(message)
        {
            ErrorMessage = message;
        }
    }
}
