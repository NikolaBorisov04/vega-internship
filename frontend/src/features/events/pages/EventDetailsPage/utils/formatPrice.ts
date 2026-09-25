export function formatPrice(price?: number) {
  if (price === undefined) {
    return "Price unavailable";
  }

  if (price === 0) {
    return "Free";
  }

  return `${price.toLocaleString(undefined, {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })} EUR`;
}