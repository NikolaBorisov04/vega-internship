import { Client } from "./generated/api";

const API_URL = import.meta.env.VITE_API_URL;

if (!API_URL) {
    throw new Error("VITE_API_URL is not defined.");
}

const API_BASE_URL = API_URL.replace(/\/api\/?$/, "");

const http = {
    fetch: (
        input: RequestInfo | URL,
        init?: RequestInit
    ) => {
        const requestUrl =
            input instanceof Request
                ? input.url
                : input.toString();

        const url = new URL(
            requestUrl,
            API_BASE_URL
        );

        return window.fetch(
            url.toString(),
            {
                ...init,
                credentials: "include",
            }
        );
    },
};

export const apiClient = new Client(
    API_BASE_URL,
    http
);