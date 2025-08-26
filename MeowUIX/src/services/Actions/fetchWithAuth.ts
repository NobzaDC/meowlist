import { type ApiResponse } from "../Interfaces/ApiResponse";

/**
 * Wrapper for fetch requests that checks for 401 and redirects to login.
 */
export async function fetchWithAuth<T>(
  input: RequestInfo,
  init?: RequestInit
): Promise<ApiResponse<T>> {
  try {
    const token = localStorage.getItem("token");
    const myHeaders = new Headers(init?.headers);
    if (token) {
      myHeaders.append("Authorization", `Bearer ${token}`);
    } 

    const response = await fetch(input, { ...init, headers: myHeaders });
    if (response.status === 401) {
      window.location.href = "/";
    }
    const data = await response.json() as ApiResponse<T>;
    return data;
  } catch (error) {
    return {
      code: 500,
      value: undefined,
      message: String(error),
    };
  }
}