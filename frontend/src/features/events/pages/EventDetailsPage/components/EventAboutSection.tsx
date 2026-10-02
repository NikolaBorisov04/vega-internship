import type { EventDetailsEvent } from "./EventDetailsEvent";

interface EventAboutSectionProps {
  event: EventDetailsEvent;
}

export function EventAboutSection({
  event,
}: EventAboutSectionProps) {
  return (
    <section className="event-details-section">
      <div className="event-details-section__heading">
        <span>ABOUT THE EVENT</span>
        <h2>Make a night of it.</h2>
      </div>

      <p className="event-details-description">
        {event.description}
      </p>
    </section>
  );
}