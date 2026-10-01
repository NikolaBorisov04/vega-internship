using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Application.Storage;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateSponsorCommandHandler : IRequestHandler<CreateSponsorCommand, SponsorResponseDTO>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly IImageStorage _imageStorage;

    public CreateSponsorCommandHandler(
        ISponsorRepository sponsorRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        IImageStorage imageStorage)
    {
        _sponsorRepository = sponsorRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _imageStorage = imageStorage;
    }
    public async Task<SponsorResponseDTO> Handle(CreateSponsorCommand command, CancellationToken ct)
    {
        var image = await _imageStorage.UploadAsync(command.Image.Stream, command.Image.FileName, ct);

        try
        {
            var newsponsor = SponsorFactory.Create(command, image.Url, image.PublicId);

            _sponsorRepository.Add(newsponsor);

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseMapper.MapToResponse(newsponsor);
        }
        catch
        {
            await _imageStorage.DeleteAsync(image.PublicId, ct);

            throw new ArgumentException("Sponsor wasn't created succesfully.");
        }
    }
}