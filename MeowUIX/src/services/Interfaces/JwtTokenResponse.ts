/**
 * DTO for JWT token response.
 */
export interface JwtTokenResponse {
  /**
   * JWT access token string.
   */
  access_token: string;

  /**
   * Token type, usually "Bearer".
   */
  token_type: string;

  /**
   * Expiration time in seconds.
   */
  expires_in: number;
}