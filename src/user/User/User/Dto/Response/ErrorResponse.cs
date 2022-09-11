namespace User.Api.Dto.Response
{
    /// <summary>
    /// Representa um erro
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="description"></param>
        public ErrorResponse(string description)
        {
            Description = description;
        }
        /// <summary>
        /// Descrição do erro
        /// </summary>
        public string Description { get; }
    }
}
