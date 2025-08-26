import type { ApiResponse } from "../../Interfaces/ApiResponse";
import type { AuthenticationRequest } from "../../Interfaces/AuthenticationRequest";
import type { JwtTokenResponse } from "../../Interfaces/JwtTokenResponse";

/**
 * Login user and get JWT token response.
 */
export async function loginUser(
  credentials: AuthenticationRequest
): Promise<ApiResponse<JwtTokenResponse>> {
  const myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Accept", "text/plain");

  const raw = JSON.stringify(credentials);

  const requestOptions: RequestInit = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  try {
    const response = await fetch("https://localhost:7279/api/Users/Login", requestOptions);
    const result = await response.json();
    return result as ApiResponse<JwtTokenResponse>;
  } catch (error) {
    return {
      code: 500,
      value: undefined,
      message: String(error),
    };
  }
}