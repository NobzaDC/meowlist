import type { List } from "./ListModel";
import type { TodoTag } from "./TodoTag";

export type TodoStatus = "Pending" | "InProgress" | "Completed" | "Archived";

export interface Todo {
  id: number;
  title: string;
  description?: string;
  status: TodoStatus;
  listId: number;
  list?: List;
  todosTags?: TodoTag[];
}