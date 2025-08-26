/**
 * DTO for JWT token response.
 */
export interface JwtTokenResponse {


  /**
   * Id of the user doing the request
   */
  user_id: string;
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