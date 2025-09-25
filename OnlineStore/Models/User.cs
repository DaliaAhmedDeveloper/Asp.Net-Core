namespace OnlineStore.Models;

using OnlineStore.Models.Enums;
public class User : SoftDeleteEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserType UserType { get; set; } = UserType.User;// Enum , Admin, Customer
    public ProviderType? Provider { get; set; } = null;//  "Facebook" ,  "Google"
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int UserAvailablePoints { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>(); // user has many address
    public ICollection<Order> Orders { get; set; } = new List<Order>(); // user has many orders
    public Cart? Cart { get; set; } // user has one cart
    public ICollection<Review> Reviews { get; set; } = new List<Review>(); // user has many reviews
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>(); // user has many notifications
    public Wallet Wallet { get; set; } = null!;
    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>(); // user can have list of copouns to choose from
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<UserPoint> Points { get; set; } = new List<UserPoint>();
    public ICollection<Return> Returns { get; set; } = new List<Return>();
    public RefreshToken RefreshToken { get; set; } = null!;
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    public int StateId { get; set; }
    public State State { get; set; } = null!;

    public ICollection<TicketMessage> TicketMessages { set; get; } = new List<TicketMessage>();
    public ICollection<SupportTicket> SupportTickets { set; get; } = new List<SupportTicket>();
    public ICollection<CouponUser> CouponUsers { get; set; } = new List<CouponUser>();
}
