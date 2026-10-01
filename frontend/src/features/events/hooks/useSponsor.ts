import { useQuery } from "@tanstack/react-query";

import { type ProblemDetails, type SponsorResponseDTO } from "../../../api/generated/api";
import { apiClient } from "../../../api/client";

export function useSponsor(eventId?: string) {
  return useQuery<SponsorResponseDTO[], ProblemDetails>({
    queryKey: ["event-sponsors", eventId],
    queryFn: () => {
      if (!eventId) {
        throw new Error("Event ID is required to load sponsors.");
      }

      return apiClient.eventAll2(eventId);
    },
    enabled: Boolean(eventId),
  });
}