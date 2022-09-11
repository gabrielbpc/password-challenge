using System.Text.Json.Serialization;

namespace User.Api.Dto.Response
{
    /// <summary>
    /// Representa um usuario
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Id do usuario
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
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
