import { formatDateTime } from "../../../../../shared/utils/formatDateTime";

import type { EventDetailsEvent } from "./EventDetailsEvent";

interface EventInformationSectionProps {
  event: EventDetailsEvent;
}

export function EventInformationSection({
  event,
}: EventInformationSectionProps) {
  return (
    <section className="event-details-section">
      <div className="event-details-section__heading">
        <span>EVENT INFORMATION</span>
        <h2>Everything you need to know.</h2>
      </div>

      <div className="event-details-info-grid">
        <div className="event-details-info-card">
          <div className="event-details-info-card__icon">
            ◷
          </div>

          <div>
            <span>Starts</span>

            <strong>
              {formatDateTime(event.startOfEvent)}
            </strong>
          </div>
        </div>

        <div className="event-details-info-card">
          <div className="event-details-info-card__icon">
            ◴
          </div>

          <div>
            <span>Ends</span>

            <strong>
              {formatDateTime(event.endOfEvent)}
            </strong>
          </div>
        </div>

        <div className="event-details-info-card">
          <div className="event-details-info-card__icon">
            ♫
          </div>

          <div>
            <span>Venue</span>

            <strong>
              {event.venueName || "Event venue"}
            </strong>
          </div>
        </div>

        <div className="event-details-info-card">
          <div className="event-details-info-card__icon">
            ⌖
          </div>

          <div>
            <span>Location</span>

            <strong>
              {event.city}, {event.country}
            </strong>
          </div>
        </div>
      </div>
    </section>
  );
}