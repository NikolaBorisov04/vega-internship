import { Link } from "react-router-dom";
import "./EventCard.css";
import type { EventResponseDTO } from "../../../../api/generated/api";
import { formatEventRange } from "../../../../shared/utils/formatDateTime";

interface EventCardProps {
  event: EventResponseDTO;
}

export function EventCard({ event }: EventCardProps) {
  const priority = event.priority?.toLowerCase() ?? "standard";
  const isPriorityEvent = priority !== "standard";

  return (
    <article className="event-card">
      <div className="event-card__image-wrapper">
        <img
          className="event-card__image"
          src={event.mainImageURL}
          alt={event.title}
          loading="lazy"
          onError={(e) => {
            e.currentTarget.style.display = "none";
            e.currentTarget.parentElement?.classList.add(
              "event-card__image-wrapper--fallback"
            );
          }}
        />

        <div className="event-card__image-overlay" />

        <span className="event-card__music-note" aria-hidden="true">
          ♪
        </span>

        {isPriorityEvent && (
          <span className="event-card__priority">
            {event.priority}
          </span>
        )}
      </div>

      <div className="event-card__content">
        <div className="event-card__meta">
          <time dateTime={new Date(event.startOfEvent).toISOString()}>
            {formatEventRange(event.startOfEvent, event.endOfEvent)}
          </time>
        </div>

        <h2 className="event-card__title">{event.title}</h2>

        <p className="event-card__description">
          {event.description}
        </p>

        <div className="event-card__location">
          <span aria-hidden="true">♫</span>

          <div>
            <strong>{event.venueName || "Event venue"}</strong>
            <span>
              {event.city}, {event.country}
            </span>
          </div>
        </div>

        <Link
          className="event-card__button"
          to={`/events/${event.id}`}
        >
          Explore event
          <span aria-hidden="true">→</span>
        </Link>
      </div>
    </article>
  );
}