import type { Tag } from "./TagModel";
import type { Todo } from "./TodoModel";

export interface TodoTag {
  todoId: number;
  tagId: number;
  todo?: Todo;
  tag?: Tag;
}