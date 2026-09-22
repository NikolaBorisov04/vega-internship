const API_URL = import.meta.env.VITE_API_URL;

if (!API_URL) {
  throw new Error("VITE_API_URL is not configured.");
}

export class ApiError extends Error {
  public readonly status: number;
  public readonly body: unknown;

  constructor(status: number, body: unknown) {
    super(`API request failed with status ${status}`);
    this.name = "ApiError";
    this.status = status;
    this.body = body;
  }
}

function buildUrl(path: string): string {
  const baseUrl = API_URL.replace(/\/$/, "");
  const normalizedPath = path.startsWith("/") ? path : `/${path}`;

  return `${baseUrl}${normalizedPath}`;
}

async function request<T>(
  path: string,
  options: RequestInit = {},
): Promise<T> {
  const response = await fetch(buildUrl(path), {
    ...options,
    headers: {
      Accept: "application/json",
      ...options.headers,
    },
  });

  if (!response.ok) {
    let body: unknown;

    try {
      body = await response.json();
    } catch {
      body = await response.text();
    }

    throw new ApiError(response.status, body);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json() as Promise<T>;
}

export const httpClient = {
  get<T>(path: string, signal?: AbortSignal): Promise<T> {
    return request<T>(path, {
      method: "GET",
      signal,
    });
  },

  post<T>(
    path: string,
    body: unknown,
    signal?: AbortSignal,
  ): Promise<T> {
    return request<T>(path, {
      method: "POST",
      signal,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });
  },

  put<T>(
    path: string,
    body: unknown,
    signal?: AbortSignal,
  ): Promise<T> {
    return request<T>(path, {
      method: "PUT",
      signal,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });
  },

  delete<T>(path: string, signal?: AbortSignal): Promise<T> {
    return request<T>(path, {
      method: "DELETE",
      signal,
    });
  },
};