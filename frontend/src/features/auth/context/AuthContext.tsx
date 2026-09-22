import {
    createContext,
    useCallback,
    useContext,
    useMemo,
    useState,
} from "react";
import type { ReactNode } from "react";

const ACCESS_TOKEN_KEY = "events_access_token";

interface AuthContextValue {
    token: string | null;
    isAuthenticated: boolean;
    signIn: (token: string) => void;
    signOut: () => void;
}

const AuthContext = createContext<AuthContextValue | undefined>(
    undefined
);

interface AuthProviderProps {
    children: ReactNode;
}

export function AuthProvider({
    children,
}: AuthProviderProps) {
    const [token, setToken] = useState<string | null>(() => {
        return localStorage.getItem(ACCESS_TOKEN_KEY);
    });

    const signIn = useCallback((newToken: string) => {
        localStorage.setItem(
            ACCESS_TOKEN_KEY,
            newToken
        );

        setToken(newToken);
    }, []);

    const signOut = useCallback(() => {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        setToken(null);
    }, []);

    const value = useMemo(
        () => ({
            token,
            isAuthenticated: token !== null,
            signIn,
            signOut,
        }),
        [token, signIn, signOut]
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