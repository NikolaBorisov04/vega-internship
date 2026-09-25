import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../../../api/client";
import type { ProblemDetails, TicketTypeResponseDTO } from "../../../api/generated/api";

export function useEventTicketTypes(
  eventId: string | undefined,
) {
  return useQuery<TicketTypeResponseDTO[], ProblemDetails>({
    queryKey: ["event-ticket-types", eventId],

    queryFn: async () => {
      if (!eventId) {
        throw new Error("Event ID is required.");
      }

      return apiClient.eventAll3(eventId);
    },

    enabled: Boolean(eventId),
  });
}