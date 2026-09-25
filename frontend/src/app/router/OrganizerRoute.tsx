import { Navigate, Outlet } from "react-router-dom";

import { UserRole } from "../../api/generated/api";
import { useAuth } from "../../features/auth/context/AuthContext";
import { ROUTES } from "../../constants/routes";

export function OrganizerRoute() {
    const { isAuthenticated, role } = useAuth();

    if (!isAuthenticated) {
        return (
            <Navigate
                to={ROUTES.LOGIN}
                replace
            />
        );
    }

    if (
        role !== UserRole.Organizer &&
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