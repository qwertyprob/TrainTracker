export interface ApiResponse<T = any> {
  data?: T;
  message: string;
  status: number;
  headers: Headers;
  error?: string;
}

export interface ApiConfig {
  baseUrl?: string;
  defaultHeaders?: Record<string, string>;
  timeout?: number;
}
