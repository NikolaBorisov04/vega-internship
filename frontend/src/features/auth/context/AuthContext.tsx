import {
    createContext,
    useCallback,
    useContext,
    useMemo,
    useState,
} from "react";
import type { ReactNode } from "react";
import type { UserRole } from "../../../api/generated/api";
import { getUserRoleFromToken } from "../utils/getUserRoleFromToken";

const ACCESS_TOKEN_KEY = "events_access_token";

interface AuthContextValue {
    token: string | null;
    role: UserRole | null;
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

    const role = useMemo(
        () => getUserRoleFromToken(token),
        [token]
    );

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
            role,
            isAuthenticated: token !== null,
            signIn,
            signOut,
        }),
        [token, role, signIn, signOut]
    );

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}
// custom hook folder
export function useAuth(): AuthContextValue {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth must be used inside AuthProvider"
        );
    }

    return context;
}


//me -> propovi za sve

// UserResponsexyz{
//     sve propove iz user
//     Dictionary<string, object> additionalData;
// }

// //admin specific name ?? throw exception()

// CustomerResponse
// {
//     customer identifiter ...
//     Role: Customer
// }


// const userR = //userresponsexzy
// if role is customer:
//     userR.additonalData.key-value
//     key:label
//     value:xxxxx