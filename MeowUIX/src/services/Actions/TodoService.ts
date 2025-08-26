import { fetchWithAuth } from "./fetchWithAuth";
import type { ApiResponse } from "../Interfaces/ApiResponse";
import type { TodoRequestDto } from "../Interfaces/TodoRequestDto";
import type { TodoResponse } from "../Interfaces/TodoResponse";


export async function createTodo(
  todo: TodoRequestDto
): Promise<ApiResponse<TodoResponse>> {
  const myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Accept", "text/plain");

  const raw = JSON.stringify(todo);

  const requestOptions: RequestInit = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  return await fetchWithAuth<TodoResponse>(
    "https://localhost:7279/api/Todos/CreateTodo",
    requestOptions
  );  
}

export async function getMyTodos(): Promise<ApiResponse<TodoResponse[]>> {
  const user_id = localStorage.getItem("user_id");
  const myHeaders = new Headers();
  myHeaders.append("Accept", "text/plain");

  const requestOptions: RequestInit = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  return await fetchWithAuth<TodoResponse[]>(
    `https://localhost:7279/api/Todos/GetTodosByUser/user/${user_id}`,
    requestOptions
  );
}

/**
 * Update a todo by ID using the stored JWT token.
 */
export async function updateTodo(
  todoId: number,
  todo: TodoRequestDto
): Promise<ApiResponse<TodoResponse>> {
  const myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Accept", "text/plain");

  const raw = JSON.stringify(todo);

  const requestOptions: RequestInit = {
    method: "PUT",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  return await fetchWithAuth<TodoResponse>(
    `https://localhost:7279/api/Todos/UpdateTodo/${todoId}`,
    requestOptions
  );
}

/**
 * Delete a todo by ID using the stored JWT token.
 */
export async function deleteTodo(
  todoId: number
): Promise<ApiResponse<null>> {
  const myHeaders = new Headers();
  myHeaders.append("Accept", "text/plain");

  const requestOptions: RequestInit = {
    method: "DELETE",
    headers: myHeaders,
    redirect: "follow",
  };

  return await fetchWithAuth<null>(
    `https://localhost:7279/api/Todos/DeleteTodo/${todoId}`,
    requestOptions
  );
}