import { useNavigate } from "react-router-dom";
import { EventFilters } from "../../components/EventFilters/EventFilters";
import { EventsHero } from "../../components/EventsHero/EventsHero";
import { EventsList } from "../../components/EventsList/EventsList";
import { useEventFilters } from "../../hooks/useEventFilters";
import { useEvents } from "../../hooks/useEvents";
import "./EventsPage.css";
import { useAuth } from "../../../auth/context/AuthContext";
import { UserRole } from "../../../../api/generated/api";
import { ROUTES } from "../../../../constants/routes";

export function EventsPage() {
  const {
    data: events = [],
    isLoading,
    isError,
    refetch,
  } = useEvents();

  const {
    search,
    setSearch,
    city,
    setCity,
    sort,
    setSort,
    cities,
    filteredEvents,
    clearFilters,
  } = useEventFilters(events);

  const navigate = useNavigate();
  const { role } = useAuth();

  return (
    <main className="events-page">
      <EventsHero />

      <section className="events-content">
        <div className="events-toolbar">
          <div className="events-toolbar__heading">
            <p className="events-toolbar__label">EXPLORE</p>

            <h2>Events worth showing up for</h2>
          </div>

          <div className="events-toolbar__actions">
            {(role === UserRole.Organizer ||
              role === UserRole.Admin) && (
              <button
                type="button"
                className="events-create-button"
                onClick={() => navigate(ROUTES.CREATEEVENT)}
              >
                <span>+</span>
                Create Event
              </button>
            )}

            <span className="events-toolbar__count">
              {filteredEvents.length}{" "}
              {filteredEvents.length === 1 ? "event" : "events"}
            </span>
          </div>
        </div>

        <EventFilters
          search={search}
          city={city}
          sort={sort}
          cities={cities}
          onSearchChange={setSearch}
          onCityChange={setCity}
          onSortChange={setSort}
        />

        <EventsList
          events={filteredEvents}
          isLoading={isLoading}
          isError={isError}
          onRetry={refetch}
          onClearFilters={clearFilters}
        />
      </section>
    </main>
  );
}