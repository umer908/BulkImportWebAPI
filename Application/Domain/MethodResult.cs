using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class MethodResult
    {
        public List<ErrorResponse> Errors { get; set; } = new List<ErrorResponse>();
    }

    public class ErrorResponse
    {
        public int OperationId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
