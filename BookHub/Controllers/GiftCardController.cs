using BusinessLayer.DTOs.GiftCard;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftCardController(IGiftCardService giftCardService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetGiftCards()
    {
        var giftCards = await giftCardService.GetGiftCardsSummaryAsync();
        return Ok(giftCards);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetGiftCard(int id)
    {
        var giftCard = await giftCardService.GetGiftCardAsync(id);
        return Ok(giftCard);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateGiftCard([FromBody] GiftCardUpsertDTO giftCardDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await giftCardService.CreateGiftCardAsync(giftCardDto);
        return CreatedAtAction(nameof(GetGiftCard), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateGiftCard(int id, [FromBody] GiftCardUpsertDTO giftCardDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await giftCardService.UpdateGiftCardAsync(id, giftCardDto);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PatchGiftCard(int id, [FromBody] GiftCardPartialDTO giftCardDto)
    {
        await giftCardService.PatchGiftCardAsync(id, giftCardDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGiftCard(int id)
    {
        await giftCardService.DeleteGiftCardAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/is-valid")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> IsGiftCardValid(int id)
    {
        var giftCard = await giftCardService.GetGiftCardAsync(id);
        var isValid = giftCardService.IsGiftCardValid(giftCard);
        return Ok(new { isValid, validFrom = giftCard.ValidFrom, validTo = giftCard.ValidTo });
    }

    [HttpPost("{id}/codes")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCodes(int id, [FromBody] int numberOfCodes)
    {
        await giftCardService.AddGiftCodesToGiftCard(id, numberOfCodes);
        return NoContent();
    }

    [HttpDelete("{id}/codes")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveCodes(int id, [FromBody] string[] codes)
    {
        await giftCardService.RemoveGiftCodesToGiftCard(id, codes);
        return NoContent();
    }
}
