import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import type { CreateEventRequest } from "../../../api/generated/createEventRequest";


interface CreateEventState extends CreateEventRequest {
    currentStep: 1 | 2 | 3 | 4;

    setField: <K extends keyof CreateEventRequest>(
        field: K,
        value: CreateEventRequest[K]
    ) => void;

    nextStep: () => void;
    previousStep: () => void;
    reset: () => void;
}

const initialDraft: CreateEventRequest = {
    title: "",
    description: "",
    country: "",
    city: "",
    address: "",
    venueName: "",
    startOfEvent: "",
    endOfEvent: "",
    mainImage: null,
};

export const useCreateEventStore =
    create<CreateEventState>()(
        persist(
            (set) => ({
                ...initialDraft,

                currentStep: 1,

                setField: (field, value) =>
                    set({
                        [field]: value,
                    } as Partial<CreateEventRequest>),

                nextStep: () =>
                    set((state) => ({
                        currentStep:
                            state.currentStep < 4 ? ((state.currentStep + 1) as 1 | 2 | 3 | 4) : 4,
                    })),

                previousStep: () =>
                    set((state) => ({
                        currentStep:
                            state.currentStep > 1 ? ((state.currentStep - 1) as 1 | 2 | 3 | 4) : 1,
                    })),

                reset: () =>
                    set({
                        ...initialDraft,
                        currentStep: 1,
                    }),
            }),
            {
                name: "create-event-draft",

                storage: createJSONStorage(
                    () => localStorage
                ),

                partialize: (state) => ({
                    title: state.title,
                    description: state.description,
                    country: state.country,
                    city: state.city,
                    address: state.address,
                    venueName: state.venueName,
                    startOfEvent: state.startOfEvent,
                    endOfEvent: state.endOfEvent,
                    currentStep: state.currentStep,

                    // we can't persist a file inside the local storage
                    mainImage: null,
                }),
            }
        )
    );