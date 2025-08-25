import type { List } from "./ListModel";
import type { Tag } from "./TagModel";

export interface User {
  id: number;
  name: string;
  email: string;
  passwordHash: string;
  displayName: string;
  isAdmin: boolean;
  lists?: List[];
  tags?: Tag[];
}