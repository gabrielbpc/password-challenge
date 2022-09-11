using System.Text.Json.Serialization;

namespace User.Api.Dto.Request
{
    /// <summary>
    /// Representa uma requisição para criar um usuario
    /// </summary>
    public class CreateAnUserRequest
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        [JsonPropertyName("nome")]
        public string Name { get; set; }
        /// <summary>
        /// Email do usuario
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
