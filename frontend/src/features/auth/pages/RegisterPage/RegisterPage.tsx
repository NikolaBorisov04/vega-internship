import {
    ArrowRight,
    Building2,
    CheckCircle2,
    Music2,
    ShieldCheck,
    Sparkles,
    Ticket,
} from "lucide-react";

import type { LucideIcon } from "lucide-react";

import {
    useEffect,
    useState,
    type CSSProperties,
} from "react";

import {
    Link,
    useNavigate,
} from "react-router-dom";

import type { UserResponseDTO } from "../../../../api/generated/api";
import { UserRole } from "../../../../api/generated/api";

import { useAuth } from "../../context/AuthContext";

import {
    useRegisterStore,
} from "../../store/registerStore";

import type {
    RegistrationRole,
    RegistrationScope,
} from "../../store/registerStore";

import { RegisterForm } from "./components/RegisterForm";
import { RegisterRoleCard } from "./components/RegisterRoleCard";

import { ROUTES } from "../../../../constants/routes";

import "./RegisterPage.css";

interface RoleMeta {
    icon: LucideIcon;

    guestEyebrow: string;
    adminEyebrow: string;

    guestTitle: string;
    adminTitle: string;

    description: string;

    successTitle: string;
}

const ROLE_META: Record<
    RegistrationRole,
    RoleMeta
> = {
    [UserRole.Customer]: {
        icon: Ticket,

        guestEyebrow: "EXPERIENCE HUNTER",
        adminEyebrow: "CREATE CUSTOMER",

        guestTitle:
            "Register as a customer",
        adminTitle:
            "Create customer profile",

        description:
            "Discover events, keep your tickets in one place and follow the experiences you care about.",

        successTitle:
            "Customer profile created.",
    },

    [UserRole.Organizer]: {
        icon: Building2,

        guestEyebrow: "EVENT CREATOR",
        adminEyebrow: "CREATE ORGANIZER",

        guestTitle:
            "Register as an organizer",
        adminTitle:
            "Create organizer profile",

        description:
            "Build events, manage your organization and bring unforgettable experiences to the platform.",

        successTitle:
            "Organizer profile created.",
    },

    [UserRole.Admin]: {
        icon: ShieldCheck,

        guestEyebrow: "PLATFORM ACCESS",
        adminEyebrow: "ADMIN ACCESS",

        guestTitle: "Admin",
        adminTitle:
            "Create admin profile",

        description:
            "Create a profile with full administrative permissions across the Events platform.",

        successTitle:
            "Admin profile created.",
    },
};

function getAllowedRoles(
    isAdmin: boolean
): RegistrationRole[] {
    if (isAdmin) {
        return [
            UserRole.Customer,
            UserRole.Organizer,
            UserRole.Admin,
        ];
    }

    return [
        UserRole.Customer,
        UserRole.Organizer,
    ];
}

export default function RegisterPage() {
    const navigate = useNavigate();

    const {
        isAuthenticated,
        role: currentUserRole,
    } = useAuth();

    const isAdmin =
        isAuthenticated &&
        currentUserRole === UserRole.Admin;

    const expectedScope: RegistrationScope =
        isAdmin
            ? "admin"
            : "guest";

    const selectedRole =
        useRegisterStore(
            (state) =>
                state.selectedRole
        );

    const scope = useRegisterStore(
        (state) => state.scope
    );

    const selectRole = useRegisterStore(
        (state) => state.selectRole
    );

    const reset = useRegisterStore(
        (state) => state.reset
    );

    const [
        successRole,
        setSuccessRole,
    ] = useState<RegistrationRole | null>(
        null
    );

    useEffect(() => {
        if (
            scope !== null &&
            scope !== expectedScope
        ) {
            reset();
        }
    }, [
        scope,
        expectedScope,
        reset,
    ]);

    const activeRole =
        scope === expectedScope
            ? selectedRole
            : null;

    const allowedRoles =
        getAllowedRoles(isAdmin);

    const handleRoleSelect = (
        role: RegistrationRole
    ) => {
        setSuccessRole(null);

        selectRole(
            role,
            expectedScope
        );
    };

    const handleSuccess = (
        role: RegistrationRole,
        _user: UserResponseDTO | null
    ) => {
        reset();
        setSuccessRole(role);
    };

    const handleBackToSelection = () => {
        useRegisterStore
            .getState()
            .selectRole(
                activeRole ?? UserRole.Customer,
                expectedScope
            );

        reset();
    };

    const handleSuccessAction = () => {
        if (isAdmin) {
            navigate(
                ROUTES.EVENTS
            );

            return;
        }

        navigate(
            ROUTES.LOGIN
        );
    };

    const renderSuccess = () => {
        if (!successRole) {
            return null;
        }

        const meta =
            ROLE_META[
                successRole
            ];

        const Icon = meta.icon;

        return (
            <section className="register-success">
                <div className="register-success__burst">
                    {Array.from(
                        {
                            length: 8,
                        },
                        (_, index) => (
                            <span
                                key={index}
                                style={{
                                    "--burst-angle": `${index * 45}deg`,
                                } as CSSProperties}
                            />
                        )
                    )}
                </div>

                <div className="register-success__icon">
                    <Icon size={32} />
                </div>

                <span className="register-success__kicker">
                    PROFILE CREATED
                </span>

                <h2>
                    You're
                    <em> all set.</em>
                </h2>

                <p>
                    {meta.successTitle} Your
                    profile is now part of the
                    Events experience.
                </p>

                <div className="register-success__check">
                    <CheckCircle2
                        size={17}
                    />

                    <span>
                        Everything has been
                        saved successfully.
                    </span>
                </div>

                <button
                    type="button"
                    className="register-submit register-success__button"
                    onClick={
                        handleSuccessAction
                    }
                >
                    <span>
                        {isAdmin
                            ? "Back to events"
                            : "Continue to login"}
                    </span>

                    <ArrowRight
                        size={18}
                    />
                </button>

                {isAdmin && (
                    <button
                        type="button"
                        className="register-success__secondary"
                        onClick={() =>
                            setSuccessRole(
                                null
                            )
                        }
                    >
                        Create another profile
                    </button>
                )}
            </section>
        );
    };

    return (
        <main className="register-page">
            <div className="register-page__glow register-page__glow--one" />
            <div className="register-page__glow register-page__glow--two" />
            <div className="register-page__glow register-page__glow--three" />

            <div className="register-page__particles">
                <span />
                <span />
                <span />
                <span />
                <span />
            </div>

            <div className="register-shell">
                <section className="register-showcase">
                    <div className="register-showcase__top">
                        <Link
                            to={
                                ROUTES.EVENTS
                            }
                            className="register-brand"
                        >
                            <span className="register-brand__icon">
                                <Music2
                                    size={19}
                                />
                            </span>

                            <span>
                                Events
                            </span>
                        </Link>

                    </div>

                    <div className="register-showcase__content">
                        <span className="register-eyebrow">
                            {successRole
                                ? "MISSION COMPLETE"
                                : activeRole
                                ? "PROFILE SETUP"
                                : "YOUR NEXT MOVE"}
                        </span>

                        <h1>
                            {successRole
                                ? "You're"
                                : activeRole
                                ? "Make your"
                                : "Make your"}
                            <span>
                                {successRole
                                    ? " all set."
                                    : activeRole
                                    ? " mark."
                                    : " way in."}
                            </span>
                        </h1>

                        <p>
                            {successRole
                                ? "Your new profile is ready. The next experience is waiting."
                                : activeRole
                                ? "Create a profile that matches the way you want to experience Events."
                                : "Choose your path. Whether you're here to discover, create or run the show, there's a place for you."}
                        </p>

                        <div className="register-console">
                            <div className="register-console__ring register-console__ring--outer" />
                            <div className="register-console__ring register-console__ring--inner" />

                            <div className="register-console__orbit register-console__orbit--one">
                                <span>
                                    ✦
                                </span>
                            </div>

                            <div className="register-console__orbit register-console__orbit--two">
                                <span>
                                    +
                                </span>
                            </div>

                            <div className="register-console__core">
                                <Music2
                                    size={34}
                                />
                            </div>
                        </div>
                    </div>

                    <div className="register-showcase__footer">
                        <span>
                            FIND YOUR MOMENT
                        </span>

                        <span className="register-showcase__line" />

                        <span>
                            EVENTS
                        </span>
                    </div>
                </section>

                <section className="register-panel">
                    <div className="register-card">
                        {!activeRole &&
                            !successRole && (
                                <div className="register-selection">
                                    <div className="register-card__mobile-brand">
                                        <span className="register-brand__icon">
                                            <Music2
                                                size={
                                                    18
                                                }
                                            />
                                        </span>

                                        <span>
                                            Events
                                        </span>
                                    </div>

                                    <div className="register-selection__header">
                                        <span className="register-card__kicker">
                                            {isAdmin
                                                ? "ADMIN MODE"
                                                : "NEW TO EVENTS?"}
                                        </span>

                                        <h2>
                                            Choose your
                                            <em>
                                                {" "}
                                                role.
                                            </em>
                                        </h2>

                                        <p>
                                            {isAdmin
                                                ? "Create a profile for yourself or on behalf of another user."
                                                : "Pick the experience that fits the way you want to use Events."}
                                        </p>
                                    </div>

                                    <div
                                        className={`register-role-grid register-role-grid--${allowedRoles.length}`}
                                    >
                                        {allowedRoles.map(
                                            (
                                                role
                                            ) => {
                                                const meta =
                                                    ROLE_META[
                                                        role
                                                    ];

                                                const Icon =
                                                    meta.icon;

                                                return (
                                                    <RegisterRoleCard
                                                        key={
                                                            role
                                                        }
                                                        role={
                                                            role
                                                        }
                                                        Icon={
                                                            Icon
                                                        }
                                                        eyebrow={
                                                            isAdmin
                                                                ? meta.adminEyebrow
                                                                : meta.guestEyebrow
                                                        }
                                                        title={
                                                            isAdmin
                                                                ? meta.adminTitle
                                                                : meta.guestTitle
                                                        }
                                                        description={
                                                            meta.description
                                                        }
                                                        onSelect={() =>
                                                            handleRoleSelect(
                                                                role
                                                            )
                                                        }
                                                    />
                                                );
                                            }
                                        )}
                                    </div>

                                    <div className="register-selection__tip">
                                        <Sparkles
                                            size={
                                                16
                                            }
                                        />

                                        <span>
                                            Your draft
                                            is saved
                                            automatically
                                            as you go.
                                        </span>
                                    </div>
                                </div>
                            )}

                        {activeRole &&
                            !successRole && (
                                <RegisterForm
                                    role={
                                        activeRole
                                    }
                                    onBack={
                                        handleBackToSelection
                                    }
                                    onSuccess={
                                        handleSuccess
                                    }
                                />
                            )}

                        {successRole &&
                            renderSuccess()}
                    </div>
                </section>
            </div>
        </main>
    );
}