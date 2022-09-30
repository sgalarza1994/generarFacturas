using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Security
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }

    }
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

}
