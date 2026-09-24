import { FormField } from "./FormField";
import { useCreateEventStore } from "../../../store/createEventStore";
import type { CreateEventErrors } from "../createEventValidation";

interface EventLocationStepProps {
    errors: CreateEventErrors;
}

export function EventLocationStep({
    errors,
}: EventLocationStepProps) {
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

    const setField = useCreateEventStore(
        (state) => state.setField
    );

    return (
        <div className="create-event-step">
            <div className="create-event-step__intro">
                <span className="create-event-step__eyebrow">
                    Step 2
                </span>

                <h2>Where is your event?</h2>

                <p>
                    Add the location where your event will take
                    place.
                </p>
            </div>

            <div className="create-event-grid">
                <FormField
                    name="country"
                    label="Country"
                    placeholder="e.g. Serbia"
                    value={country}
                    onChange={(event) =>
                        setField("country", event.target.value)
                    }
                    error={errors.country}
                    required
                />

                <FormField
                    name="city"
                    label="City"
                    placeholder="e.g. Niš"
                    value={city}
                    onChange={(event) =>
                        setField("city", event.target.value)
                    }
                    error={errors.city}
                    required
                />
            </div>

            <FormField
                name="address"
                label="Address"
                placeholder="e.g. Voždova 12"
                value={address}
                onChange={(event) =>
                    setField("address", event.target.value)
                }
                error={errors.address}
                required
            />

            <FormField
                name="venueName"
                label="Venue name"
                placeholder="e.g. Niš Fortress"
                value={venueName}
                onChange={(event) =>
                    setField("venueName", event.target.value)
                }
            />
        </div>
    );
}
