using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.WebApp.Services.Base
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}
