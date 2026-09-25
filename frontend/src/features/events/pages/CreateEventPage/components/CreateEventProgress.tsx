interface CreateEventProgressProps {
    currentStep: 1 | 2 | 3 | 4;
}

const steps = [
    "Details",
    "Location",
    "Schedule",
    "Image",
] as const;

export function CreateEventProgress({
    currentStep,
}: CreateEventProgressProps) {
    return (
        <div className="create-event-progress">
            {steps.map((label, index) => {
                const step =
                    (index + 1) as 1 | 2 | 3 | 4;

                const isActive =
                    step === currentStep;

                const isComplete =
                    step < currentStep;

                return (
                    <div
                        key={label}
                        className={
                            "create-event-progress__item" +
                            (isActive
                                ? " create-event-progress__item--active"
                                : "") +
                            (isComplete
                                ? " create-event-progress__item--complete"
                                : "")
                        }
                    >
                        <span className="create-event-progress__number">
                            {isComplete ? "✓" : step}
                        </span>

                        <span className="create-event-progress__label">
                            {label}
                        </span>
                    </div>
                );
            })}
        </div>
    );
}