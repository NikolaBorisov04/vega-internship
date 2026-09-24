import { getPasswordStrength } from "../utils/getPasswordStrenght";
import { Mail, UserRound } from "lucide-react";
import { RegisterInput } from "./RegisterInput";
import type { RegisterDraftFields } from "../../../store/registerStore";
import type { RegisterErrors } from "../../../utils/registerValidation";

interface RegisterIdentityStepProps {
    draft: RegisterDraftFields;
    errors: RegisterErrors;
    isLoading: boolean;
    onChange: <
        K extends keyof RegisterDraftFields
    >(
        field: K,
        value: RegisterDraftFields[K]
    ) => void;
}

export function RegisterIdentityStep({
    draft,
    isLoading,
    errors,
    onChange,
}: RegisterIdentityStepProps) {
    const passwordStrength =
        getPasswordStrength(draft.password);

    return (
        <div className="register-form-step register-form-step--enter">
            <div className="register-form-intro">
                <span>LET'S START WITH YOU</span>

                <h2>
                    Build your
                    <em> identity.</em>
                </h2>

                <p>
                    These details are the foundation of your Events profile.
                </p>
            </div>

            <div className="register-form-grid">
                <RegisterInput
                    id="register-name"
                    label="Full name"
                    icon={<UserRound size={18} />}
                    value={draft.name}
                    onValueChange={(value) => onChange("name", value)}
                    placeholder="Your full name"
                    autoComplete="name"
                    disabled={isLoading}
                    error={errors.name}
                />

                <RegisterInput
                    id="register-email"
                    label="E-mail"
                    icon={<Mail size={18} />}
                    type="email"
                    value={draft.email}
                    onValueChange={(value) => onChange("email", value)}
                    placeholder="you@example.com"
                    autoComplete="email"
                    disabled={isLoading}
                    error={errors.email}
                />

                <div className="register-password-field">
                    <RegisterInput
                        id="register-password"
                        label="Password"
                        icon={
                            <span className="register-lock-icon">•</span>
                        }
                        type="password"
                        value={draft.password}
                        onValueChange={(value) => onChange("password", value)}
                        placeholder="Create a strong password"
                        autoComplete="new-password"
                        disabled={isLoading}
                        error={errors.password}
                    />

                    <div className="register-password-strength">
                        <div className="register-password-strength__bars">
                            {[1, 2, 3, 4].map((bar) => (
                                <span
                                    key={bar}
                                    className={
                                        bar <= passwordStrength.score
                                            ? "register-password-strength__bar register-password-strength__bar--active"
                                            : "register-password-strength__bar"
                                    }
                                />
                            ))}
                        </div>

                        <span>{passwordStrength.label}</span>
                    </div>
                </div>
            </div>
        </div>
    );
}