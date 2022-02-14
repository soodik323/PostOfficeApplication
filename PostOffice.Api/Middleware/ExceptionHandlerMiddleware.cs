using System;
using System.Net;
using System.Threading.Tasks;
using FinanceReporter.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PostOffice.Application.Responses;

namespace PostOffice.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                //case ValidationException validationException:
                //    httpStatusCode = HttpStatusCode.BadRequest;
                //    result = JsonConvert.SerializeObject(validationException.ValdationErrors);
                //    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                //case Exception ex:
                //    httpStatusCode = HttpStatusCode.BadRequest;
                //    break;
            }

            context.Response.StatusCode = (int) httpStatusCode;

            _logger.LogError((int)httpStatusCode, exception, exception.Message);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var returnValue = JsonConvert.SerializeObject(new ErrorResponse((int)httpStatusCode, httpStatusCode.ToString(), exception.Message), jsonSerializerSettings);

            return context.Response.WriteAsync(returnValue);
        }
    }
}
