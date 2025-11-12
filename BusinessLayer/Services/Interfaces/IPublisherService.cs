using BusinessLayer.DTOs.Publisher;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService
{
    public Task<PublisherDTO> CreateAuthorAsync(PublisherUpsertDTO publisherDto);

    public Task<List<PublisherDTO>> GetAuthorsAsync(int[]? ids = null, bool includeBooks = true);

    public Task<PublisherDTO> GetAuthorAsync(int id, bool includeBooks = true);

    public Task PatchAuthorAsync(int id, PublisherPartialDTO publisherDto);
    public Task UpdateAuthorAsync(int id, PublisherUpsertDTO publisherDto);
    public Task DeleteAuthorAsync(int id);
}