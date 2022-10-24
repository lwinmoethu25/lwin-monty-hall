using System;
using System.Text.Json;

namespace LwinMontyHall.Models
{
    public class ExceptionDetail
    {
        public readonly int StatusCode;
        public readonly string Message;

        public ExceptionDetail(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
