import { Client } from "./generated/api";

const API_URL = import.meta.env.VITE_API_URL;

if (!API_URL) {
    throw new Error("VITE_API_URL is not defined.");
}

const API_BASE_URL = API_URL.replace(/\/api\/?$/, "");

const http = {
    fetch: (input: RequestInfo | URL, init?: RequestInit) => {
        const token = localStorage.getItem(
            "events_access_token"
        );

        const headers = new Headers(init?.headers);

        const requestUrl =
            input instanceof Request
                ? input.url
                : input.toString();

        const url = new URL(
            requestUrl,
            API_BASE_URL
        );

        const isLoginRequest =
            url.pathname === "/api/Auth/login";

        if (token && !isLoginRequest) {
            headers.set(
                "Authorization",
                `Bearer ${token}`
            );
        }

        return window.fetch(url.toString(), {
            ...init,
            headers,
        });
    },
};

export const apiClient = new Client(
    API_BASE_URL,
    http
);