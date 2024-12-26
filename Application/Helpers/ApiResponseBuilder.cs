using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ApiResponseBuilder
    {
        public static ApiResponse GenerateOK(object data, string message, string description)
        {
            return new ApiResponse(data, (int)HttpStatusCode.OK, "OK", message, description);
        }
        public static ApiResponse GenerateBadRequest(string message, string description)
        {
            return new ApiResponse(null, (int)HttpStatusCode.BadRequest, "BadRequest", message, description);
        }
        public static ApiResponse GenerateUnauthorized(string message, string description)
        {
            return new ApiResponse(null, (int)HttpStatusCode.Unauthorized, "Unauthorized", message, description);
        }
        public static ApiResponse GenerateForbidden(string message, string description)
        {
            return new ApiResponse(null, (int)HttpStatusCode.Forbidden, "Forbidden", message, description);
        }
        public static ApiResponse GenerateNotFound(string message, string description)
        {
            return new ApiResponse(null, (int)HttpStatusCode.NotFound, "Not Found", message, description);
        }
        public static ApiResponse GenerateInternalServerError(object data, string message, string description)
        {
            return new ApiResponse(data, (int)HttpStatusCode.InternalServerError, "Internal Server Error", message, description);
        }
    }
}
