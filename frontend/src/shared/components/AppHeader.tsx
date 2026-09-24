import { Music2 } from "lucide-react";
import { useLocation, useNavigate } from "react-router-dom";

import { useAuth } from "../../features/auth/context/AuthContext";
import { ROUTES } from "../../constants/routes";

import "./AppHeader.css";

export function AppHeader() {
    const location = useLocation();
    const navigate = useNavigate();

    const { isAuthenticated, signOut } = useAuth();

    const isAuthRoute =
        location.pathname === ROUTES.LOGIN ||
        location.pathname === ROUTES.REGISTER;

    if (isAuthRoute) {
        return null;
    }

    const handleAuthClick = () => {
        if (isAuthenticated) {
            signOut();
            navigate(ROUTES.EVENTS);
            return;
        }

        navigate(ROUTES.LOGIN);
    };

    return (
        <header className="app-header">
            <div className="app-header__inner">

                {/* BRAND */}
                <button
                    type="button"
                    className="app-header__brand"
                    onClick={() => navigate(ROUTES.EVENTS)}
                >
                    <span className="app-header__brand-icon">
                        <Music2 size={19} />
                    </span>

                    <span className="app-header__brand-name">
                        Events
                    </span>
                </button>

                {/* NAVIGATION */}
                <nav className="app-header__nav">
                    <button
                        type="button"
                        className="app-header__nav-link"
                        onClick={() => navigate(ROUTES.EVENTS)}
                    >
                        Explore
                    </button>
                </nav>

                {/* AUTH */}
                <div className="app-header__actions">
                    <button
                        type="button"
                        className={`app-header__auth-button ${
                            isAuthenticated
                                ? "app-header__auth-button--logout"
                                : ""
                        }`}
                        onClick={handleAuthClick}
                    >
                        {isAuthenticated ? "Log out" : "Log in"}
                    </button>
                </div>
            </div>
        </header>
    );
}