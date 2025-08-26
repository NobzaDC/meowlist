namespace MeowCore.Models
{

    /// <summary>
    /// DTO for JWT token response.
    /// </summary>
    public class JwtTokenResponse
    {
        /// <summary>
        /// Id of the user doing the request
        /// </summary>
        public string user_id { get; set; } = string.Empty;
        /// <summary>
        /// JWT access token string.
        /// </summary>
        public string access_token { get; set; } = string.Empty;

        /// <summary>
        /// Token type, usually "Bearer".
        /// </summary>
        public string token_type { get; set; } = "Bearer";

        /// <summary>
        /// Expiration time in seconds.
        /// </summary>
        public int expires_in { get; set; }
    }
}
