export type EventPriority = "Standard" | "High";

export interface EventResponse {
  id: string;
  title: string;
  description: string;
  country: string;
  city: string;
  address: string;
  mainImageURL: string;
  venueName: string;
  priority: EventPriority;
  startOfEvent: string;
  endOfEvent: string;
  organizerId: string;
}