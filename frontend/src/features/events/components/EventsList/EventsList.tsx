import type { EventResponseDTO } from "../../../../api/generated/api";
import { EventCard } from "../EventCard/EventCard";
import "./EventsList.css"

interface EventsListProps {
  events: EventResponseDTO[];
  isLoading: boolean;
  isError: boolean;
  onRetry: () => void;
  onClearFilters: () => void;
}

const skeletons = Array.from({ length: 6 });

export function EventsList({
  events,
  isLoading,
  isError,
  onRetry,
  onClearFilters,
}: EventsListProps) {
  if (isLoading) {
    return (
      <div className="events-grid">
        {skeletons.map((_, index) => (
          <div className="event-card-skeleton" key={index}>
            <div className="event-card-skeleton__image" />

            <div className="event-card-skeleton__body">
              <div className="event-card-skeleton__line event-card-skeleton__line--small" />
              <div className="event-card-skeleton__line event-card-skeleton__line--title" />
              <div className="event-card-skeleton__line" />
              <div className="event-card-skeleton__line" />
              <div className="event-card-skeleton__button" />
            </div>
          </div>
        ))}
      </div>
    );
  }

  if (isError) {
    return (
      <div className="events-state">
        <div className="events-state__icon">!</div>

        <h3>We couldn't load the events.</h3>

        <p>
          Something went wrong while talking to the event service.
        </p>

        <button
          type="button"
          className="events-state__button"
          onClick={onRetry}
        >
          Try again
        </button>
      </div>
    );
  }

  if (events.length === 0) {
    return (
      <div className="events-state">
        <div className="events-state__icon">♪</div>

        <h3>No events found.</h3>

        <p>
          Try another search or remove one of the filters.
        </p>

        <button
          type="button"
          className="events-state__button"
          onClick={onClearFilters}
        >
          Clear filters
        </button>
      </div>
    );
  }

  return (
    <div className="events-grid">
      {events.map((event) => (
        <EventCard key={event.id} event={event} />
      ))}
    </div>
  );
}