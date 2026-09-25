import {
    ArrowRight,
    Eye,
    EyeOff,
    LockKeyhole,
    Mail,
    Music2,
} from "lucide-react";
import { Link, useNavigate } from "react-router-dom";

import { useLogin } from "../../hooks/useLogin";

import "./LoginPage.css";
import { useAuth } from "../../context/AuthContext";
import { useState, type SubmitEvent } from "react";
import type { LoginDTO } from "../../../../api/generated/api";
import { ROUTES } from "../../../../constants/routes";

export default function LoginPage() {
    const navigate = useNavigate();

    const { signIn } = useAuth();

    const {
        login,
        isLoading,
        error,
        clearError,
    } = useLogin();

    const [formData, setFormData] = useState<LoginDTO>({
        email: "",
        password: "",
    });

    const [showPassword, setShowPassword] = useState(false);
    const [formError, setFormError] = useState<string | null>(null);

    const handleChange = (
        field: keyof LoginDTO,
        value: string
    ) => {
        setFormData((previous) => ({
            ...previous,
            [field]: value,
        }));

        setFormError(null);
        clearError();
    };

    const handleSubmit = async (
        event: SubmitEvent
    ) => {
        event.preventDefault();

        const email = formData.email?.trim() ?? "";
        const password = formData.password ?? "";

        if (!email || !password) {
            setFormError(
                "Unesite e-mail i lozinku."
            );
            return;
        }

        const token = await login({
            email,
            password,
        });

        if (!token) {
            return;
        }

        signIn(token);

        navigate("/");
    };

    return (
        <main className="login-page">
            <div className="login-page__glow login-page__glow--one" />
            <div className="login-page__glow login-page__glow--two" />

            <div className="login-shell">

                {/* LEFT SIDE */}
                <section className="login-showcase">
                    <div className="login-showcase__top">
                        <Link
                            to="/"
                            className="login-brand"
                        >
                            <span className="login-brand__icon">
                                <Music2 size={19} />
                            </span>

                            <span>Events</span>
                        </Link>
                    </div>

                    <div className="login-showcase__content">

                        <span className="login-eyebrow">
                            YOUR NEXT EXPERIENCE
                        </span>

                        <h1>
                            Where the
                            <span> night begins.</span>
                        </h1>

                        <p>
                            Sign in to discover events,
                            manage your tickets and keep
                            every unforgettable night
                            in one place.
                        </p>

                        <div className="login-waveform">
                            {[
                                28,
                                45,
                                65,
                                38,
                                78,
                                52,
                                92,
                                44,
                                68,
                                34,
                                57,
                                82,
                                49,
                                72,
                                39,
                                61,
                            ].map((height, index) => (
                                <span
                                    key={index}
                                    style={{
                                        height: `${height}px`,
                                    }}
                                />
                            ))}
                        </div>

                        <div className="login-showcase__tags">
                            <span>LIVE MUSIC</span>
                            <span>FESTIVALS</span>
                            <span>CLUB NIGHTS</span>
                            <span>PERFORMANCES</span>
                        </div>
                    </div>

                    <div className="login-showcase__footer">
                        <span>
                            FIND YOUR MOMENT
                        </span>

                        <span className="login-showcase__line" />

                        <span>
                            EVENTS
                        </span>
                    </div>
                </section>

                {/* LOGIN CARD */}
                <section className="login-panel">
                    <div className="login-card">

                        <div className="login-card__header">
                            <div className="login-card__mobile-logo">
                                <span className="login-brand__icon">
                                    <Music2 size={18} />
                                </span>

                                <span>Events</span>
                            </div>

                            <span className="login-card__kicker">
                                WELCOME BACK
                            </span>

                            <h2>
                                Sign in to Events
                            </h2>

                            <p>
                                Enter your details to
                                continue your experience.
                            </p>
                        </div>

                        {(error || formError) && (
                            <div className="login-error">
                                {error || formError}
                            </div>
                        )}

                        <form
                            className="login-form"
                            onSubmit={handleSubmit}
                        >
                            {/* EMAIL */}
                            <div className="login-field">
                                <label htmlFor="email">
                                    E-mail
                                </label>

                                <div className="login-input-wrapper">
                                    <Mail
                                        size={19}
                                        className="login-input-icon"
                                    />

                                    <input
                                        id="email"
                                        type="email"
                                        value={formData.email ?? ""}
                                        onChange={(event) =>
                                            handleChange(
                                                "email",
                                                event.target.value
                                            )
                                        }
                                        placeholder="you@example.com"
                                        autoComplete="email"
                                        disabled={isLoading}
                                        required
                                    />
                                </div>
                            </div>

                            {/* PASSWORD */}
                            <div className="login-field">
                                <div className="login-field__label-row">
                                    <label htmlFor="password">
                                        Password
                                    </label>
                                </div>

                                <div className="login-input-wrapper">
                                    <LockKeyhole
                                        size={19}
                                        className="login-input-icon"
                                    />

                                    <input
                                        id="password"
                                        type={
                                            showPassword
                                                ? "text"
                                                : "password"
                                        }
                                        value={formData.password ?? ""}
                                        onChange={(event) =>
                                            handleChange(
                                                "password",
                                                event.target.value
                                            )
                                        }
                                        placeholder="Enter your password"
                                        autoComplete="current-password"
                                        disabled={isLoading}
                                    />

                                    <button
                                        type="button"
                                        className="password-toggle"
                                        onClick={() =>
                                            setShowPassword(
                                                (previous) => !previous
                                            )
                                        }
                                        aria-label={
                                            showPassword
                                                ? "Hide password"
                                                : "Show password"
                                        }
                                    >
                                        {showPassword ? (
                                            <EyeOff size={19} />
                                        ) : (
                                            <Eye size={19} />
                                        )}
                                    </button>
                                </div>
                            </div>

                            <button
                                type="submit"
                                className="login-submit"
                                disabled={isLoading}
                            >
                                {isLoading ? (
                                    <span className="login-spinner" />
                                ) : (
                                    <>
                                        <span>
                                            Enter Events
                                        </span>

                                        <ArrowRight size={19} />
                                    </>
                                )}
                            </button>
                        </form>

                        <div className="login-register">
                            <span>
                                Don't have an account?
                            </span>

                            <Link to={ROUTES.REGISTER}>
                                Create one
                            </Link>
                        </div>
                    </div>
                </section>
            </div>
        </main>
    );
}