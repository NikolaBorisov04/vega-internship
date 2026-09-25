import { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";

import type { ReactNode } from "react";

import type { UserResponseDTO, UserRole } from "../../../api/generated/api";

import { apiClient } from "../../../api/client";

interface AuthContextValue {
    user: UserResponseDTO | null;
    role: UserRole | null;
    isAuthenticated: boolean;
    isLoading: boolean;

    signIn: (user: UserResponseDTO) => void;
    signOut: () => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

interface AuthProviderProps {
    children: ReactNode;
}

export function AuthProvider({
    children,
}: AuthProviderProps) {
    const [
        user,
        setUser,
    ] = useState<UserResponseDTO | null>(null);

    const [
        isLoading,
        setIsLoading,
    ] = useState(true);

    const loadCurrentUser = useCallback(
        async () => {
            try {
                const currentUser =
                    await apiClient.me();

                setUser(currentUser ?? null);
            } catch {
                setUser(null);
            } finally {
                setIsLoading(false);
            }
        },
        []
    );

    useEffect(() => {
        void loadCurrentUser();
    }, [loadCurrentUser]);

    const role = useMemo(
        () => user?.role ?? null,
        [user]
    );

    const signIn = useCallback(
        (newUser: UserResponseDTO) => {
            setUser(newUser);
        },
        []
    );

    const signOut = useCallback(async () =>
    {
        try
        {
            await apiClient.logout();
        }
        catch
        {}
        finally
        {
            setUser(null);
        }
    }, []);

    const value = useMemo(
        () => ({
            user,
            role,
            isAuthenticated: user !== null,
            isLoading,
            signIn,
            signOut,
        }),
        [
            user,
            role,
            isLoading,
            signIn,
            signOut,
        ]
    );

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth(): AuthContextValue {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth must be used inside AuthProvider"
        );
    }
    return context;
}