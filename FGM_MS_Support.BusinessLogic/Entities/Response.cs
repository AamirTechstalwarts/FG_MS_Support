using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGM_MS_Support.BusinessLogic.Entities
{
    public  class Response<T> where T : class
    {
        public BaseResponse ResponseHeader { get; set; } = new();
        public T? ResponseBody { get; set; }
    }


    public class BaseResponse
    {
        public bool Issuccess { get; set; }
        public string? Message { get; set; }
        public string? Errorcode { get; set; }
    }

    public class ResponseObject
    {
        public bool Issuccess { get; set; }
        public string? Message { get; set; }
        public string? Errorcode { get; set; }
        public Object? ResponseBody { get; set; }

    }
}
