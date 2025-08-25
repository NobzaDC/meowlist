export interface ApiResponse<T> {
  code: number;
  value?: T;
  message: string;
}