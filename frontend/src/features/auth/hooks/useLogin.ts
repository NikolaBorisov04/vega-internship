import { useState } from "react";

import type {
    LoginDTO,
    UserResponseDTO,
} from "../../../api/generated/api";

import { apiClient } from "../../../api/client";

import {
    getApiErrorMessage,
} from "../utils/getApiErrorMessage";

interface UseLoginResult {
    login: (
        credentials: LoginDTO
    ) => Promise<UserResponseDTO | null>;

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
    ): Promise<UserResponseDTO | null> => {
        setIsLoading(true);
        setError(null);

        try {
            const response =
                await apiClient.login(credentials);

            return response ?? null;
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