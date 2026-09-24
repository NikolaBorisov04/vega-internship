import {
    Navigate,
    Outlet,
} from "react-router-dom";

import { UserRole } from "../../api/generated/api";

import { useAuth } from "../../features/auth/context/AuthContext";

import { ROUTES } from "../../constants/routes";

export function RegisterRoute() {
    const {
        isAuthenticated,
        role,
    } = useAuth();
    
    if (
        isAuthenticated &&
        role !== UserRole.Admin
    ) {
        return (
            <Navigate
                to={ROUTES.EVENTS}
                replace
            />
        );
    }

    return <Outlet />;
}