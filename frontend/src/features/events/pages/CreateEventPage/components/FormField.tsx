import type {
    InputHTMLAttributes,
    TextareaHTMLAttributes,
} from "react";

interface BaseProps {
    label: string;
    error?: string;
    required?: boolean;
}

interface InputProps
    extends BaseProps,
        Omit<
            InputHTMLAttributes<HTMLInputElement>,
            "id"
        > {
    textarea?: false;
}

interface TextareaProps
    extends BaseProps,
        Omit<
            TextareaHTMLAttributes<HTMLTextAreaElement>,
            "id"
        > {
    textarea: true;
}

type FormFieldProps =
    | InputProps
    | TextareaProps;

export function FormField(
    props: FormFieldProps
) {
    const {
        label,
        error,
        required,
    } = props;

    const id = props.name;

    const className = error
        ? "create-event-input create-event-input--error"
        : "create-event-input";

    return (
        <div className="create-event-field">
            <label htmlFor={id}>
                {label}

                {required && (
                    <span className="required-mark">
                        *
                    </span>
                )}
            </label>

            {props.textarea
                ? renderTextarea(
                      props,
                      id,
                      className
                  )
                : renderInput(
                      props,
                      id,
                      className
                  )}

            {error && (
                <span className="create-event-error">
                    {error}
                </span>
            )}
        </div>
    );
}

function renderInput(
    props: InputProps,
    id: string | undefined,
    className: string
) {
    const {
        label,
        error,
        required,
        textarea,
        ...inputProps
    } = props;

    return (
        <input
            id={id}
            {...inputProps}
            className={className}
        />
    );
}

function renderTextarea(
    props: TextareaProps,
    id: string | undefined,
    className: string
) {
    const {
        label,
        error,
        required,
        textarea,
        ...textareaProps
    } = props;

    return (
        <textarea
            id={id}
            {...textareaProps}
            className={className}
        />
    );
}