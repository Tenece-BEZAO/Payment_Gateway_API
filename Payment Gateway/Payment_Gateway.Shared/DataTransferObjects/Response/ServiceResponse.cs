using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Response
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public IEnumerable<string> Errors { get; set; }

        public ServiceResponse(T data)
        {
            Data = data;
        }

        public ServiceResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public ServiceResponse()
        {

        }
    }
}
