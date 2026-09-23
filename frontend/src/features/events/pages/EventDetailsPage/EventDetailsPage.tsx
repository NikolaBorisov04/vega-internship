import { Link, useParams } from "react-router-dom";

import { useEvent } from "../../hooks/useEvent";
import { useEventPhoto } from "../../hooks/useEventPhoto";

import { EventGallery } from "../../components/EventGallery";

import { ApiError } from "../../../../shared/api/httpClient";
import { EventPriority } from "../../../../api/generated/api";

import {
  formatDateTime,
  formatEventRange,
} from "../../../../shared/utils/formatDateTime";

import "./EventDetailsPage.css";

export function EventDetailsPage() {
  const { id } = useParams<{ id: string }>();

  const {
    data: event,
    isLoading,
    isError,
    error,
    refetch,
  } = useEvent(id);

  const {
    data: eventPhotos = [],
    isLoading: isPhotosLoading,
    isError: isPhotosError,
  } = useEventPhoto(id);

  if (isLoading) {
    return (
      <main className="event-details-page">
        <div className="event-details-container">
          <Link to="/events" className="event-details-back">
            <span>←</span>
            Back to events
          </Link>

          <div className="event-details-loading">
            <div className="event-details-loading__hero" />

            <div className="event-details-loading__content">
              <div className="skeleton skeleton--small" />
              <div className="skeleton skeleton--title" />
              <div className="skeleton" />
              <div className="skeleton skeleton--medium" />
            </div>
          </div>
        </div>
      </main>
    );
  }

  if (isError || !event) {
    const isNotFound =
      error instanceof ApiError && error.status === 404;

    return (
      <main className="event-details-page">
        <div className="event-details-container">
          <Link to="/events" className="event-details-back">
            <span>←</span>
            Back to events
          </Link>

          <section className="event-details-error">
            <div className="event-details-error__icon">
              {isNotFound ? "♪" : "!"}
            </div>

            <h1>
              {isNotFound
                ? "Event not found"
                : "Something went wrong"}
            </h1>

            <p>
              {isNotFound
                ? "This event may have been removed or is no longer available."
                : "We couldn't load the event information. Please try again."}
            </p>

            {!isNotFound && (
              <button
                type="button"
                onClick={() => refetch()}
                className="event-details-error__button"
              >
                Try again
              </button>
            )}

            <Link
              to="/events"
              className="event-details-error__link"
            >
              Browse events
            </Link>
          </section>
        </div>
      </main>
    );
  }

  const isHighPriority =
    event.priority === EventPriority.High;

  return (
    <main className="event-details-page">
      <div className="event-details-container">
        <Link to="/events" className="event-details-back">
          <span>←</span>
          Back to events
        </Link>

        {/* HERO */}
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
                    "event-details-hero__image-frame--fallback"
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
                event.endOfEvent
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
              <button
                type="button"
                className="event-details-buy-button"
                disabled
              >
                Get tickets
                <span>→</span>
              </button>

              <span className="event-details-buy-note">
                Ticket selection will be available soon.
              </span>
            </div>
          </div>
        </section>

        {/* ABOUT */}
        <section className="event-details-section">
          <div className="event-details-section__heading">
            <span>ABOUT THE EVENT</span>
            <h2>Make a night of it.</h2>
          </div>

          <p className="event-details-description">
            {event.description}
          </p>
        </section>

        {/* EVENT INFORMATION */}
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

        {/* GALLERY */}
        <section className="event-details-section">
          <div className="event-details-section__heading">
            <span>EVENT PHOTOS</span>
            <h2>See the atmosphere.</h2>
          </div>

          <EventGallery
            eventTitle={event.title}
            mainImageURL={event.mainImageURL}
            photos={eventPhotos}
            isLoading={isPhotosLoading}
            isError={isPhotosError}
          />
        </section>

        {/* ORGANIZER */}
        <section className="event-details-section">
          <div className="event-details-section__heading">
            <span>ORGANIZER</span>
            <h2>Who's behind the event?</h2>
          </div>

          <div className="event-organizer-card">
            <div className="event-organizer-card__avatar">
              ♪
            </div>

            <div className="event-organizer-card__content">
              <span>EVENT ORGANIZER</span>

              <h3>Organizer information</h3>

              <p>
                Organizer details will be displayed here once
                they are included in the event response.
              </p>
            </div>
          </div>
        </section>

        {/* SPONSORS */}
        <section className="event-details-section event-details-section--last">
          <div className="event-details-section__heading">
            <span>SPONSORS</span>
            <h2>Made possible by.</h2>
          </div>

          <div className="event-sponsors">
            <div className="event-sponsor-placeholder">
              <span>✦</span>
              <p>Event sponsor</p>
            </div>

            <div className="event-sponsor-placeholder">
              <span>✦</span>
              <p>Event sponsor</p>
            </div>

            <div className="event-sponsor-placeholder">
              <span>✦</span>
              <p>Event sponsor</p>
            </div>

            <div className="event-sponsor-placeholder">
              <span>✦</span>
              <p>Event sponsor</p>
            </div>
          </div>
        </section>
      </div>
    </main>
  );
}