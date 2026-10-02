import { useQuery } from "@tanstack/react-query";

import {
  type ProblemDetails,
  type UserResponseDTO,
} from "../../../api/generated/api";
import { apiClient } from "../../../api/client";

export function useOrganizer(eventId?: string) {
  return useQuery<UserResponseDTO, ProblemDetails>({
    queryKey: ["event-organizer", eventId],
    queryFn: () => {
      if (!eventId) {
        throw new Error("Event ID is required to load organizer.");
      }

      return apiClient.event(eventId);
    },
    enabled: Boolean(eventId),
  });
}