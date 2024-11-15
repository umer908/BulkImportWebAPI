﻿namespace Application.Domain
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
