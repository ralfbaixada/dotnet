using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Net.Http;

namespace Exceptions
{
    public class PreconditionFailedException : HttpRequestException
    {
        public BadRequestObjectResult BadRequestObjectResult { get; }

        public PreconditionFailedException(BadRequestObjectResult badRequestObjectResult, Exception innerException = default) : base("", innerException)
        {
            BadRequestObjectResult = badRequestObjectResult;
            BadRequestObjectResult.StatusCode = (int)HttpStatusCode.PreconditionFailed;
        }

        public PreconditionFailedException(ModelStateDictionary modelStateDictionary, Exception innerException = default) : this(new BadRequestObjectResult(modelStateDictionary), innerException)
        {
        }

        public PreconditionFailedException(string key, string errorMessage, Exception innerException = default) : this(GetModelStateDictionary(key, errorMessage), innerException)
        {
        }

        private static ModelStateDictionary GetModelStateDictionary(string key, string errorMessage)
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError(key, errorMessage);
            return modelStateDictionary;
        }

        public static void Throw(string key, string errorMessage, Exception innerException = default)
        {
            throw new PreconditionFailedException(key, errorMessage, innerException);
        }

        public static void Throw(ModelStateDictionary modelStateDictionary, Exception innerException = default)
        {
            throw new PreconditionFailedException(modelStateDictionary, innerException);
        }

        public static void ThrowObject(object error, Exception innerException = default)
        {
            throw new PreconditionFailedException(new BadRequestObjectResult(error), innerException);
        }
    }
}
