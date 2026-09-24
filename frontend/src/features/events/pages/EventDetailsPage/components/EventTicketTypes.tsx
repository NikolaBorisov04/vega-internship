import type { ProblemDetails, TicketTypeResponseDTO } from "../../../../../api/generated/api";
import { formatAvailability } from "../utils/formatAvailability";
import { formatPrice } from "../utils/formatPrice";

interface EventTicketTypesProps {
  ticketTypes: TicketTypeResponseDTO[];
  isLoading: boolean;
  error?: ProblemDetails | null;
  onRetry?: () => void;
}

export function EventTicketTypes({
  ticketTypes,
  isLoading,
  error,
  onRetry,
}: EventTicketTypesProps) {
  if (isLoading) {
    return (
      <div className="event-ticket-types event-ticket-types--loading">
        {Array.from({ length: 3 }).map((_, index) => (
          <div
            key={index}
            className="event-ticket-card event-ticket-card--skeleton"
          >
            <div className="event-ticket-card__skeleton-image" />

            <div className="event-ticket-card__skeleton-content">
              <div className="event-ticket-card__skeleton-line event-ticket-card__skeleton-line--title" />
              <div className="event-ticket-card__skeleton-line" />
              <div className="event-ticket-card__skeleton-line event-ticket-card__skeleton-line--short" />
              <div className="event-ticket-card__skeleton-button" />
            </div>
          </div>
        ))}
      </div>
    );
  }

  if (error) {
    return (
      <div className="event-ticket-types-message event-ticket-types-message--error">
        <div className="event-ticket-types-message__icon">
          !
        </div>

        <div>
          <h3>Couldn't load ticket types</h3>

          <p>
            {error.detail ||
              "We couldn't load the available ticket types for this event."}
          </p>
        </div>

        {onRetry && (
          <button
            type="button"
            onClick={onRetry}
            className="event-ticket-types-message__button"
          >
            Try again
          </button>
        )}
      </div>
    );
  }

  if (!ticketTypes.length) {
    return (
      <div className="event-ticket-types-message">
        <div className="event-ticket-types-message__icon">
          ♫
        </div>

        <div>
          <h3>No ticket types available</h3>

          <p>
            Ticket types haven't been added for this event yet.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="event-ticket-types">
      {ticketTypes.map((ticketType) => {
        const isSoldOut =
          ticketType.quantityAvailable !== undefined &&
          ticketType.quantityAvailable <= 0;

        return (
          <article
            key={ticketType.id ?? ticketType.name}
            className={`event-ticket-card${
              isSoldOut ? " event-ticket-card--sold-out" : ""
            }`}
          >
            <div className="event-ticket-card__visual">
              {ticketType.imageUrl && (
                <img
                  src={ticketType.imageUrl}
                  alt={`${ticketType.name} ticket`}
                  className="event-ticket-card__image"
                  onError={(event) => {
                    event.currentTarget.style.display = "none";
                  }}
                />
              )}

              <div className="event-ticket-card__visual-overlay" />
              <span className="event-ticket-card__availability">
                {formatAvailability(
                  ticketType.quantityAvailable,
                )}
              </span>
            </div>

            <div className="event-ticket-card__body">
              <div className="event-ticket-card__header">
                <div>
                  <h3>{ticketType.name}</h3>

                  <span className="event-ticket-card__availability-mobile">
                    {formatAvailability(
                      ticketType.quantityAvailable,
                    )}
                  </span>
                </div>

                <div className="event-ticket-card__price">
                  {formatPrice(ticketType.price)}
                </div>
              </div>

              <p className="event-ticket-card__description">
                {ticketType.description}
              </p>

              <div className="event-ticket-card__footer">
                <span className="event-ticket-card__quantity">
                  {formatAvailability(
                    ticketType.quantityAvailable,
                  )}
                </span>

                <button
                  type="button"
                  disabled
                  className="event-ticket-card__button"
                >
                  {isSoldOut ? "Sold out" : "Purchase"}
                  <span>→</span>
                </button>
              </div>
            </div>
          </article>
        );
      })}
    </div>
  );
}