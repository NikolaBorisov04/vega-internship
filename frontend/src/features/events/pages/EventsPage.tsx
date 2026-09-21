import { useMemo, useState } from "react";
import { EventCard } from "../components/EventCard";
import { useEvents } from "../hooks/UseEvents";
import "./EventsPage.css";

type SortOption = "dateAsc" | "dateDesc" | "title";

const skeletons = Array.from({ length: 6 });

export function EventsPage() {
  const { data: events = [], isLoading, isError, refetch } = useEvents();

  const [search, setSearch] = useState("");
  const [city, setCity] = useState("all");
  const [sort, setSort] = useState<SortOption>("dateAsc");

  const cities = useMemo(() => {
    return [...new Set(events.map((event) => event.city))]
      .filter(Boolean)
      .sort((a, b) => a.localeCompare(b));
  }, [events]);

  const filteredEvents = useMemo(() => {
    const normalizedSearch = search.trim().toLowerCase();

    const result = events.filter((event) => {
      const matchesSearch =
        normalizedSearch.length === 0 ||
        event.title.toLowerCase().includes(normalizedSearch) ||
        event.description.toLowerCase().includes(normalizedSearch) ||
        event.city.toLowerCase().includes(normalizedSearch) ||
        event.country.toLowerCase().includes(normalizedSearch) ||
        event.venueName.toLowerCase().includes(normalizedSearch);

      const matchesCity =
        city === "all" || event.city === city;

      return matchesSearch && matchesCity;
    });

    return [...result].sort((a, b) => {
      switch (sort) {
        case "title":
          return a.title.localeCompare(b.title);

        case "dateDesc":
          return (
            new Date(b.startOfEvent).getTime() -
            new Date(a.startOfEvent).getTime()
          );

        case "dateAsc":
        default:
          return (
            new Date(a.startOfEvent).getTime() -
            new Date(b.startOfEvent).getTime()
          );
      }
    });
  }, [events, search, city, sort]);

  return (
    <main className="events-page">
      <section className="events-hero">
        <div className="events-hero__content">
          <span className="events-hero__eyebrow">
            LIVE THE MOMENT
          </span>

          <h1>
            Find your next
            <span> unforgettable night.</span>
          </h1>

          <p>
            Discover concerts, festivals, shows and experiences
            worth getting out of the house for.
          </p>

          <div className="events-hero__chips">
            <span>♫ Live music</span>
            <span>✦ Big moments</span>
            <span>♪ Good company</span>
          </div>
        </div>

        <div className="events-hero__art" aria-hidden="true">
          <div className="events-hero__disc">
            <div className="events-hero__disc-hole" />
          </div>

          <div className="events-hero__note events-hero__note--one">
            ♪
          </div>

          <div className="events-hero__note events-hero__note--two">
            ♫
          </div>

          <div className="events-hero__spark events-hero__spark--one">
            ✦
          </div>

          <div className="events-hero__spark events-hero__spark--two">
            ✦
          </div>
        </div>
      </section>

      <section className="events-content">
        <div className="events-toolbar">
          <div>
            <p className="events-toolbar__label">EXPLORE</p>
            <h2>Events worth showing up for</h2>
          </div>

          <span className="events-toolbar__count">
            {filteredEvents.length}{" "}
            {filteredEvents.length === 1 ? "event" : "events"}
          </span>
        </div>

        <div className="events-filters">
          <label className="events-search">
            <span aria-hidden="true">⌕</span>

            <input
              type="search"
              value={search}
              onChange={(event) => setSearch(event.target.value)}
              placeholder="Search events, venues, cities..."
              aria-label="Search events"
            />
          </label>

          <label className="events-filter">
            <span>City</span>

            <select
              value={city}
              onChange={(event) => setCity(event.target.value)}
            >
              <option value="all">All cities</option>

              {cities.map((cityName) => (
                <option key={cityName} value={cityName}>
                  {cityName}
                </option>
              ))}
            </select>
          </label>

          <label className="events-filter">
            <span>Sort by</span>

            <select
              value={sort}
              onChange={(event) =>
                setSort(event.target.value as SortOption)
              }
            >
              <option value="dateAsc">Soonest first</option>
              <option value="dateDesc">Latest first</option>
              <option value="title">A–Z</option>
            </select>
          </label>
        </div>

        {isLoading && (
          <div className="events-grid">
            {skeletons.map((_, index) => (
              <div className="event-card-skeleton" key={index}>
                <div className="event-card-skeleton__image" />

                <div className="event-card-skeleton__body">
                  <div className="event-card-skeleton__line event-card-skeleton__line--small" />
                  <div className="event-card-skeleton__line event-card-skeleton__line--title" />
                  <div className="event-card-skeleton__line" />
                  <div className="event-card-skeleton__line" />
                  <div className="event-card-skeleton__button" />
                </div>
              </div>
            ))}
          </div>
        )}

        {isError && !isLoading && (
          <div className="events-state">
            <div className="events-state__icon">!</div>

            <h3>We couldn't load the events.</h3>

            <p>
              Something went wrong while talking to the event
              service.
            </p>

            <button
              type="button"
              className="events-state__button"
              onClick={() => refetch()}
            >
              Try again
            </button>
          </div>
        )}

        {!isLoading &&
          !isError &&
          filteredEvents.length === 0 && (
            <div className="events-state">
              <div className="events-state__icon">♪</div>

              <h3>No events found.</h3>

              <p>
                Try another search or remove one of the filters.
              </p>

              <button
                type="button"
                className="events-state__button"
                onClick={() => {
                  setSearch("");
                  setCity("all");
                }}
              >
                Clear filters
              </button>
            </div>
          )}

        {!isLoading &&
          !isError &&
          filteredEvents.length > 0 && (
            <div className="events-grid">
              {filteredEvents.map((event) => (
                <EventCard key={event.id} event={event} />
              ))}
            </div>
          )}
      </section>
    </main>
  );
}