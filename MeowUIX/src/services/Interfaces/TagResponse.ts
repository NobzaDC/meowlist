import type { TodoTagResponse } from "./TodoTagResponse";
import type { UserResponse } from "./UserResponse";

export interface TagResponse {
  id: number;
  name: string;
  color?: string;
  userId: number;
  user?: UserResponse;
  todoTags?: TodoTagResponse[];
}