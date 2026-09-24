import {
    ArrowLeft,
    ArrowRight,
    BriefcaseBusiness,
    Check,
    Globe2,
    Mail,
    MapPin,
    Phone,
    UserRound,
} from "lucide-react";

import {
    useState,
    type SubmitEvent,
} from "react";


import { RegisterInput } from "./RegisterInput";
import { useRegisterStore, type RegisterDraftFields, type RegisterStep, type RegistrationRole } from "../../../store/registerStore";
import { UserRole, type UserResponseDTO } from "../../../../../api/generated/api";
import { useRegister } from "../../../hooks/useRegister";
import { validateRegisterStep, type RegisterErrors } from "../../../utils/registerValidation";

interface RegisterFormProps {
    role: RegistrationRole;

    onBack: () => void;

    onSuccess: (
        role: RegistrationRole,
        user: UserResponseDTO | null
    ) => void;
}

interface StepDefinition {
    id: RegisterStep;
    title: string;
    caption: string;
}

function getRoleTitle(
    role: RegistrationRole
): string {
    switch (role) {
        case UserRole.Customer:
            return "Customer profile";

        case UserRole.Organizer:
            return "Organizer profile";

        case UserRole.Admin:
            return "Admin profile";
    }
}

function getSubmitLabel(
    role: RegistrationRole
): string {
    switch (role) {
        case UserRole.Customer:
            return "Create customer profile";

        case UserRole.Organizer:
            return "Create organizer profile";

        case UserRole.Admin:
            return "Create admin profile";
    }
}

function getPasswordStrength(
    password: string
) {
    let score = 0;

    if (password.length >= 8) {
        score += 1;
    }

    if (/[A-Z]/.test(password)) {
        score += 1;
    }

    if (/[0-9]/.test(password)) {
        score += 1;
    }

    if (/[^A-Za-z0-9]/.test(password)) {
        score += 1;
    }

    const labels = [
        "Start with 8+ characters",
        "Needs a little more",
        "Getting stronger",
        "Nice and strong",
        "Excellent password",
    ];

    return {
        score,
        label: labels[score],
    };
}

export function RegisterForm({
    role,
    onBack,
    onSuccess,
}: RegisterFormProps) {
    const name = useRegisterStore(
        (state) => state.name
    );

    const email = useRegisterStore(
        (state) => state.email
    );

    const password = useRegisterStore(
        (state) => state.password
    );

    const country = useRegisterStore(
        (state) => state.country
    );

    const city = useRegisterStore(
        (state) => state.city
    );

    const address = useRegisterStore(
        (state) => state.address
    );

    const phoneNumber = useRegisterStore(
        (state) => state.phoneNumber
    );

    const companyName = useRegisterStore(
        (state) => state.companyName
    );

    const currentStep = useRegisterStore(
        (state) => state.currentStep
    );

    const setField = useRegisterStore(
        (state) => state.setField
    );

    const nextStep = useRegisterStore(
        (state) => state.nextStep
    );

    const previousStep =
        useRegisterStore(
            (state) =>
                state.previousStep
        );

    const {
        register,
        isLoading,
        error,
        clearError,
    } = useRegister();

    const [
        fieldErrors,
        setFieldErrors,
    ] = useState<RegisterErrors>({});

    const isCustomer =
        role === UserRole.Customer;

    const totalSteps = isCustomer
        ? 2
        : 3;

    const steps: StepDefinition[] =
        isCustomer
            ? [
                  {
                      id: 1,
                      title: "Identity",
                      caption:
                          "Introduce yourself",
                  },
                  {
                      id: 2,
                      title: "Details",
                      caption:
                          "Set your location",
                  },
              ]
            : [
                  {
                      id: 1,
                      title: "Identity",
                      caption:
                          "Introduce yourself",
                  },
                  {
                      id: 2,
                      title: "Details",
                      caption:
                          "Set your location",
                  },
                  {
                      id: 3,
                      title: "Organization",
                      caption:
                          "Tell us who you represent",
                  },
              ];

    const draft: RegisterDraftFields = {
        name,
        email,
        password,
        country,
        city,
        address,
        phoneNumber,
        companyName,
    };

    const passwordStrength =
        getPasswordStrength(
            password
        );

    const handleChange = <
        K extends keyof RegisterDraftFields
    >(
        field: K,
        value: RegisterDraftFields[K]
    ) => {
        setField(field, value);

        clearError();

        setFieldErrors(
            (previous) => {
                if (!previous[field]) {
                    return previous;
                }

                const next = {
                    ...previous,
                };

                delete next[field];

                return next;
            }
        );
    };

    const handleSubmit = async (
        event: SubmitEvent
    ) => {
        event.preventDefault();

        const errors =
            validateRegisterStep(
                currentStep,
                draft,
                role
            );

        if (
            Object.keys(errors)
                .length > 0
        ) {
            setFieldErrors(errors);
            return;
        }

        setFieldErrors({});

        if (
            currentStep < totalSteps
        ) {
            nextStep(totalSteps);
            return;
        }

        clearError();

        const user = await register({
            role,
            data: draft,
        });

        if (!user) {
            return;
        }

        onSuccess(role, user);
    };

    const handleBack = () => {
        clearError();
        setFieldErrors({});

        if (currentStep === 1) {
            onBack();
            return;
        }

        previousStep();
    };

    return (
        <div className="register-form-shell">
            <div className="register-form-header">
                <button
                    type="button"
                    className="register-back-button"
                    onClick={handleBack}
                    disabled={isLoading}
                >
                    <ArrowLeft size={16} />
                    <span>
                        {currentStep === 1
                            ? "Change profile"
                            : "Back"}
                    </span>
                </button>

                <span className="register-form-role">
                    {getRoleTitle(role)}
                </span>
            </div>

            <div className="register-progress">
                {steps.map((step) => {
                    const isComplete =
                        currentStep >
                        step.id;

                    const isActive =
                        currentStep ===
                        step.id;

                    return (
                        <div
                            key={step.id}
                            className={`register-progress__item ${
                                isActive
                                    ? "register-progress__item--active"
                                    : ""
                            } ${
                                isComplete
                                    ? "register-progress__item--complete"
                                    : ""
                            }`}
                        >
                            <span className="register-progress__dot">
                                {isComplete ? (
                                    <Check
                                        size={12}
                                    />
                                ) : (
                                    step.id
                                )}
                            </span>

                            <span className="register-progress__copy">
                                <strong>
                                    {step.title}
                                </strong>

                                <small>
                                    {
                                        step.caption
                                    }
                                </small>
                            </span>
                        </div>
                    );
                })}
            </div>

            <div className="register-step-counter">
                STEP {currentStep} /{" "}
                {totalSteps}
            </div>

            {(error || Object.keys(fieldErrors).length > 0) && (
                error && (
                    <div
                        className="register-server-error"
                        role="alert"
                    >
                        {error}
                    </div>
                )
            )}

            <form
                className="register-form"
                onSubmit={handleSubmit}
            >
                {currentStep === 1 && (
                    <div className="register-form-step register-form-step--enter">
                        <div className="register-form-intro">
                            <span>
                                LET'S START WITH YOU
                            </span>

                            <h2>
                                Build your
                                <em> identity.</em>
                            </h2>

                            <p>
                                These details are the
                                foundation of your Events
                                profile.
                            </p>
                        </div>

                        <div className="register-form-grid">
                            <RegisterInput
                                id="register-name"
                                label="Full name"
                                icon={
                                    <UserRound
                                        size={18}
                                    />
                                }
                                value={name}
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "name",
                                        value
                                    )
                                }
                                placeholder="Your full name"
                                autoComplete="name"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.name
                                }
                            />

                            <RegisterInput
                                id="register-email"
                                label="E-mail"
                                icon={
                                    <Mail
                                        size={18}
                                    />
                                }
                                type="email"
                                value={email}
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "email",
                                        value
                                    )
                                }
                                placeholder="you@example.com"
                                autoComplete="email"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.email
                                }
                            />

                            <div className="register-password-field">
                                <RegisterInput
                                    id="register-password"
                                    label="Password"
                                    icon={
                                        <span className="register-lock-icon">
                                            •
                                        </span>
                                    }
                                    type="password"
                                    value={
                                        password
                                    }
                                    onValueChange={(
                                        value
                                    ) =>
                                        handleChange(
                                            "password",
                                            value
                                        )
                                    }
                                    placeholder="Create a strong password"
                                    autoComplete="new-password"
                                    disabled={
                                        isLoading
                                    }
                                    error={
                                        fieldErrors.password
                                    }
                                />

                                <div className="register-password-strength">
                                    <div className="register-password-strength__bars">
                                        {[1, 2, 3, 4].map(
                                            (
                                                bar
                                            ) => (
                                                <span
                                                    key={
                                                        bar
                                                    }
                                                    className={
                                                        bar <=
                                                        passwordStrength.score
                                                            ? "register-password-strength__bar register-password-strength__bar--active"
                                                            : "register-password-strength__bar"
                                                    }
                                                />
                                            )
                                        )}
                                    </div>

                                    <span>
                                        {
                                            passwordStrength.label
                                        }
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                )}

                {currentStep === 2 && (
                    <div className="register-form-step register-form-step--enter">
                        <div className="register-form-intro">
                            <span>
                                YOUR HOME BASE
                            </span>

                            <h2>
                                Tell us
                                <em> where.</em>
                            </h2>

                            <p>
                                Give your profile a
                                little more context.
                            </p>
                        </div>

                        <div className="register-form-grid">
                            <RegisterInput
                                id="register-country"
                                label="Country"
                                icon={
                                    <Globe2
                                        size={18}
                                    />
                                }
                                value={country}
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "country",
                                        value
                                    )
                                }
                                placeholder="Country Name"
                                autoComplete="country-name"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.country
                                }
                            />

                            <RegisterInput
                                id="register-city"
                                label="City"
                                icon={
                                    <MapPin
                                        size={18}
                                    />
                                }
                                value={city}
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "city",
                                        value
                                    )
                                }
                                placeholder="City Name"
                                autoComplete="address-level2"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.city
                                }
                            />

                            <RegisterInput
                                id="register-address"
                                label="Address"
                                icon={
                                    <MapPin
                                        size={18}
                                    />
                                }
                                value={address}
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "address",
                                        value
                                    )
                                }
                                placeholder="Street and number"
                                autoComplete="street-address"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.address
                                }
                            />

                            <RegisterInput
                                id="register-phone"
                                label="Phone number"
                                icon={
                                    <Phone
                                        size={18}
                                    />
                                }
                                value={
                                    phoneNumber
                                }
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "phoneNumber",
                                        value
                                    )
                                }
                                placeholder="+381 64 123 4567"
                                autoComplete="tel"
                                inputMode="tel"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.phoneNumber
                                }
                            />
                        </div>
                    </div>
                )}

                {currentStep === 3 &&
                    !isCustomer && (
                        <div className="register-form-step register-form-step--enter">
                            <div className="register-form-intro">
                                <span>
                                    THE BIG PICTURE
                                </span>

                                <h2>
                                    Bring your
                                    <em> world.</em>
                                </h2>

                                <p>
                                    This profile represents
                                    an organization inside
                                    the Events platform.
                                </p>
                            </div>

                            <RegisterInput
                                id="register-company"
                                label="Company / organization"
                                icon={
                                    <BriefcaseBusiness
                                        size={18}
                                    />
                                }
                                value={
                                    companyName
                                }
                                onValueChange={(
                                    value
                                ) =>
                                    handleChange(
                                        "companyName",
                                        value
                                    )
                                }
                                placeholder="Your company or organization"
                                autoComplete="organization"
                                disabled={
                                    isLoading
                                }
                                error={
                                    fieldErrors.companyName
                                }
                            />

                            <div className="register-ready-panel">
                                <div className="register-ready-panel__icon">
                                    <Check
                                        size={18}
                                    />
                                </div>

                                <div>
                                    <strong>
                                        Profile almost
                                        ready
                                    </strong>

                                    <p>
                                        Everything looks
                                        good. Create the
                                        profile and step
                                        into Events.
                                    </p>
                                </div>
                            </div>
                        </div>
                    )}

                <div className="register-form-footer">
                    <span className="register-form-footer__hint">
                        {currentStep === 1
                            ? "You can change these details later."
                            : currentStep === 2
                            ? "Almost there."
                            : "One last step."}
                    </span>

                    <button
                        type="submit"
                        className="register-submit"
                        disabled={isLoading}
                    >
                        {isLoading ? (
                            <>
                                <span className="register-spinner" />

                                <span>
                                    Creating...
                                </span>
                            </>
                        ) : (
                            <>
                                <span>
                                    {currentStep <
                                    totalSteps
                                        ? "Continue"
                                        : getSubmitLabel(
                                              role
                                          )}
                                </span>

                                <ArrowRight
                                    size={18}
                                />
                            </>
                        )}
                    </button>
                </div>
            </form>
        </div>
    );
}