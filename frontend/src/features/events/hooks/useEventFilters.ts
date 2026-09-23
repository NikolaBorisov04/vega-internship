import { useMemo, useState } from "react";
import type { EventResponseDTO } from "../../../api/generated/api";
import { useDebouncedValue } from "../../../hooks/useDebouncedValue";

export type SortOption = "dateAsc" | "dateDesc" | "title";

const SEARCH_DEBOUNCE_MS = 500;

export function useEventFilters(events: EventResponseDTO[]) {
  const [search, setSearch] = useState("");
  const [city, setCity] = useState("all");
  const [sort, setSort] = useState<SortOption>("dateAsc");

  const debouncedSearch = useDebouncedValue(
    search,
    SEARCH_DEBOUNCE_MS,
  );

  const cities = useMemo(() => {
    return [...new Set(events.map((event) => event.city))]
      .filter(Boolean)
      .sort((a, b) => a.localeCompare(b));
  }, [events]);

  const filteredEvents = useMemo(() => {
    const normalizedSearch = debouncedSearch.trim().toLowerCase();

    const filtered = events.filter((event) => {
      const matchesSearch =
        normalizedSearch.length === 0 ||
        event.title.toLowerCase().includes(normalizedSearch) ||
        event.description.toLowerCase().includes(normalizedSearch) ||
        event.city.toLowerCase().includes(normalizedSearch) ||
        event.country.toLowerCase().includes(normalizedSearch) ||
        event.venueName?.toLowerCase().includes(normalizedSearch);

      const matchesCity =
        city === "all" || event.city === city;

      return matchesSearch && matchesCity;
    });

    return [...filtered].sort((a, b) => {
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
  }, [events, debouncedSearch, city, sort]);

  function clearFilters() {
    setSearch("");
    setCity("all");
    setSort("dateAsc");
  }

  return {
    search,
    setSearch,
    city,
    setCity,
    sort,
    setSort,
    cities,
    filteredEvents,
    clearFilters,
  };
}