import { useEvent } from "../../../hooks/useEvent";

export type EventDetailsEvent = NonNullable<
  ReturnType<typeof useEvent>["data"]
>;