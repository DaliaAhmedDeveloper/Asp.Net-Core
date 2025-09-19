namespace OnlineStore.Repositories;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    IProductRepository Product { get; }
    ITagRepository Tag { get; }
    ICartRepository Cart { get; }
    ICartItemRepository CartItem { get; }
    IWishlistRepository Wishlist { get; }
    IDataBaseExistsRepository DataBaseExists { get; }
    IRefreshTokenRepository RefreshToken { get; }
    IStockRepository Stock { get; }
    IProductVariantRepository ProductVariant { get; }
    IStockMovementRepository StockMovement { get; }
    ICouponRepository Coupon { get; }
    IUserPointRepository Point { get; }
    IWalletRepository Wallet { get; }
    IOrderRepository Order { get; }
    IOrderTrackingRepository OrderTracking { get; }
    INotificationRepository Notification { get; }
    ICategoryRepository Category { get; }
    IAddressRepository Address { get; }
    IShippingMethodRepository ShippingMethod { get; }
    IProductAttributeValueRepository ProductAttributeValue { get; }
    IReturnRepository Return { get; }
    public IWarehouseRepository Warehouse { get; }
    public ISupportTicketRepository SupportTicket { get; }
    public ITicketMessageRepository TicketMessage { get; }
    public IReviewRepository Review { get; }

    // Optional: Unified commit method if not calling SaveChanges in repositories
    // Task<int> SaveChangesAsync();

    // Optional: If not using dependency injection's scope handling
    // void Dispose();
}