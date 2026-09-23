import { useEffect, useState } from "react";
import { useCreateEventStore } from "../../../store/createEventStore";
import type { CreateEventErrors } from "../createEventValidation";

interface EventMediaStepProps {
    errors: CreateEventErrors;
}

export function EventMediaStep({
    errors,
}: EventMediaStepProps) {
    const mainImage = useCreateEventStore(
        (state) => state.mainImage
    );

    const setField = useCreateEventStore(
        (state) => state.setField
    );

    const [previewUrl, setPreviewUrl] = useState<
        string | null
    >(null);

    useEffect(() => {
        if (!mainImage) {
            setPreviewUrl(null);
            return;
        }

        const url =
            URL.createObjectURL(mainImage);

        setPreviewUrl(url);

        return () => {
            URL.revokeObjectURL(url);
        };
    }, [mainImage]);

    return (
        <div className="create-event-step">
            <div className="create-event-step__intro">
                <span className="create-event-step__eyebrow">
                    Step 4
                </span>

                <h2>Make your event stand out</h2>

                <p>
                    Choose the main image that will represent
                    your event across the Events page.
                </p>
            </div>

            <label
                htmlFor="mainImage"
                className="create-event-upload"
            >
                <input
                    id="mainImage"
                    type="file"
                    accept="image/*"
                    onChange={(event) => {
                        const file =
                            event.target.files?.[0] ??
                            null;

                        setField(
                            "mainImage",
                            file
                        );
                    }}
                />

                {previewUrl ? (
                    <img
                        src={previewUrl}
                        alt="Selected event"
                        className="create-event-upload__preview"
                    />
                ) : (
                    <div className="create-event-upload__empty">
                        <div className="create-event-upload__icon">
                            ↑
                        </div>

                        <strong>
                            Choose your main image
                        </strong>

                        <span>
                            Click here to browse your files
                        </span>
                    </div>
                )}
            </label>

            {errors.mainImage && (
                <span className="create-event-error">
                    {errors.mainImage}
                </span>
            )}

            {mainImage && (
                <div className="create-event-file-name">
                    {mainImage.name}
                </div>
            )}
        </div>
    );
}