import { useEventTicketTypes } from "../../../hooks/useEventTicketTypes";

import { EventTicketTypes } from "./EventTicketTypes";

interface EventTicketTypesSectionProps {
  eventId?: string;
}

export function EventTicketTypesSection({
  eventId,
}: EventTicketTypesSectionProps) {
  const {
    data: ticketTypes = [],
    isLoading,
    error,
    refetch,
  } = useEventTicketTypes(eventId);

  return (
    <section
      id="event-ticket-types"
      className="event-details-section"
    >
      <div className="event-details-section__heading">
        <span>TICKETS</span>
        <h2>Choose your ticket.</h2>
      </div>

      <EventTicketTypes
        ticketTypes={ticketTypes}
        isLoading={isLoading}
        error={error}
        onRetry={() => refetch()}
      />
    </section>
  );
}