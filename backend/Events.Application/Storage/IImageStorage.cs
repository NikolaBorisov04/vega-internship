namespace Events.Application.Storage;

public interface IImageStorage
{
    Task<StoredImageResult> UploadAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default);
}