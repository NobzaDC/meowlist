import type { Todo } from "./TodoModel";
import type { User } from "./UserModel";

export interface List {
  id: number;
  title: string;
  userId: number;
  user?: User;
  todos?: Todo[];
}