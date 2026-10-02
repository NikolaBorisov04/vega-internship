import { EventPriority } from "../../../../../api/generated/api";
import {
  formatEventRange,
} from "../../../../../shared/utils/formatDateTime";

import type { EventDetailsEvent } from "./EventDetailsEvent";

interface EventDetailsHeroProps {
  event: EventDetailsEvent;
}

export function EventDetailsHero({
  event,
}: EventDetailsHeroProps) {
  const isHighPriority =
    event.priority === EventPriority.High;

  return (
    <section className="event-details-hero">
      <div className="event-details-hero__visual">
        <div
          className="event-details-hero__blur"
          style={{
            backgroundImage: `url("${event.mainImageURL}")`,
          }}
        />

        <div className="event-details-hero__image-frame">
          <img
            src={event.mainImageURL}
            alt={event.title}
            className="event-details-hero__image"
            onError={(e) => {
              e.currentTarget.style.display = "none";

              e.currentTarget.parentElement?.classList.add(
                "event-details-hero__image-frame--fallback",
              );
            }}
          />
        </div>

        {isHighPriority && (
          <span className="event-details-hero__priority">
            High priority
          </span>
        )}

        <div className="event-details-hero__music-note">
          ♪
        </div>
      </div>

      <div className="event-details-hero__info">
        <div className="event-details-hero__date">
          {formatEventRange(
            event.startOfEvent,
            event.endOfEvent,
          )}
        </div>

        <h1>{event.title}</h1>

        <div className="event-details-hero__venue">
          <div className="event-details-hero__venue-icon">
            ♫
          </div>

          <div>
            <strong>
              {event.venueName || "Event venue"}
            </strong>

            <span>
              {event.address}, {event.city},{" "}
              {event.country}
            </span>
          </div>
        </div>

        <div className="event-details-hero__divider" />

        <div className="event-details-hero__actions">
          <a
            href="#event-ticket-types"
            className="event-details-buy-button"
          >
            View ticket types
            <span>↓</span>
          </a>

          <span className="event-details-buy-note">
            Choose a ticket type below. Purchase will be
            available soon.
          </span>
        </div>
      </div>
    </section>
  );
}