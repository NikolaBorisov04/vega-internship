import { Link, useParams } from "react-router-dom";

import { useEvent } from "../../hooks/useEvent";

import { ApiError } from "../../../../shared/api/httpClient";

import { EventAboutSection } from "./components/EventAboutSection";
import { EventDetailsHero } from "./components/EventDetailsHero";
import { EventGallerySection } from "./components/EventGallerySection";
import { EventInformationSection } from "./components/EventInformationSection";
import { EventOrganizerSection } from "./components/EventOrganizerSection";
import { EventSponsors } from "./components/EventSponsors";

import "./EventDetailsPage.css";
import { EventTicketTypesSection } from "./components/EventTicketTypesSection";

export function EventDetailsPage() {
  const { id } = useParams<{ id: string }>();

  const {
    data: event,
    isLoading,
    isError,
    error,
    refetch,
  } = useEvent(id);

  if (isLoading) {
    return (
      <main className="event-details-page">
        <div className="event-details-container">
          <Link
            to="/events"
            className="event-details-back"
          >
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
          <Link
            to="/events"
            className="event-details-back"
          >
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

  return (
    <main className="event-details-page">
      <div className="event-details-container">
        <Link
          to="/events"
          className="event-details-back"
        >
          <span>←</span>
          Back to events
        </Link>

        <EventDetailsHero event={event} />

        <EventAboutSection event={event} />

        <EventInformationSection event={event} />

        <EventTicketTypesSection eventId={event.id} />

        <EventGallerySection event={event} />

        <EventOrganizerSection eventId={event.id} />

        <EventSponsors eventId={event.id} />
      </div>
    </main>
  );
}