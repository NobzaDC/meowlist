import type { TodoTag } from "./TodoTag";
import type { User } from "./UserModel";

export interface Tag {
  id: number;
  name: string;
  color?: string;
  userId: number;
  user?: User;
  todoTags?: TodoTag[];
}