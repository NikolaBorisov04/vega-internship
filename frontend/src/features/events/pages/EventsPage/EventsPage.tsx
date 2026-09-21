import { EventFilters } from "../../components/EventFilters/EventFilters";
import { EventsHero } from "../../components/EventsHero/EventsHero";
import { EventsList } from "../../components/EventsList/EventsList";
import { useEventFilters } from "../../hooks/useEventFilters";
import { useEvents } from "../../hooks/useEvents";
import "./EventsPage.css";

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

  return (
    <main className="events-page">
      <EventsHero />
      
      <section className="events-content">
        <header className="events-toolbar">
          <div>
            <p className="events-toolbar__label">EXPLORE</p>

            <h2>Events worth showing up for</h2>
          </div>

          <span className="events-toolbar__count">
            {filteredEvents.length}{" "}
            {filteredEvents.length === 1 ? "event" : "events"}
          </span>
        </header>

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