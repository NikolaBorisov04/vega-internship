using CloudinaryDotNet.Actions;
using Events.Application.Storage;

namespace Events.Infrastructure.Storage;

public sealed class CloudinaryImageStorage : IImageStorage
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryImageStorage(
        CloudinaryDotNet.Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<StoredImageResult> UploadAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new CloudinaryDotNet.FileDescription(fileName, fileStream),
            PublicId = $"events/{Guid.NewGuid():N}"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            throw new InvalidOperationException(
                $"Cloudinary upload failed: {result.Error.Message}");
        }

        return new StoredImageResult(
            result.SecureUrl.AbsoluteUri,
            result.PublicId
        );
    }

    public async Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default)
    {
        var deletionParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deletionParams);

        if (result.Error != null)
        {
            throw new InvalidOperationException(
                $"Cloudinary deletion failed: {result.Error.Message}");
        }
    }
}