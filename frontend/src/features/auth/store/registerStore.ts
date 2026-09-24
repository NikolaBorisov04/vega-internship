import { create } from "zustand";
import {
    createJSONStorage,
    persist,
} from "zustand/middleware";

import type { UserRole } from "../../../api/generated/api";

export type RegistrationRole =
    | UserRole.Customer
    | UserRole.Organizer
    | UserRole.Admin;

export type RegistrationScope =
    | "guest"
    | "admin";

export type RegisterStep = 1 | 2 | 3;

// Mogao sam ovde da koristim vec izgenerisan interface nego sam hteo da ovaj bude zajednicki i da ima sva polja pa posle da ga prebacim u onaj koji mi treba zapravo za api poziv
export interface RegisterDraftFields {
    name: string;
    email: string;
    password: string;
    country: string;
    city: string;
    address: string;
    phoneNumber: string;
    companyName: string;
}

interface RegisterState extends RegisterDraftFields {
    selectedRole: RegistrationRole | null;
    currentStep: RegisterStep;
    scope: RegistrationScope | null;

    setField: <
        K extends keyof RegisterDraftFields
    >(
        field: K,
        value: RegisterDraftFields[K]
    ) => void;

    selectRole: (
        role: RegistrationRole,
        scope: RegistrationScope
    ) => void;

    nextStep: (totalSteps: number) => void;
    previousStep: () => void;

    reset: () => void;
}

const initialDraft: RegisterDraftFields = {
    name: "",
    email: "",
    password: "",
    country: "",
    city: "",
    address: "",
    phoneNumber: "",
    companyName: "",
};

export const useRegisterStore =
    create<RegisterState>()(
        persist(
            (set) => ({
                ...initialDraft,

                selectedRole: null,
                currentStep: 1,
                scope: null,

                setField: (field, value) =>
                    set({
                        [field]: value,
                    } as Partial<RegisterDraftFields>),

                selectRole: (role, scope) =>
                    set({
                        selectedRole: role,
                        currentStep: 1,
                        scope,
                    }),

                nextStep: (totalSteps) =>
                    set((state) => ({
                        currentStep: Math.min(
                            state.currentStep + 1,
                            totalSteps
                        ) as RegisterStep,
                    })),

                previousStep: () =>
                    set((state) => ({
                        currentStep: Math.max(
                            state.currentStep - 1,
                            1
                        ) as RegisterStep,
                    })),

                reset: () =>
                    set({
                        ...initialDraft,
                        selectedRole: null,
                        currentStep: 1,
                        scope: null,
                    }),
            }),
            {
                name: "register-draft",

                storage: createJSONStorage(
                    () => localStorage
                ),

                partialize: (state) => ({
                    name: state.name,
                    email: state.email,

                    // ovo nije pametno bas da se perzistuje
                    password: "",

                    country: state.country,
                    city: state.city,
                    address: state.address,
                    phoneNumber: state.phoneNumber,
                    companyName: state.companyName,

                    selectedRole: state.selectedRole,
                    currentStep: state.currentStep,
                    scope: state.scope,
                }),
            }
        )
    );