export function formatAvailability(quantityAvailable?: number) {
  if (quantityAvailable === undefined) {
    return "Availability unavailable";
  }

  if (quantityAvailable <= 0) {
    return "Sold out";
  }

  return `${quantityAvailable} available`;
}