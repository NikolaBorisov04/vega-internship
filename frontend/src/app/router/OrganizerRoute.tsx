import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../features/auth/context/AuthContext";
import { ROUTES } from "../../constants/routes";
import { UserRole } from "../../api/generated/api";

export function OrganizerRoute() {
    const { isAuthenticated, role } = useAuth();

    if (!isAuthenticated) {
        return <Navigate to={ROUTES.LOGIN} replace />;
    }

    if (role !== UserRole.Organizer) {
        return <Navigate to={ROUTES.EVENTS} replace />;
    }

    return <Outlet />;
}