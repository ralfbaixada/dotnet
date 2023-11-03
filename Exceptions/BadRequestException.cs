using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net.Http;

namespace Exceptions
{
    public class BadRequestException : HttpRequestException
    {
        public BadRequestObjectResult BadRequestObjectResult { get; }

        public BadRequestException(BadRequestObjectResult badRequestObjectResult, Exception innerException = default) : base("", innerException)
        {
            BadRequestObjectResult = badRequestObjectResult;
        }

        public static void Throw(string key, string errorMessage, Exception innerException = default)
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError(key, errorMessage);
            Throw(modelStateDictionary, innerException);
        }

        public static void Throw(ModelStateDictionary modelStateDictionary, Exception innerException = default)
        {
            var badRequestObjectResult = new BadRequestObjectResult(modelStateDictionary);
            throw new BadRequestException(badRequestObjectResult, innerException);
        }
    }
}
