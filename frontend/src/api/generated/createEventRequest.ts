// Since I am changing the backend a lot I put this here so that NSwag doesn't overwrite it when I run nswag run nswag.json

export interface CreateEventRequest {
    title: string;
    description: string;
    country: string;
    city: string;
    address: string;
    venueName: string;
    startOfEvent: string;
    endOfEvent: string;
    mainImage: File | null;
}