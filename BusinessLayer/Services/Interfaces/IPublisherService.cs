using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Publisher;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService
{
    public Task<PublisherDTO> CreatePublisherAsync(PublisherUpsertDTO publisherDto);

    public Task<List<PublisherDTO>> GetPublishersAsync(int[]? ids = null, bool includeBooks = true);

    public Task<PagedResult<PublisherDTO>> GetPublisherPaginatedAsync(PaginationParameters paginationParameters,
        int[]? ids = null, bool includeBooks = true);

    public Task<PublisherDTO> GetPublisherAsync(int id, bool includeBooks = true);

    public Task<List<PublisherDTO>> SearchPublishersAsync(string searchTerm);

    public Task PatchPublisherAsync(int id, PublisherPartialDTO publisherDto);
    public Task UpdatePublisherAsync(int id, PublisherUpsertDTO publisherDto);
    public Task DeletePublisherAsync(int id);
}