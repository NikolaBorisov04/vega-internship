import { useState } from "react";
import type {
    LoginDTO,
    ProblemDetails,
} from "../../../api/generated/api";
import { apiClient } from "../../../api/client";

interface UseLoginResult {
    login: (credentials: LoginDTO) => Promise<string | null>;
    isLoading: boolean;
    error: string | null;
    clearError: () => void;
}

export function useLogin(): UseLoginResult {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const clearError = () => {
        setError(null);
    };

    const login = async (
        credentials: LoginDTO
    ): Promise<string | null> => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await apiClient.login(credentials);

            if (!response) {
                return null;
            }

// I had to map this to the string for some reason the backend returns an object that has a key pair value token: "thetokenitself"
            const token = (response as unknown as { token: string }).token;

            if (!token) {
                return null;
            }

            return token;
        } catch (err: unknown) {
            const problem = err as ProblemDetails & {
                result?: ProblemDetails;
            };

            const message =
                problem.detail ??
                problem.result?.detail ??
                null;

            setError(message);

            return null;
        } finally {
            setIsLoading(false);
        }
    };

    return {
        login,
        isLoading,
        error,
        clearError,
    };
}