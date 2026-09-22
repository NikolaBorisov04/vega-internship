import type { SortOption } from "../../hooks/useEventFilters";
import "./EventFilters.css";

interface EventFiltersProps {
  search: string;
  city: string;
  sort: SortOption;
  cities: string[];
  onSearchChange: (value: string) => void;
  onCityChange: (value: string) => void;
  onSortChange: (value: SortOption) => void;
}

export function EventFilters({
  search,
  city,
  sort,
  cities,
  onSearchChange,
  onCityChange,
  onSortChange,
}: EventFiltersProps) {
  return (
    <div className="events-filters">
      <label className="events-search">
        <span aria-hidden="true">⌕</span>

        <input
          type="search"
          value={search}
          onChange={(event) => onSearchChange(event.target.value)}
          placeholder="Search events, venues, cities..."
          aria-label="Search events"
        />
      </label>

      <label className="events-filter">
        <span>City</span>

        <select
          value={city}
          onChange={(event) => onCityChange(event.target.value)}
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
            onSortChange(event.target.value as SortOption)
          }
        >
          <option value="dateAsc">Soonest first</option>
          <option value="dateDesc">Latest first</option>
          <option value="title">A–Z</option>
        </select>
      </label>
    </div>
  );
}