import type { TodoResponse } from "./TodoResponse";
import type { UserResponse } from "./UserResponse";

export interface ListResponse {
  id: number;
  title: string;
  userId: number;
  user?: UserResponse;
  todos?: TodoResponse[];
}