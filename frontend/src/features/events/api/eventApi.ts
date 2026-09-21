import { httpClient } from "../../../shared/api/httpClient";
import type { EventResponse } from "../types/Event";

export async function getEvents(
  signal?: AbortSignal,
): Promise<EventResponse[]> {
  return httpClient.get<EventResponse[]>("/Event/all", signal);
}

export async function getEventById(
  id: string,
  signal?: AbortSignal,
): Promise<EventResponse> {
  return httpClient.get<EventResponse>(`/Event/${id}`, signal);
}