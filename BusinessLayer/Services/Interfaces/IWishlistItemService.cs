using BusinessLayer.DTOs.WishlistItem;

namespace BusinessLayer.Services.Interfaces
{
    public interface IWishlistItemService
    {
        public Task<WishlistItemDTO> CreateWishlistItemAsync(WishlistItemUpsertDTO wishlistItem);

        public Task<List<WishlistItemDTO>> GetWishlistItemsAsync(int[]? ids = null, bool includeBook = true, bool includeUser = true);

        public Task<WishlistItemDTO> GetWishlistItemAsync(int id, bool includeBook = true, bool includeUser = true);

        public Task PatchWishlistItemAsync(int id, WishlistItemPartialDTO wishlistItem);
        public Task UpdateWishlistItemAsync(int id, WishlistItemUpsertDTO wishlistItem);
        public Task DeleteWishlistItemAsync(int id);
    }
}
