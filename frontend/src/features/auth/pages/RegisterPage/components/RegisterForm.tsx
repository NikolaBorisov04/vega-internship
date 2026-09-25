import {
    ArrowLeft,
    ArrowRight,
    Check,
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
import { REGISTER_ROLE_CONFIG } from "../constants/registerRoles";
import { REGISTER_ORGANIZATION_STEP, REGISTER_STEPS } from "../constants/registerSteps";
import { getPasswordStrength } from "../utils/getPasswordStrenght";
import { RegisterOrganizationStep } from "./RegisterOrganizationStep";
import { RegisterIdentityStep } from "./RegisterIdentityStep";
import { RegisterLocationStep } from "./RegisterLocationStep";

interface RegisterFormProps {
    role: RegistrationRole;

    onBack: () => void;

    onSuccess: (
        role: RegistrationRole,
        user: UserResponseDTO | null
    ) => void;
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

    const steps = isCustomer
    ? REGISTER_STEPS
    : [...REGISTER_STEPS, REGISTER_ORGANIZATION_STEP];

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
                    {REGISTER_ROLE_CONFIG[role].title}
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

                {error && (
                    <div
                        className="register-server-error"
                        role="alert"
                    >
                        {error}
                    </div>
                )}
                
            <form
                className="register-form"
                onSubmit={handleSubmit}
            >
                {currentStep === 1 && (
                    <RegisterIdentityStep
                        draft={draft}
                        errors={fieldErrors}
                        isLoading={isLoading}
                        onChange={handleChange}
                    />
                )}

                {currentStep === 2 && (
                    <RegisterLocationStep
                        draft={draft}
                        errors={fieldErrors}
                        isLoading={isLoading}
                        onChange={handleChange}
                    />
                )}

                {currentStep === 3 && !isCustomer && (
                    <RegisterOrganizationStep
                        draft={draft}
                        errors={fieldErrors}
                        isLoading={isLoading}
                        onChange={handleChange}
                    />
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
                                        : REGISTER_ROLE_CONFIG[role].submitLabel}
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