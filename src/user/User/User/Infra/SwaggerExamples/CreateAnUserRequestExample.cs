using Swashbuckle.AspNetCore.Filters;
using User.Api.Dto.Request;

namespace User.Api.Infra.SwaggerExamples
{
    /// <summary>
    /// Provedor de exemplo de criação de usuario
    /// </summary>
    public class CreateAnUserRequestExample : IExamplesProvider<CreateAnUserRequest>
    {
        /// <summary>
        /// Retorna um exemplo de requisição para criar usuario 
        /// </summary>
        /// <returns></returns>
        public CreateAnUserRequest GetExamples()
        {
            return new CreateAnUserRequest { Name = "Gabriel", Email = "gabriel@invalid.com" };
        }
    }
}
