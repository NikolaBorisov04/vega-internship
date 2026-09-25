import * as yup from "yup";
import type { CreateEventRequest } from "../../../../api/generated/createEventRequest";

export type CreateEventField = keyof CreateEventRequest;

export type CreateEventErrors = Partial<Record<CreateEventField, string>>;

const stepSchemas: Record<
    1 | 2 | 3 | 4,
    yup.ObjectSchema<any>> = {
    1: yup.object({
        title: yup
            .string()
            .trim()
            .required("Title is required."),

        description: yup
            .string()
            .trim()
            .required("Description is required."),
    }),

    2: yup.object({
        country: yup
            .string()
            .trim()
            .required("Country is required."),

        city: yup
            .string()
            .trim()
            .required("City is required."),

        address: yup
            .string()
            .trim()
            .required("Address is required."),
    }),

    3: yup.object({
        startOfEvent: yup
            .string()
            .required("Start date and time are required."),

        endOfEvent: yup
            .string()
            .required("End date and time are required.")
            .test(
                "end-after-start",
                "End of event must be after the start.",
                function (endOfEvent) {
                    const { startOfEvent } = this.parent;

                    if (!startOfEvent || !endOfEvent) {
                        return true;
                    }

                    return (
                        new Date(endOfEvent) >
                        new Date(startOfEvent)
                    );
                }
            ),
    }),

    4: yup.object({
        mainImage: yup
            .mixed()
            .required("A main image is required."),
    }),
};

export function validateStep(step: 1 | 2 | 3 | 4, draft: CreateEventRequest): CreateEventErrors
{
    const schema = stepSchemas[step];

    try {
        schema.validateSync(draft, {
            abortEarly: false,
        });

        return {};
    } catch (error) {
        if (!(error instanceof yup.ValidationError)) {
            throw error;
        }

        const errors: CreateEventErrors = {};

        for (const validationError of error.inner) {
            if (!validationError.path) {
                continue;
            }

            errors[
                validationError.path as CreateEventField
            ] = validationError.message;
        }

        return errors;
    }
}