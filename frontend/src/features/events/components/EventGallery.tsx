import type {
  EventPhotoResponseDTO,
  ProblemDetails,
} from "../../../api/generated/api";

interface EventGalleryProps {
  eventTitle: string;
  mainImageURL: string;
  photos: EventPhotoResponseDTO[];
  isLoading: boolean;
  error?: ProblemDetails | null;
  onRetry?: () => void;
}

export function EventGallery({
  eventTitle,
  mainImageURL,
  photos,
  isLoading,
  error,
  onRetry,
}: EventGalleryProps) {
  if (isLoading) {
    return (
      <div className="event-gallery event-gallery--loading">
        <div className="event-gallery__message">
          Loading event photos...
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="event-gallery event-gallery--error">
        <div className="event-gallery__message">
          {error.detail}
        </div>

        {onRetry && (
          <button
            type="button"
            onClick={onRetry}
          >
            Try again
          </button>
        )}
      </div>
    );
  }

  return (
    <div className="event-gallery">
      <div className="event-gallery__featured">
        <img
          src={mainImageURL}
          alt={`${eventTitle} main`}
        />
      </div>

      {photos.map((photo) => (
        <div
          key={photo.id ?? photo.publicId}
          className="event-gallery__photo"
        >
          <img
            src={photo.url}
            alt={photo.caption || `${eventTitle} event photo`}
          />

          {photo.caption && (
            <span className="event-gallery__caption">
              {photo.caption}
            </span>
          )}
        </div>
      ))}

      {!photos.length && (
        <div className="event-gallery__message">
          No additional photos are available for this event.
        </div>
      )}
    </div>
  );
}