import { useMutation } from "@tanstack/react-query";

import type { CreateEventRequest } from "../../../api/generated/createEventRequest";

const API_URL = import.meta.env.VITE_API_URL.replace(/\/$/, "");

async function createEventRequest(
    draft: CreateEventRequest
): Promise<void> {
    if (!draft.mainImage) {
        throw new Error("Main image is required.");
    }

    const formData = new FormData();

    formData.append("Title", draft.title);
    formData.append("Description", draft.description);
    formData.append("Country", draft.country);
    formData.append("City", draft.city);
    formData.append("Address", draft.address);

    if (draft.venueName.trim()) {
        formData.append(
            "VenueName",
            draft.venueName
        );
    }

    formData.append(
        "StartOfEvent",
        new Date(
            draft.startOfEvent
        ).toISOString()
    );

    formData.append(
        "EndOfEvent",
        new Date(
            draft.endOfEvent
        ).toISOString()
    );

    formData.append(
        "MainImage",
        draft.mainImage,
        draft.mainImage.name
    );

    const response = await fetch(
        `${API_URL}/api/Event/create`,
        {
            method: "POST",
            credentials: "include",
            body: formData,
        }
    );

    if (response.ok) {
        return;
    }

    let message =
        "Failed to create the event.";

    try {
        const problem =
            await response.json();

        message =
            problem.detail ??
            problem.title ??
            message;
    } catch {
        // Response was not JSON.
    }

    throw new Error(message);
}

export function useCreateEvent() {
    return useMutation({
        mutationFn: (
            draft: CreateEventRequest
        ) => createEventRequest(draft),
    });
}