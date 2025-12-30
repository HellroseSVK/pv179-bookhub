using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GiftCard;
using BusinessLayer.Extensions;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class GiftCardService(CustomDBContext dbContext) : IGiftCardService
{
    public async Task<GiftCardDTO> CreateGiftCardAsync(GiftCardUpsertDTO genreDto)
    {
        ValidateGiftCardDates(genreDto.ValidFrom, genreDto.ValidTo);

        var giftCard = genreDto.Adapt<GiftCard>();

        var created = dbContext.GiftCards.Add(giftCard);

        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<GiftCardDTO>();
    }

    public async Task<List<GiftCardDTO>> GetGiftCardsAsync(int[]? ids = null, bool includeCodes = true)
    {
        var query = GetBaseQuery(includeCodes);

        if (ids is { Length: > 0 })
        {
            query = query.Where(p => ids.Contains(p.Id));
        }

        var giftCards = await query.ToListAsync();

        return giftCards.Adapt<List<GiftCardDTO>>();
    }

    public async Task<List<GiftCardSummaryDTO>> GetGiftCardsSummaryAsync(int[]? ids = null)
    {
        var query = GetBaseQuery(includeCodes: false);

        if (ids is { Length: > 0 })
        {
            query = query.Where(p => ids.Contains(p.Id));
        }

        var giftCards = await query.ToListAsync();

        return giftCards.Adapt<List<GiftCardSummaryDTO>>();
    }

    public async Task<PagedResult<GiftCardDTO>> GetGiftCardPaginatedAsync(PaginationParameters paginationParameters,
        int[]? ids = null, bool includeCodes = true)
    {
        var query = GetBaseQuery(includeCodes);

        if (ids is { Length: > 0 })
        {
            query = query.Where(p => ids.Contains(p.Id));
        }

        var totalCount = await query.CountAsync();

        var giftCards = await query
            .Paginate(paginationParameters.PageNumber, paginationParameters.PageSize)
            .ToListAsync();

        return new PagedResult<GiftCardDTO>
        {
            Items = giftCards.Adapt<List<GiftCardDTO>>(),
            TotalCount = totalCount,
            PageNumber = paginationParameters.PageNumber,
            PageSize = paginationParameters.PageSize
        };
    }

    public async Task<GiftCardDTO> GetGiftCardAsync(int id)
    {
        var giftCard = await FindGiftCard(id);

        return giftCard.Adapt<GiftCardDTO>();
    }

    public async Task<List<GiftCardDTO>> SearchGiftCardsAsync(string searchTerm, bool includeCodes = true)
    {
        var query = GetBaseQuery(includeCodes);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(giftCard => giftCard.Title.ToLower().Contains(searchTerm.ToLower()));
        }

        var giftCards = await query.ToListAsync();
        return giftCards.Adapt<List<GiftCardDTO>>();
    }

    public async Task<GiftCardDTO> AddGiftCodesToGiftCard(int id, int numberOfCodes)
    {
        var giftCard = await FindGiftCard(id);

        for (int i = 0; i < numberOfCodes; i++)
        {
            var giftCode = new GiftCode
            {
                Code = Guid.NewGuid().ToString(),
                GiftCardId = giftCard.Id
            };
            dbContext.GiftCodes.Add(giftCode);
        }

        await dbContext.SaveChangesAsync();

        return giftCard.Adapt<GiftCardDTO>();
    }

    public async Task<GiftCardDTO> RemoveGiftCodesToGiftCard(int id, string[] codes)
    {
        var giftCard = await FindGiftCard(id);

        var codesToRemove = giftCard.GiftCodes
            .Where(gc => codes.Contains(gc.Code))
            .ToList();

        var codesNotFound = codes.Except(codesToRemove.Select(c => c.Code)).ToList();

        if (codesNotFound.Any())
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                "Gift Codes not found: " + string.Join(", ", codesNotFound));
        }

        dbContext.GiftCodes.RemoveRange(codesToRemove);
        await dbContext.SaveChangesAsync();

        return giftCard.Adapt<GiftCardDTO>();
    }

    public async Task PatchGiftCardAsync(int id, GiftCardPartialDTO genreDto)
    {
        var giftCard = await FindGiftCard(id, false);

        giftCard.Title = genreDto.Title ?? giftCard.Title;

        var validFrom = genreDto.ValidFrom ?? giftCard.ValidFrom;
        var validTo = genreDto.ValidTo ?? giftCard.ValidTo;

        if (genreDto.ValidFrom.HasValue || genreDto.ValidTo.HasValue)
        {
            ValidateGiftCardDates(validFrom, validTo);
        }

        giftCard.ValidFrom = validFrom;
        giftCard.ValidTo = validTo;
        giftCard.Price = genreDto.Price ?? giftCard.Price;

        dbContext.GiftCards.Update(giftCard);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateGiftCardAsync(int id, GiftCardUpsertDTO genreDto)
    {
        var giftCard = await FindGiftCard(id, false);

        ValidateGiftCardDates(genreDto.ValidFrom, genreDto.ValidTo);

        giftCard.Title = genreDto.Title;
        giftCard.ValidFrom = genreDto.ValidFrom;
        giftCard.ValidTo = genreDto.ValidTo;
        giftCard.Price = genreDto.Price;

        dbContext.GiftCards.Update(giftCard);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGiftCardAsync(int id)
    {
        var giftCard = await FindGiftCard(id);

        dbContext.GiftCodes.RemoveRange(giftCard.GiftCodes);
        dbContext.GiftCards.Remove(giftCard);

        await dbContext.SaveChangesAsync();
    }

    private async Task<GiftCard> FindGiftCard(int giftCardId, bool includeCodes = true)
    {
        var query = GetBaseQuery(includeCodes);

        var giftCard = await query.FirstOrDefaultAsync(GiftCard => GiftCard.Id == giftCardId);

        return giftCard ?? throw new StatusCodeException(StatusCodes.Status404NotFound,
            $"Gift Card with id {giftCardId} not found.");
    }

    private IQueryable<GiftCard> GetBaseQuery(bool includeCodes)
    {
        IQueryable<GiftCard> userQuery = dbContext.GiftCards;

        if (includeCodes)
        {
            userQuery = userQuery.Include(giftCard => giftCard.GiftCodes);
        }

        return userQuery;
    }

    private void ValidateGiftCardDates(DateTime validFrom, DateTime validTo)
    {
        if (validFrom >= validTo)
        {
            throw new StatusCodeException(
                StatusCodes.Status400BadRequest,
                "ValidFrom must be before ValidTo");
        }
    }

    public bool IsGiftCardValid(GiftCardDTO giftCard)
    {
        var now = DateTime.UtcNow;
        return now >= giftCard.ValidFrom && now <= giftCard.ValidTo;
    }

    public async Task<GiftCodeDTO> ValidateGiftCodeAsync(string code)
    {
        var giftCode = await dbContext.GiftCodes
            .Include(gc => gc.GiftCard)
            .FirstOrDefaultAsync(gc => gc.Code.ToLower() == code.ToLower());

        if (giftCode == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                "Gift code not found.");
        }

        if (giftCode.OrderId != null)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest,
                "Gift code has already been used.");
        }

        var now = DateTime.UtcNow;
        if (now < giftCode.GiftCard!.ValidFrom || now > giftCode.GiftCard.ValidTo)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest,
                "Gift code is not currently valid.");
        }

        return giftCode.Adapt<GiftCodeDTO>();
    }

    public async Task<GiftCardDTO> GetGiftCardByCodeAsync(string code)
    {
        var giftCode = await dbContext.GiftCodes
            .Include(gc => gc.GiftCard)
            .FirstOrDefaultAsync(gc => gc.Code.ToLower() == code.ToLower());

        if (giftCode == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                "Gift code not found.");
        }

        return giftCode.GiftCard!.Adapt<GiftCardDTO>();
    }
}