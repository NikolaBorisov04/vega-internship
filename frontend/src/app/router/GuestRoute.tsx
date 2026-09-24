import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../features/auth/context/AuthContext";
import { ROUTES } from "../../constants/routes";

export function GuestRoute() {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to={ROUTES.EVENTS} replace />;
  }

  return <Outlet />;
}