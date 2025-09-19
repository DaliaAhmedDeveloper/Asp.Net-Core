namespace OnlineStore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository User { get; }
    public IProductRepository Product { get; }
    public IProductVariantRepository ProductVariant { get; }
    public ITagRepository Tag { get; }
    public IStockRepository Stock { get; }
    public IStockMovementRepository StockMovement { get; }
    public ICouponRepository Coupon { get; }
    public IUserPointRepository Point { get; }
    public IWalletRepository Wallet { get; }
    public IOrderRepository Order { get; }
    public IOrderTrackingRepository OrderTracking { get; }
    public INotificationRepository Notification { get; }
    public ICartRepository Cart { get; }
    public ICartItemRepository CartItem { get; }
    public IWishlistRepository Wishlist { get; }
    public IRefreshTokenRepository RefreshToken { get; }
    public ICategoryRepository Category { get; }
    public IAddressRepository Address { get; }
    public IShippingMethodRepository ShippingMethod { get; }
    public IDataBaseExistsRepository DataBaseExists { get; }
    public IReviewRepository Review { get; }
    public IWarehouseRepository Warehouse { get; }
    public ISupportTicketRepository SupportTicket { get; }
    public ITicketMessageRepository TicketMessage { get; }
    public IProductAttributeValueRepository ProductAttributeValue { get; }

    public IReturnRepository Return { get; }
    public UnitOfWork(
        IUserRepository user,
        IStockRepository stock,
        IStockMovementRepository stockMovement,
        ICouponRepository coupon,
        IUserPointRepository point,
        IWalletRepository wallet,
        IOrderRepository order,
        IOrderTrackingRepository orderTracking,
        INotificationRepository notification,
        IProductRepository product,
        IProductVariantRepository productVariant,
        ITagRepository tag,
        ICartRepository cart,
        ICartItemRepository cartItem,
        IWishlistRepository wishlist,
        IRefreshTokenRepository refreshToken,
        IDataBaseExistsRepository dataBaseExists,
        ICategoryRepository category,
        IAddressRepository address,
        IShippingMethodRepository shippingMethod,
        IProductAttributeValueRepository productAttributeValue,
        IReviewRepository review,
        IReturnRepository _return,
        IWarehouseRepository warehouse,
        ISupportTicketRepository supportTicket,
        ITicketMessageRepository ticketMessage
    )
    {
        User = user;
        Stock = stock;
        StockMovement = stockMovement;
        Coupon = coupon;
        Point = point;
        Wallet = wallet;
        Order = order;
        OrderTracking = orderTracking;
        Notification = notification;
        Category = category;
        Product = product;
        ProductVariant = productVariant;
        Tag = tag;
        Cart = cart;
        CartItem = cartItem;
        Wishlist = wishlist;
        RefreshToken = refreshToken;
        DataBaseExists = dataBaseExists;
        Address = address;
        ShippingMethod = shippingMethod;
        ProductAttributeValue = productAttributeValue;
        Review = review;
        Return = _return;
        SupportTicket = supportTicket;
        TicketMessage = ticketMessage;
        Warehouse = warehouse;
    }

    // If you handle SaveChanges here, you can commit all changes at once.
    // public async Task<int> SaveChangesAsync() 
    // {
    //     return await _context.SaveChangesAsync();
    // }

    // Dispose : clean up objects after finish --  _context object
    // If you're using dependency injection (DI), you don't need to call Dispose manually.
    // public void Dispose()
    // {
    //     _context.Dispose();
    // }
}
