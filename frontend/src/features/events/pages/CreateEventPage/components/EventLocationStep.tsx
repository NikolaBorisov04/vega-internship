import type { CreateEventErrors } from "../createEventValidation";
import { useCreateEventStore } from "../../../store/createEventStore";

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
            <div className="create-event-step__header">
                <span className="create-event-step__eyebrow">
                    STEP 2
                </span>

                <h2>Where is your event?</h2>

                <p>
                    Add the location where your event will take
                    place.
                </p>
            </div>

            <div className="create-event-form">
                <div className="create-event-form__row">
                    <div className="create-event-field">
                        <label htmlFor="country">
                            Country
                        </label>

                        <input
                            id="country"
                            type="text"
                            value={country}
                            onChange={(event) =>
                                setField(
                                    "country",
                                    event.target.value
                                )
                            }
                            placeholder="e.g. Serbia"
                            aria-invalid={Boolean(errors.country)}
                        />

                        {errors.country && (
                            <span className="create-event-field__error">
                                {errors.country}
                            </span>
                        )}
                    </div>

                    <div className="create-event-field">
                        <label htmlFor="city">
                            City
                        </label>

                        <input
                            id="city"
                            type="text"
                            value={city}
                            onChange={(event) =>
                                setField(
                                    "city",
                                    event.target.value
                                )
                            }
                            placeholder="e.g. Niš"
                            aria-invalid={Boolean(errors.city)}
                        />

                        {errors.city && (
                            <span className="create-event-field__error">
                                {errors.city}
                            </span>
                        )}
                    </div>
                </div>

                <div className="create-event-field">
                    <label htmlFor="address">
                        Address
                    </label>

                    <input
                        id="address"
                        type="text"
                        value={address}
                        onChange={(event) =>
                            setField(
                                "address",
                                event.target.value
                            )
                        }
                        placeholder="e.g. Voždova 12"
                        aria-invalid={Boolean(errors.address)}
                    />

                    {errors.address && (
                        <span className="create-event-field__error">
                            {errors.address}
                        </span>
                    )}
                </div>

                <div className="create-event-field">
                    <label htmlFor="venueName">
                        Venue name
                        <span className="create-event-field__optional">
                            Optional
                        </span>
                    </label>

                    <input
                        id="venueName"
                        type="text"
                        value={venueName}
                        onChange={(event) =>
                            setField(
                                "venueName",
                                event.target.value
                            )
                        }
                        placeholder="e.g. Niš Fortress"
                    />
                </div>
            </div>
        </div>
    );
}