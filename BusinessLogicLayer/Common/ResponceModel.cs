using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public class ResponceModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ExpiredTime { get; set; }
        public List<string> Errors { get; set; }
    }
}
