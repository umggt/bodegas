using Bodegas.Exceptions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bodegas.Middlewares
{
    // tomado de http://stackoverflow.com/a/31054664 
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error handler will not be executed.");
                    throw;
                }

                if (ex is RegistroNoEncontradoException)
                {
                    try
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        //context.Response.Headers.Clear();
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { errores = new[] { ex.Message } }));
                        return;
                    }
                    catch (Exception exRegistroNoEncontrado)
                    {
                        _logger.LogError($"Ocurrió un error mientras se procesaba la lógica de RegistroNoEncontradoException {ex.Message}.", exRegistroNoEncontrado);
                        throw;
                    }
                }

                if (ex is InvalidOperationException)
                {
                    try
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { errores = new[] { ex.Message } }));
                        return;
                    }
                    catch (Exception exInvalidOperationException)
                    {
                        _logger.LogError($"Ocurrió un error mientras se procesaba la lógica de InvalidOperationException {ex.Message}.", exInvalidOperationException);
                        throw;
                    }
                }

                _logger.LogError($"An unhandled exception has occurred: {ex.Message}", ex);
                throw; // Re-throw the original if we couldn't handle it
            }
        }
    }
}
