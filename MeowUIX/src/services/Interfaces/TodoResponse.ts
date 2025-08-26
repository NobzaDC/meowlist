import type { ListResponse } from "./ListResponse";
import type { TodoTagResponse } from "./TodoTagResponse";

export type TodoStatus = 0 | 1 | 2 | 3; {/* 0: Pending, 1: InProgress, 2: Completed, 3: Archived */}

export interface TodoResponse {
  id: number;
  title: string;
  description?: string;
  status: TodoStatus;
  listId: number;
  list?: ListResponse;
  todosTags?: TodoTagResponse[];
}