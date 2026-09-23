import { FormField } from "./FormField";
import { useCreateEventStore } from "../../../store/createEventStore";
import type { CreateEventErrors } from "../createEventValidation";

interface EventBasicsStepProps {
    errors: CreateEventErrors;
}

export function EventBasicsStep({
    errors,
}: EventBasicsStepProps) {
    const title = useCreateEventStore(
        (state) => state.title
    );

    const description = useCreateEventStore(
        (state) => state.description
    );

    const setField = useCreateEventStore(
        (state) => state.setField
    );

    return (
        <div className="create-event-step">
            <div className="create-event-step__intro">
                <span className="create-event-step__eyebrow">
                    Step 1
                </span>

                <h2>Tell us about your event</h2>

                <p>
                    Start with the basic information attendees
                    will see when they discover your event.
                </p>
            </div>

            <FormField
                name="title"
                label="Event title"
                placeholder="Summer Music Festival"
                value={title}
                onChange={(event) =>
                    setField("title", event.target.value)
                }
                error={errors.title}
                required
            />

            <FormField
                name="description"
                label="Description"
                placeholder="Tell attendees what makes your event special..."
                value={description}
                onChange={(event) =>
                    setField(
                        "description",
                        event.target.value
                    )
                }
                error={errors.description}
                required
                textarea
                rows={7}
            />
        </div>
    );
}