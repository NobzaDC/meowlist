import type { TagResponse } from "./TagResponse";
import type { TodoResponse } from "./TodoResponse";

export interface TodoTagResponse {
  todoId: number;
  tagId: number;
  todo?: TodoResponse;
  tag?: TagResponse;
}