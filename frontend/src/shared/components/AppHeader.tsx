import { Music2 } from "lucide-react";
import { useLocation, useNavigate } from "react-router-dom";

import { useAuth } from "../../features/auth/context/AuthContext";

import "./AppHeader.css";

export function AppHeader() {
    const location = useLocation();
    const navigate = useNavigate();

    const { isAuthenticated, signOut } = useAuth();

    const isAuthRoute =
        location.pathname === "/login" ||
        location.pathname === "/register";

    if (isAuthRoute) {
        return null;
    }

    const handleAuthClick = () => {
        if (isAuthenticated) {
            signOut();
            navigate("/events");
            return;
        }

        navigate("/login");
    };

    return (
        <header className="app-header">
            <div className="app-header__inner">

                {/* BRAND */}
                <button
                    type="button"
                    className="app-header__brand"
                    onClick={() => navigate("/events")}
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
                        onClick={() => navigate("/events")}
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
                        {isAuthenticated
                            ? "Log out"
                            : "Log in"}
                    </button>
                </div>
            </div>
        </header>
    );
}