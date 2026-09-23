import { useState } from "react";
import { useNavigate } from "react-router-dom";

import { useCreateEventStore } from "../../store/createEventStore";

import {
    validateStep,
    type CreateEventErrors,
} from "./createEventValidation";

import { CreateEventProgress } from "./components/CreateEventProgress";
import { EventBasicsStep } from "./components/EventBasicStep";
import { EventLocationStep } from "./components/EventLocationStep";
import { EventMediaStep } from "./components/EventMediaStep";
import { EventScheduleStep } from "./components/EventScheduleStep";

import "./CreateEventPage.css";
import { useCreateEvent } from "../../hooks/useCreateEvent";

export function CreateEventPage() {
    const navigate = useNavigate();

    const currentStep = useCreateEventStore(
        (state) => state.currentStep
    );

    const nextStep = useCreateEventStore(
        (state) => state.nextStep
    );

    const previousStep = useCreateEventStore(
        (state) => state.previousStep
    );

    const reset = useCreateEventStore(
        (state) => state.reset
    );

    const title = useCreateEventStore(
        (state) => state.title
    );

    const description = useCreateEventStore(
        (state) => state.description
    );

    const country = useCreateEventStore(
        (state) => state.country
    );

    const city = useCreateEventStore(
        (state) => state.city
    );

    const address = useCreateEventStore(
        (state) => state.address
    );

    const venueName = useCreateEventStore(
        (state) => state.venueName
    );

    const startOfEvent = useCreateEventStore(
        (state) => state.startOfEvent
    );

    const endOfEvent = useCreateEventStore(
        (state) => state.endOfEvent
    );

    const mainImage = useCreateEventStore(
        (state) => state.mainImage
    );

    const {
        mutateAsync: createEvent,
        isPending,
    } = useCreateEvent();

    const [errors, setErrors] =
        useState<CreateEventErrors>({});

    const [submitError, setSubmitError] =
        useState<string | null>(null);

    const draft = {
        title,
        description,
        country,
        city,
        address,
        venueName,
        startOfEvent,
        endOfEvent,
        mainImage,
    };

    const handleNext = () => {
        const stepErrors = validateStep(
            currentStep,
            draft
        );

        setErrors(stepErrors);

        if (Object.keys(stepErrors).length > 0) {
            return;
        }

        nextStep();
        setErrors({});
    };

    const handleBack = () => {
        setErrors({});
        previousStep();
    };

    const handleSubmit = async () => {
        const stepErrors = validateStep(
            4,
            draft
        );

        if (Object.keys(stepErrors).length > 0) {
            setErrors(stepErrors);
            return;
        }

        if (!mainImage) {
            return;
        }

        setSubmitError(null);

        try {
            await createEvent(draft);

            reset();
            navigate("/events");
        } catch (error) {
            setSubmitError(
                error instanceof Error
                    ? error.message
                    : "Failed to create the event."
            );
        }
    };

    return (
        <main className="create-event-page">
            <div className="create-event-shell">
                <div className="create-event-heading">
                    <div>
                        <span className="create-event-eyebrow">
                            ORGANIZER
                        </span>

                        <h1>Create an event</h1>

                        <p>
                            Build your event step by step and
                            publish it when everything looks right.
                        </p>
                    </div>
                </div>

                <CreateEventProgress
                    currentStep={currentStep}
                />

                <section className="create-event-card">
                    {currentStep === 1 && (
                        <EventBasicsStep
                            errors={errors}
                        />
                    )}

                    {currentStep === 2 && (
                        <EventLocationStep
                            errors={errors}
                        />
                    )}

                    {currentStep === 3 && (
                        <EventScheduleStep
                            errors={errors}
                        />
                    )}

                    {currentStep === 4 && (
                        <EventMediaStep
                            errors={errors}
                        />
                    )}

                    {submitError && (
                        <div className="create-event-submit-error">
                            {submitError}
                        </div>
                    )}

                    <div className="create-event-actions">
                        {currentStep > 1 ? (
                            <button
                                type="button"
                                className="create-event-button create-event-button--secondary"
                                onClick={handleBack}
                            >
                                Back
                            </button>
                        ) : (
                            <button
                                type="button"
                                className="create-event-button create-event-button--secondary"
                                onClick={() =>
                                    navigate("/events")
                                }
                            >
                                Cancel
                            </button>
                        )}

                        <div className="create-event-actions__right">
                            {currentStep < 4 ? (
                                <button
                                    type="button"
                                    className="create-event-button create-event-button--primary"
                                    onClick={handleNext}
                                >
                                    Continue
                                    <span>→</span>
                                </button>
                            ) : (
                                <button
                                    type="button"
                                    className="create-event-button create-event-button--primary"
                                    onClick={handleSubmit}
                                    disabled={isPending}
                                >
                                    {isPending
                                        ? "Creating..."
                                        : "Create event"}
                                </button>
                            )}
                        </div>
                    </div>
                </section>
            </div>
        </main>
    );
}