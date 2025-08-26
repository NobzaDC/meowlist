import type { ListResponse } from "./ListResponse";
import type { TagResponse } from "./TagResponse";

export interface UserResponse {
  id: number;
  name: string;
  email: string;
  passwordHash: string;
  displayName: string;
  isAdmin: boolean;
  lists?: ListResponse[];
  tags?: TagResponse[];
}