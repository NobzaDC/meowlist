import { type ApiResponse } from "../Interfaces/ApiResponse";
import { useNavigate } from "react-router-dom";

/**
 * Wrapper for fetch requests that checks for 401 and redirects to login.
 */
export async function fetchWithAuth<T>(
  input: RequestInfo,
  init?: RequestInit
): Promise<ApiResponse<T>> {
  try {
    const navigate = useNavigate();

    const response = await fetch(input, init);
    const data = await response.json() as ApiResponse<T>;
    if (data.code === 401 || response.status === 401) {
      navigate("/");
    }
    return data;
  } catch (error) {
    return {
      code: 500,
      value: undefined,
      message: String(error),
    };
  }
}