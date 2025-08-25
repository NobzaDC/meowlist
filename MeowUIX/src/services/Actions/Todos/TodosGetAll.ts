import { fetchWithAuth } from "../fetchWithAuth";
import type { ApiResponse } from "../../Interfaces/ApiResponse";
import type { Todo } from "../../Interfaces/TodoModel";

/**
 * Fetch all todos using the stored JWT token.
 */
export async function getAllTodos(): Promise<ApiResponse<Todo[]>> {
    const token = localStorage.getItem("token");
    const myHeaders = new Headers();
    myHeaders.append("Accept", "text/plain");
    myHeaders.append("Authorization", `Bearer ${token}`);

    const requestOptions: RequestInit = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow",
    };

    return await fetchWithAuth<Todo[]>(
        "https://localhost:7279/api/Todos/GetTodos",
        requestOptions
    );
}