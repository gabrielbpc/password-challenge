using Swashbuckle.AspNetCore.Filters;
using User.Api.Dto.Response;

namespace User.Api.Infra.SwaggerExamples
{
    /// <summary>
    /// Provedor de exemplo de erro
    /// </summary>
    public class ErrorResponseExample : IExamplesProvider<ErrorResponse>
    {
        /// <summary>
        /// Retorna um exemplo de erro
        /// </summary>
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse("Ocorreu um erro inesperado.");
        }
    }
}
