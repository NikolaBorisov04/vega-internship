import { useQuery } from "@tanstack/react-query";
import { getEvents } from "../api/EventApi";

export function useEvents() {
  return useQuery({
    queryKey: ["events"],
    queryFn: ({ signal }) => getEvents(signal),
    staleTime: 60_000,
  });
}