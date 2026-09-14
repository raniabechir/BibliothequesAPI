

using Bibliotheques.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotheques.Api
{
    public class GestionnaireExceptionsGlobal : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException valEx)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Erreur de validation",
                    Detail = valEx.Message,
                    Type = "https://api.exemple.ca/erreurs/validation",
                    Instance = httpContext.Request.Path
                };

                problemDetails.Extensions["errors"] = new Dictionary<string, string[]>
            {
                { valEx.Champ, new[] { valEx.Message } }
            };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
