export function formatDateTime(value: Date | string): string {
  const date = value instanceof Date ? value : new Date(value);

  if (Number.isNaN(date.getTime())) {
    return "Invalid date";
  }

  return new Intl.DateTimeFormat("sr-RS", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  }).format(date);
}

// Smart formatter for event start/end ranges

export function formatEventRange(
  startValue?: Date | string | null,
  endValue?: Date | string | null
): string {
  if (!startValue) return "";

  const start = startValue instanceof Date ? startValue : new Date(startValue);
  if (Number.isNaN(start.getTime())) return "Invalid date";

  const dateFormatter = new Intl.DateTimeFormat("sr-RS", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });

  const timeFormatter = new Intl.DateTimeFormat("sr-RS", {
    hour: "2-digit",
    minute: "2-digit",
  });

  const startDate = dateFormatter.format(start);
  const startTime = timeFormatter.format(start);

  if (!endValue) {
    return `${startDate} ${startTime}`;
  }

  const end = endValue instanceof Date ? endValue : new Date(endValue);
  if (Number.isNaN(end.getTime()) || start.getTime() === end.getTime()) {
    return `${startDate} ${startTime}`;
  }

  const endDate = dateFormatter.format(end);
  const endTime = timeFormatter.format(end);

  const isSameDay =
    start.getFullYear() === end.getFullYear() &&
    start.getMonth() === end.getMonth() &&
    start.getDate() === end.getDate();

  if (isSameDay) {
    return `${startDate} ${startTime} – ${endTime}`;
  }

  return `${startDate} ${startTime} – ${endDate} ${endTime}`;
}