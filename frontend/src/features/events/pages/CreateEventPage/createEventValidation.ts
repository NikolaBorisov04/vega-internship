import type { CreateEventRequest } from "../../../../api/generated/api";

export type CreateEventField =
    keyof CreateEventRequest;

export type CreateEventErrors = Partial<
    Record<CreateEventField, string>
>;

export function validateStep(
    step: 1 | 2 | 3 | 4,
    draft: CreateEventRequest
): CreateEventErrors {
    const errors: CreateEventErrors = {};

    if (step === 1) {
        if (!draft.title.trim()) {
            errors.title = "Title is required.";
        }

        if (!draft.description.trim()) {
            errors.description =
                "Description is required.";
        }
    }

    if (step === 2) {
        if (!draft.country.trim()) {
            errors.country = "Country is required.";
        }

        if (!draft.city.trim()) {
            errors.city = "City is required.";
        }

        if (!draft.address.trim()) {
            errors.address = "Address is required.";
        }
    }

    if (step === 3) {
        if (!draft.startOfEvent) {
            errors.startOfEvent =
                "Start date and time are required.";
        }

        if (!draft.endOfEvent) {
            errors.endOfEvent =
                "End date and time are required.";
        }

        if (
            draft.startOfEvent &&
            draft.endOfEvent &&
            new Date(draft.endOfEvent) <=
                new Date(draft.startOfEvent)
        ) {
            errors.endOfEvent =
                "End of event must be after the start.";
        }
    }

    if (step === 4) {
        if (!draft.mainImage) {
            errors.mainImage =
                "A main image is required.";
        }
    }

    return errors;
}