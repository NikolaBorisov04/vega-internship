import { useEventPhoto } from "../../../hooks/useEventPhoto";
import { EventGallery } from "../../../components/EventGallery";

import type { EventDetailsEvent } from "./EventDetailsEvent";

interface EventGallerySectionProps {
  event: EventDetailsEvent;
}

export function EventGallerySection({
  event,
}: EventGallerySectionProps) {
  const {
    data: eventPhotos = [],
    isLoading,
    error,
    refetch,
  } = useEventPhoto(event.id);

  return (
    <section className="event-details-section">
      <div className="event-details-section__heading">
        <span>EVENT PHOTOS</span>
        <h2>See the atmosphere.</h2>
      </div>

      <EventGallery
        eventTitle={event.title}
        mainImageURL={event.mainImageURL}
        photos={eventPhotos}
        isLoading={isLoading}
        error={error}
        onRetry={() => refetch()}
      />
    </section>
  );
}