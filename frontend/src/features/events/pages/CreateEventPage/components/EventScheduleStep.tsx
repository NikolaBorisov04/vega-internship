import { FormField } from "./FormField";
import { useCreateEventStore } from "../../../store/createEventStore";
import type { CreateEventErrors } from "../createEventValidation";

interface EventScheduleStepProps {
    errors: CreateEventErrors;
}

export function EventScheduleStep({
    errors,
}: EventScheduleStepProps) {
    const startOfEvent = useCreateEventStore(
        (state) => state.startOfEvent
    );

    const endOfEvent = useCreateEventStore(
        (state) => state.endOfEvent
    );

    const setField = useCreateEventStore(
        (state) => state.setField
    );

    return (
        <div className="create-event-step">
            <div className="create-event-step__intro">
                <span className="create-event-step__eyebrow">
                    Step 3
                </span>

                <h2>When is your event?</h2>

                <p>
                    Set the beginning and ending time for your
                    event.
                </p>
            </div>

            <div className="create-event-grid">
                <FormField
                    name="startOfEvent"
                    label="Starts"
                    type="datetime-local"
                    value={startOfEvent}
                    onChange={(event) =>
                        setField(
                            "startOfEvent",
                            event.target.value
                        )
                    }
                    error={errors.startOfEvent}
                    required
                />

                <FormField
                    name="endOfEvent"
                    label="Ends"
                    type="datetime-local"
                    value={endOfEvent}
                    min={startOfEvent || undefined}
                    onChange={(event) =>
                        setField(
                            "endOfEvent",
                            event.target.value
                        )
                    }
                    error={errors.endOfEvent}
                    required
                />
            </div>
        </div>
    );
}