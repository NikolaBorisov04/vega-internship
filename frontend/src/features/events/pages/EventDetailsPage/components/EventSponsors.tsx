import { useSponsor } from "../../../hooks/useSponsor";
import { SponsorCard } from "./SponsorCard";


interface EventSponsorsProps {
  eventId?: string;
}

const SPONSOR_SKELETONS = Array.from({ length: 4 });

export function EventSponsors({ eventId }: EventSponsorsProps) {
  const {
    data: sponsors = [],
    isLoading,
    isError,
    refetch,
  } = useSponsor(eventId);

  return (
    <section className="event-details-section event-details-section--last">
      <div className="event-details-section__heading">
        <span>SPONSORS</span>
        <h2>Made possible by.</h2>
      </div>

      {isLoading && (
        <div className="event-sponsors" aria-label="Loading sponsors">
          {SPONSOR_SKELETONS.map((_, index) => (
            <div
              key={index}
              className="event-sponsor-card event-sponsor-card--skeleton"
              aria-hidden="true"
            >
              <div className="event-sponsor-card__skeleton-visual" />
              <div className="event-sponsor-card__skeleton-body">
                <div className="event-sponsor-card__skeleton-line event-sponsor-card__skeleton-line--title" />
                <div className="event-sponsor-card__skeleton-line" />
                <div className="event-sponsor-card__skeleton-line event-sponsor-card__skeleton-line--short" />
              </div>
            </div>
          ))}
        </div>
      )}

      {!isLoading && isError && (
        <div className="event-sponsors-message event-sponsors-message--error">
          <div className="event-sponsors-message__icon">!</div>

          <div>
            <h3>We couldn't load the sponsors.</h3>
            <p>
              The event loaded successfully, but the sponsor information could
              not be retrieved.
            </p>
          </div>

          <button type="button" onClick={() => refetch()}>
            Try again
          </button>
        </div>
      )}

      {!isLoading && !isError && sponsors.length === 0 && (
        <div className="event-sponsors-message">
          <div className="event-sponsors-message__icon">✦</div>

          <div>
            <h3>No sponsors announced yet.</h3>
            <p>
              Sponsor information will appear here when sponsors are associated
              with this event.
            </p>
          </div>
        </div>
      )}

      {!isLoading && !isError && sponsors.length > 0 && (
        <div className="event-sponsors">
          {sponsors.map((sponsor) => (
            <SponsorCard
              key={sponsor.id ?? `${sponsor.name}-${sponsor.taxId}`}
              sponsor={sponsor}
            />
          ))}
        </div>
      )}
    </section>
  );
}