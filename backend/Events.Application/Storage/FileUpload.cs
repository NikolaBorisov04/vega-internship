namespace Events.Application.Storage;

public sealed record FileUpload(
    Stream Stream,
    string FileName,
    string ContentType,
    long Length
);