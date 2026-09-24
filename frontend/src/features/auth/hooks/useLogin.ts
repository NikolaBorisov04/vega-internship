import { useState } from "react";

import type {
    LoginDTO,
} from "../../../api/generated/api";

import { apiClient } from "../../../api/client";

import {
    getApiErrorMessage,
} from "../utils/getApiErrorMessage";

interface UseLoginResult {
    login: (
        credentials: LoginDTO
    ) => Promise<string | null>;

    isLoading: boolean;
    error: string | null;

    clearError: () => void;
}

export function useLogin(): UseLoginResult {
    const [
        isLoading,
        setIsLoading,
    ] = useState(false);

    const [
        error,
        setError,
    ] = useState<string | null>(null);

    const clearError = () => {
        setError(null);
    };

    const login = async (
        credentials: LoginDTO
    ): Promise<string | null> => {
        setIsLoading(true);
        setError(null);

        try {
            const response =
                await apiClient.login(
                    credentials
                );

            if (!response) {
                return null;
            }

            const token = (
                response as unknown as {
                    token: string;
                }
            ).token;

            if (!token) {
                return null;
            }

            return token;
        } catch (error: unknown) {
            setError(
                getApiErrorMessage(error)
            );

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