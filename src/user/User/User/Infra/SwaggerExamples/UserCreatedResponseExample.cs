using Swashbuckle.AspNetCore.Filters;
using System;
using User.Api.Dto.Response;

namespace User.Api.Infra.SwaggerExamples
{
    /// <summary>
    /// Provedor de exemplos para usuario criado
    /// </summary>
    public class UserCreatedResponseExample : IExamplesProvider<UserResponse>
    {
        /// <summary>
        /// Retorna exemplo de usuario criado
        /// </summary>
        /// <returns></returns>
        public UserResponse GetExamples()
        {
            return new UserResponse { Email = "gabriel@invalid.com", Name = "Gabriel", Id = Guid.NewGuid().ToString() };
        }
    }
}
