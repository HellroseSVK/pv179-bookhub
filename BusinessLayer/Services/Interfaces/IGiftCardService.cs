using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GiftCard;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardService
{
    public Task<GiftCardDTO> CreateGiftCardAsync(GiftCardUpsertDTO genreDto);

    public Task<List<GiftCardDTO>> GetGiftCardsAsync(int[]? ids = null, bool includeCodes = true);

    public Task<List<GiftCardSummaryDTO>> GetGiftCardsSummaryAsync(int[]? ids = null);

    public Task<PagedResult<GiftCardDTO>> GetGiftCardPaginatedAsync(PaginationParameters paginationParameters,
        int[]? ids = null, bool includeCodes = true);

    public Task<GiftCardDTO> GetGiftCardAsync(int id);

    public Task<List<GiftCardDTO>> SearchGiftCardsAsync(string searchTerm, bool includeCodes = true);

    public Task<GiftCardDTO> AddGiftCodesToGiftCard(int id, int numberOfCodes);
    public Task<GiftCardDTO> RemoveGiftCodesToGiftCard(int id, string[] codes);

    public Task PatchGiftCardAsync(int id, GiftCardPartialDTO genreDto);
    public Task UpdateGiftCardAsync(int id, GiftCardUpsertDTO genreDto);
    public Task DeleteGiftCardAsync(int id);

    public bool IsGiftCardValid(GiftCardDTO giftCard);

    public Task<GiftCodeDTO> ValidateGiftCodeAsync(string code);
    public Task<GiftCardDTO> GetGiftCardByCodeAsync(string code);
}