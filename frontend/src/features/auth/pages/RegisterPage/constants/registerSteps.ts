import type { RegisterStep } from "../../../store/registerStore";

export interface StepDefinition {
    id: RegisterStep;
    title: string;
    caption: string;
}

export const REGISTER_STEPS: StepDefinition[] = [
    {
        id: 1,
        title: "Identity",
        caption: "Introduce yourself",
    },
    {
        id: 2,
        title: "Details",
        caption: "Set your location",
    },
];

export const REGISTER_ORGANIZATION_STEP: StepDefinition = {
    id: 3,
    title: "Organization",
    caption: "Tell us who you represent",
};