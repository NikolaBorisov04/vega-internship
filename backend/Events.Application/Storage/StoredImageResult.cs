namespace Events.Application.Storage;

public sealed record StoredImageResult(
    string Url,
    string PublicId
);