using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Classes.DTO
{
    public class BaseResult
    {
        public string Error { get; set; } = "";
        public bool Ok { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public void SetError()
        {
            Ok = false;
            Error = "Error";
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public void SetError(string error, HttpStatusCode statusCode)
        {
            Ok = false;
            Error = error;
            StatusCode = statusCode;
        }
        
        public void SetError(string error)
        {
            Ok = false;
            Error = error;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
