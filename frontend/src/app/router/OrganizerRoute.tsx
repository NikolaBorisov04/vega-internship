import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../features/auth/context/AuthContext";

export function OrganizerRoute() {
    const { isAuthenticated, role } = useAuth();

    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    if (role !== "Organizer") {
        return <Navigate to="/events" replace />;
    }

    return <Outlet />;
}