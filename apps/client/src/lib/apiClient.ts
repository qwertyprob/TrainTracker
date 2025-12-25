// biome-ignore assist/source/organizeImports: <not sorted>
import { API_TIMEOUT, API_URL } from "@/constants/api";
import type { ApiResponse, ApiConfig } from "@/types/api";

export class ApiClient {
  private config: Required<ApiConfig>;

  constructor(config: ApiConfig = {}) {
    this.config = {
      baseUrl: config.baseUrl || API_URL,
      defaultHeaders: {
        "Content-Type": "application/json",
        ...config.defaultHeaders,
      },
      timeout: config.timeout || API_TIMEOUT,
    };
  }
  private async makeRequest<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<ApiResponse<T>> {
    const controller = new AbortController();
    const id = setTimeout(() => controller.abort(), this.config.timeout);

    try {
      const res = await fetch(`${this.config.baseUrl}${endpoint}`, {
        ...options,
        headers: { ...this.config.defaultHeaders, ...(options.headers || {}) },
        signal: controller.signal,
      });

      const response = await res.json().catch(() => null);

      if (!res.ok) {
        return {
          data: null,
          status: res.status,
          headers: res.headers,
          error: response.message,
        } as ApiResponse;
      }

      return {
        data: response.data,
        message: response.message,
        status: res.status,
        headers: res.headers,
      } as ApiResponse;
    } finally {
      clearTimeout(id);
    }
  }

  async get<T>(endpoint: string, headers?: Record<string, string>) {
    return this.makeRequest<T>(endpoint, { method: "GET", headers });
  }

  async post<T>(
    endpoint: string,
    body?: any,
    headers?: Record<string, string>
  ) {
    return this.makeRequest<T>(endpoint, {
      method: "POST",
      body: JSON.stringify(body),
      headers,
    });
  }

  async put<T>(endpoint: string, body?: any, headers?: Record<string, string>) {
    return this.makeRequest<T>(endpoint, {
      method: "PUT",
      body: JSON.stringify(body),
      headers,
    });
  }

  async delete<T>(endpoint: string, headers?: Record<string, string>) {
    return this.makeRequest<T>(endpoint, { method: "DELETE", headers });
  }
}
