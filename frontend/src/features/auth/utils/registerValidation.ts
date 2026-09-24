import * as Yup from "yup";

import { UserRole } from "../../../api/generated/api";

import type {
    RegisterDraftFields,
    RegisterStep,
    RegistrationRole,
} from "../store/registerStore";

export type RegisterErrors = Partial<
    Record<keyof RegisterDraftFields, string>
>;

const phoneRegex = /^[0-9+()\s.-]{7,}$/;

const step1Schema = Yup.object({
    name: Yup.string()
        .trim()
        .min(2, "Please enter your full name.")
        .required("Please enter your full name."),

    email: Yup.string()
        .trim()
        .email("Please enter a valid e-mail address.")
        .required("Please enter a valid e-mail address."),

    password: Yup.string()
        .required("Please create a password.")
        .min(
            8,
            "Password must contain at least 8 characters."
        )
        .matches(
            /[A-Z]/,
            "Password needs at least one uppercase letter."
        )
        .matches(
            /[a-z]/,
            "Password needs at least one lowercase letter."
        )
        .matches(
            /[0-9]/,
            "Password needs at least one number."
        )
        .matches(
            /[^A-Za-z0-9]/,
            "Password needs at least one special character."
        ),
});

const step2Schema = Yup.object({
    country: Yup.string()
        .trim()
        .required("Please enter your country."),

    city: Yup.string()
        .trim()
        .required("Please enter your city."),

    address: Yup.string()
        .trim()
        .required("Please enter your address."),

    phoneNumber: Yup.string()
        .trim()
        .matches(
            phoneRegex,
            "Please enter a valid phone number."
        )
        .required("Please enter a valid phone number."),
});

function createStep3Schema(role: RegistrationRole) {
    return Yup.object({
        companyName:
            role === UserRole.Customer
                ? Yup.string().trim()
                : Yup.string()
                      .trim()
                      .min(
                          2,
                          "Please enter a company or organization name."
                      )
                      .required(
                          "Please enter a company or organization name."
                      ),
    });
}

export function validateRegisterStep(
    step: RegisterStep,
    data: RegisterDraftFields,
    role: RegistrationRole
): RegisterErrors {
    const schema =
        step === 1
            ? step1Schema
            : step === 2
              ? step2Schema
              : createStep3Schema(role);

    try {
        schema.validateSync(data, {
            abortEarly: false,
        });

        return {};
    } catch (error) {
        if (!(error instanceof Yup.ValidationError)) {
            return {};
        }

        const errors: RegisterErrors = {};

        error.inner.forEach((validationError) => {
            const field =
                validationError.path as keyof RegisterDraftFields;

            if (field && !errors[field]) {
                errors[field] = validationError.message;
            }
        });

        return errors;
    }
}