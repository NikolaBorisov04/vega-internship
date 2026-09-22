import { useQuery } from "@tanstack/react-query";
import { getEventById } from "../api/eventApi";

export function useEvent(id: string | undefined) {
  return useQuery({
    queryKey: ["event", id],
    queryFn: ({ signal }) => getEventById(id!, signal),
    enabled: Boolean(id),
    staleTime: 60_000,
  });
}