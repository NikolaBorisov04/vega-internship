import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../../../api/client";
import type { EventPhotoResponseDTO, ProblemDetails } from "../../../api/generated/api";

export function useEventPhoto(eventId: string | undefined) {
  return useQuery<EventPhotoResponseDTO[], ProblemDetails>({
    queryKey: ["event-photos", eventId],

    queryFn: async () => {
      if (!eventId) {
        throw new Error("Event ID is required.");
      }

      return apiClient.eventAll(eventId);
    },

    enabled: Boolean(eventId),
  });
}