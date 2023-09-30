using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.WebApi.Data.DTO
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessge { get; set; }
        public string Token { get; set; }
        public Object Data { get; set; }
    }
}
