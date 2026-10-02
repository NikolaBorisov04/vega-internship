import { useOrganizer } from "../../../hooks/useOrganizer";

interface EventOrganizerSectionProps {
  eventId?: string;
}

export function EventOrganizerSection({
  eventId,
}: EventOrganizerSectionProps) {
  const {
    data: organizer,
    isLoading,
    isError,
    refetch,
  } = useOrganizer(eventId);

  return (
    <section className="event-details-section">
      <div className="event-details-section__heading">
        <span>ORGANIZER</span>
        <h2>Who's behind the event?</h2>
      </div>

      <div className="event-organizer-card">
        <div className="event-organizer-card__avatar">
          {organizer?.name
            ? organizer.name.charAt(0).toUpperCase()
            : "♪"}
        </div>

        <div className="event-organizer-card__content">
          <span>EVENT ORGANIZER</span>

          {isLoading ? (
            <>
              <h3>Loading organizer...</h3>

              <p>
                Organizer information is being loaded.
              </p>
            </>
          ) : isError || !organizer ? (
            <>
              <h3>Organizer information unavailable</h3>

              <p>
                We couldn't load the organizer information.
              </p>

              <button
                type="button"
                onClick={() => refetch()}
                className="event-details-error__button"
              >
                Try again
              </button>
            </>
          ) : (
            <>
              <h3>
                {organizer.companyName || organizer.name}
              </h3>

              <p>{organizer.name}</p>

              <p>{organizer.email}</p>

              {(organizer.city || organizer.country) && (
                <p>
                  {[organizer.city, organizer.country]
                    .filter(Boolean)
                    .join(", ")}
                </p>
              )}

              {organizer.phoneNumber && (
                <p>{organizer.phoneNumber}</p>
              )}
            </>
          )}
        </div>
      </div>
    </section>
  );
}