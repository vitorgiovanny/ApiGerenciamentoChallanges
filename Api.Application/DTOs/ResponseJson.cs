using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.DTOs
{
    public class ResponseJson
    {
        public bool Success { get; }
        public string Menssage {  get; }
        public ResponseJson(bool success, string message) 
        { 
            Success = success; 
            Menssage = message;
        }
    }
}
