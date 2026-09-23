import type { EventResponseDTO } from "../../../api/generated/api";
import { httpClient } from "../../../shared/api/httpClient";

export async function getEvents(
  signal?: AbortSignal,
): Promise<EventResponseDTO[]> {
  return httpClient.get<EventResponseDTO[]>("/api/Event/all", signal);
}

export async function getEventById(
  id: string,
  signal?: AbortSignal,
): Promise<EventResponseDTO> {
  return httpClient.get<EventResponseDTO>(`/api/Event/${id}`, signal);
}