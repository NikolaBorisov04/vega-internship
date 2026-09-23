import { useQuery } from "@tanstack/react-query";
import { Client, type EventPhotoResponseDTO } from "../../../api/generated/api";

const apiClient = new Client(
  import.meta.env.VITE_API_URL
);

export function useEventPhoto(eventId: string | undefined) {
  return useQuery<EventPhotoResponseDTO[], Error>({
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