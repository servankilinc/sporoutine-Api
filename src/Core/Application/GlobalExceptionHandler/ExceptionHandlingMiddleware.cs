using Application.GlobalExceptionHandler.CustomExceptions;
using Application.GlobalExceptionHandler.PDetails;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Application.GlobalExceptionHandler;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware>  logger)
    {
        _next = next;
        _logger = logger;
    }
     

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        { 
            await HandleExceptionAsync(context.Response, e);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";

        Type exceptionType = exception.GetType();
        if (exceptionType == typeof(ValidationException)) return CreateValidationException(response, exception);
        if (exceptionType == typeof(BusinessException)) return CreateBusinessException(response, exception);

        return CreateInternalException(response, exception);
    }

    private Task CreateValidationException(HttpResponse response, Exception exception)
    {
        response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest); 
        IEnumerable<ValidationFailure> errors = ((ValidationException)exception).Errors;

        return response.WriteAsync(new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailTypes.Validation.ToString(),
            Title = "Validation error(s)",
            Detail = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateBusinessException(HttpResponse response, Exception exception)
    {
        response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailTypes.Business.ToString(),
            Detail = exception.InnerException != null ? exception.Message + exception.InnerException.Message : exception.Message,
            Title = "Business Wrok Flow Excepiton",
        }.ToString());
    }

    private Task CreateInternalException(HttpResponse response, Exception exception)
    {
        response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError); // 500

        return response.WriteAsync(JsonConvert.SerializeObject(new Microsoft.AspNetCore.Mvc.ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = ProblemDetailTypes.General.ToString(),
                Detail = exception.InnerException != null ? exception.Message + exception.InnerException.Message : exception.Message,
                Title = "Internal exception",
            })
        );
    }
}

