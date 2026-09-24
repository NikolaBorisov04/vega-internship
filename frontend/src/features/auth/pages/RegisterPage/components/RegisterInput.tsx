import {
    Eye,
    EyeOff,
} from "lucide-react";

import {
    useState,
    type InputHTMLAttributes,
    type ReactNode,
} from "react";

interface RegisterInputProps
    extends Omit<
        InputHTMLAttributes<HTMLInputElement>,
        | "id"
        | "value"
        | "onChange"
    > {
    id: string;
    label: string;
    icon: ReactNode;

    value: string;

    onValueChange: (
        value: string
    ) => void;

    error?: string;
}

export function RegisterInput({
    id,
    label,
    icon,
    value,
    onValueChange,
    error,
    type = "text",
    ...inputProps
}: RegisterInputProps) {
    const [
        showPassword,
        setShowPassword,
    ] = useState(false);

    const isPassword = type === "password";

    const inputType =
        isPassword && showPassword
            ? "text"
            : type;

    return (
        <div className="register-field">
            <label
                className="register-field__label"
                htmlFor={id}
            >
                {label}
            </label>

            <div
                className={`register-input-wrapper ${
                    error
                        ? "register-input-wrapper--error"
                        : ""
                }`}
            >
                <span className="register-input-icon">
                    {icon}
                </span>

                <input
                    {...inputProps}
                    id={id}
                    type={inputType}
                    value={value}
                    onChange={(event) =>
                        onValueChange(
                            event.target.value
                        )
                    }
                    aria-invalid={
                        error
                            ? true
                            : undefined
                    }
                    aria-describedby={
                        error
                            ? `${id}-error`
                            : undefined
                    }
                />

                {isPassword && (
                    <button
                        type="button"
                        className="register-password-toggle"
                        onClick={() =>
                            setShowPassword(
                                (previous) =>
                                    !previous
                            )
                        }
                        aria-label={
                            showPassword
                                ? "Hide password"
                                : "Show password"
                        }
                    >
                        {showPassword ? (
                            <EyeOff size={18} />
                        ) : (
                            <Eye size={18} />
                        )}
                    </button>
                )}
            </div>

            {error && (
                <span
                    id={`${id}-error`}
                    className="register-field__error"
                    role="alert"
                >
                    {error}
                </span>
            )}
        </div>
    );
}